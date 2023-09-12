using AP.ChevronCoop.AppCore.Services.Helpers;
using AP.ChevronCoop.AppDomain.Deposits.DepositTransactions;
using AP.ChevronCoop.AppDomain.Loans.LoanTransactions;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.JournalEntries;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestAdditions;
using AP.ChevronCoop.Entities.LoanOffsetTransactions;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using AP.ChevronCoop.Entities.Loans.LoanDisbursements;
using AP.ChevronCoop.Entities.Loans.LoanProducts;
using AP.ChevronCoop.Entities.Loans.LoanRepaymentSchedules;
using AP.ChevronCoop.Entities.LoanTopupTransactions;
using AP.ChevronCoop.Entities.Payroll;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace AP.ChevronCoop.AppCore.Loans.LoanTransactions;

public class LoanTransactionCommandHandler :
  IRequestHandler<LoanTransactionCommand, CommandResult<LoanTransactionViewModel>>
{
    private readonly ChevronCoopDbContext dbContext;
    private readonly IEmailService _emailService;
    private readonly ILogger logger;
    private readonly IMapper _mapper;
    private readonly CoreAppSettings _options;

    public LoanTransactionCommandHandler(ChevronCoopDbContext appDbContext,
      ILogger<LoanTransactionCommandHandler> _logger, IMapper mapper, IEmailService emailService,
      IOptions<CoreAppSettings> options)
    {
        dbContext = appDbContext;
        logger = _logger;
        _mapper = mapper;
        _emailService = emailService;
        _options = options.Value;
    }

    public async Task<CommandResult<LoanTransactionViewModel>> Handle(LoanTransactionCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<LoanTransactionViewModel>();
        var response = new LoanTransactionViewModel();
        switch (request.TransactionType)
        {
            case TransactionType.LOAN_TOPUP:
                response = ProcessLoanTopup(request, cancellationToken);
                break;

            case TransactionType.LOAN_OFFSET_TRANSFER:
                response = ProcessLoanOffsetTransfer(request, cancellationToken);
                break;

            case TransactionType.LOAN_OFFSET_SAVINGS:
                response = ProcessLoanOffsetSavings(request, cancellationToken);
                break;

            case TransactionType.LOAN_OFFSET_SPECIAL_DEPOSIT:
                response = ProcessLoanOffsetSpecialDeposit(request, cancellationToken);
                break;

            case TransactionType.LOAN_DISBURSEMENT_FULL:
                response = ProcessLoanDisbursementFull(request, cancellationToken);
                break;

            case TransactionType.LOAN_DISBURSEMENT_TOPUP:
                response = ProcessLoanTopupDisbursement(request, cancellationToken);
                break;

            case TransactionType.LOAN_INITIALIZE_OFFSET:
                response = ProcessInitializeLoanOffset(request, cancellationToken);
                break;

            case TransactionType.LOAN_DISBURSEMENT_PARTIAL:
                response = ProcessLoanDisbursementPartial(request, cancellationToken);
                break;

            case TransactionType.LOAN_CASH_REPAYMENT:
                response = ProcessLoanCashRepayment(request, cancellationToken);
                break;

            case TransactionType.LOAN_PAYROLL_REPAYMENT:
                response = ProcessLoanPayrollRepayment(request, cancellationToken);
                break;
        }

        rsp.Response = response;
        return rsp;
    }

    private LoanTransactionViewModel ProcessLoanTopup(LoanTransactionCommand request, CancellationToken cancellationToken)
    {
        /*
		
		:old loan
//loan tracking:principal Product
DR LoanProduct.PrincipalAccount principalBalance
CR LOAN_PROD_PRINCIPAL_CONTROL principalBalance


//loan tracking:principal Account
DR LoanAccount.PrincipalBalanceAccount principalBalance
CR LOAN_ACC_PRINCIPAL_CONTROL principalBalance


//loan tracking:unearned interest Product
DR LOAN_PROD_UNEARNED_INTEREST_CONTROL unearnedInterestBalance
CR Prod.UnearnedInterest unearnedInterestBalance


//loan tracking:unearned interest Account
DR LOAN_ACC_UNEARNED_INTEREST_CONTROL unEarnedInterestBalance
CR Acc.UnearnedInterest unEarnedInterestBalance



		*/


        logger.LogInformation("entering ProcessLoanTopup");


        LoanTransactionViewModel rsp = new LoanTransactionViewModel();


        try
        {

            TransactionJournal journal = new TransactionJournal();
            journal.TransactionNo = NHiloHelper.GetNextKey(nameof(TransactionJournal)).ToString();
            journal.Title = $"Loan Topup";
            journal.TransactionType = request.TransactionType;
            journal.TransactionDate = request.TransactionDate;
            journal.DocumentRef = nameof(LoanAccount);
            journal.DocumentRefId = request.LoanAccountId;
            journal.EntityRef = nameof(LoanTopup);
            journal.EntityRefId = request.EntityId;

            var txn = dbContext.LoanTopups.Where(p => p.Id == request.EntityId)
                .Include(p => p.LoanAccount).ThenInclude(p => p.LoanApplication).ThenInclude(p => p.LoanProduct)
                .Include(p => p.LoanAccount).ThenInclude(p => p.ParentAccount)
                .Include(p => p.CustomerBankAccount).ThenInclude(p => p.LedgerAccount)
                .Include(p => p.SpecialDepositAccount).ThenInclude(p => p.DepositAccount)
                .FirstOrDefault();

            var charges = dbContext.LoanTopupCharges.Where(p => p.LoanTopupId == request.EntityId);

            var loanAccount = dbContext.LoanAccounts.Where(r => r.Id == txn.LoanAccount.ParentAccountId)
                .Include(p => p.LoanApplication).ThenInclude(p => p.LoanProduct)
                .ThenInclude(p => p.BankDepositAccount).ThenInclude(p => p.LedgerAccount)
                .Include(c => c.Customer).ThenInclude(c => c.CashAccount)
                .Include(c => c.ParentAccount)
                .FirstOrDefault();

            var _loanAccountQuery = dbContext.LoanAccounts.Where(r => r.Id == txn.LoanAccount.ParentAccountId);

            var _loanProductQuery = _loanAccountQuery.Include(x => x.LoanApplication)
                .ThenInclude(x => x.LoanProduct);



            LedgerAccount CUST_CASH_ACC = loanAccount.Customer.CashAccount;

            // loanAccount.PrincipalBalanceAccountId
            LedgerAccount LOAN_ACC_PRINCIPAL_BALANCE = _loanAccountQuery.Include(x => x.PrincipalBalanceAccount)
                .Select(c => c.PrincipalBalanceAccount).FirstOrDefault();

            // loanAccount.UnearnedInterestAccountId
            LedgerAccount LOAN_ACC_UNEARNED_INTEREST = _loanAccountQuery.Include(x => x.UnearnedInterestAccount)
                .Select(c => c.UnearnedInterestAccount).FirstOrDefault();

            // loanAccount.EarnedInterestAccountId
            LedgerAccount LOAN_ACC_EARNED_INTEREST = _loanAccountQuery.Include(x => x.EarnedInterestAccount)
                .Select(c => c.EarnedInterestAccount).FirstOrDefault();

            // loanAccount.ChargesAccruedAccountId
            LedgerAccount LOAN_ACC_CHARGES_ACCRUAL = _loanAccountQuery.Include(x => x.ChargesAccruedAccount)
                .Select(c => c.ChargesAccruedAccount).FirstOrDefault();


            // loanAccount.LoanApplication.LoanProduct.PrincipalAccount.Id
            LedgerAccount LOAN_PROD_PRINCIPAL = _loanProductQuery.ThenInclude(x => x.PrincipalAccount)
                .Select(c => c.LoanApplication.LoanProduct.PrincipalAccount)
                .FirstOrDefault();


            // "loanAccount.LoanApplication.LoanProduct.UnearnedInterestAccountId"
            LedgerAccount LOAN_PROD_UNEARNED_INTEREST = _loanProductQuery.ThenInclude(x => x.UnearnedInterestAccount)
                .Select(c => c.LoanApplication.LoanProduct.UnearnedInterestAccount)
                .FirstOrDefault();

            LedgerAccount LOAN_PROD_EARNED_INTEREST = _loanProductQuery.ThenInclude(x => x.InterestIncomeAccount)
               .Select(c => c.LoanApplication.LoanProduct.InterestIncomeAccount)
               .FirstOrDefault();

            // loanAccount.LoanApplication.LoanProduct.ChargesAccrualAccountId
            LedgerAccount LOAN_PROD_CHARGES_ACCRUAL = _loanProductQuery.ThenInclude(x => x.ChargesAccrualAccount)
                .Select(c => c.LoanApplication.LoanProduct.ChargesAccrualAccount)
                .FirstOrDefault();





            LedgerAccount LOAN_PROD_PRINCIPAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PROD_PRINCIPAL_CONTROL.ToString());

            LedgerAccount LOAN_ACC_PRINCIPAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_ACC_PRINCIPAL_CONTROL.ToString());

            LedgerAccount LOAN_PROD_UNEARNED_INTEREST_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PROD_UNEARNED_INTEREST_CONTROL.ToString());


            LedgerAccount LOAN_PROD_EARNED_INTEREST_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PROD_EARNED_INTEREST_CONTROL.ToString());



            LedgerAccount LOAN_ACC_UNEARNED_INTEREST_CONTROL =
               dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_ACC_UNEARNED_INTEREST_CONTROL.ToString());


            LedgerAccount LOAN_ACC_EARNED_INTEREST_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_ACC_EARNED_INTEREST_CONTROL.ToString());



            LedgerAccount LOAN_PROD_CHARGE_ACCRUAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PROD_CHARGE_ACCRUAL_CONTROL.ToString());

            LedgerAccount LOAN_ACC_CHARGE_ACCRUAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_ACC_CHARGE_ACCRUAL_CONTROL.ToString());


            LedgerAccount LOAN_CUST_BANK_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_CUST_BANK_CONTROL.ToString());


            var loanhelper = new LoanHelper(loanAccount.Principal, loanAccount.LoanApplication.LoanProduct.InterestRate,
                loanAccount.LoanApplication.LoanProduct.InterestMethod,
                loanAccount.LoanApplication.LoanProduct.InterestCalculationMethod,
                loanAccount.TenureUnit, loanAccount.TenureValue,
                loanAccount.LoanApplication.LoanProduct.RepaymentPeriod,
                loanAccount.LoanApplication.LoanProduct.DaysInYear,
                loanAccount.RepaymentCommencementDate.UtcDateTime);

            var paymentSchedules = loanhelper.GetAmortizationTable(loanAccount.LoanApplication.LoanProduct.InterestCalculationMethod);
            var firstPayment = paymentSchedules.FirstOrDefault();



            // :old loan
            //loan tracking:principal Product
            //DR LoanProduct.PrincipalAccount principalBalance
            //CR LOAN_PROD_PRINCIPAL_CONTROL principalBalance
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = LOAN_PROD_PRINCIPAL.Id,
                Account = LOAN_PROD_PRINCIPAL,
                Debit = txn.OldPrincipalBalance,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = LOAN_PROD_PRINCIPAL_CONTROL.Id,
                Account = LOAN_PROD_PRINCIPAL_CONTROL,
                Credit = txn.OldPrincipalBalance,
                TransactionDate = request.TransactionDate
            });



            // :old loan
            //loan tracking:principal Account
            //DR LoanAccount.PrincipalBalanceAccount principalBalance
            //CR LOAN_ACC_PRINCIPAL_CONTROL principalBalance

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = LOAN_ACC_PRINCIPAL_BALANCE.Id,
                Account = LOAN_ACC_PRINCIPAL_BALANCE,
                Debit = txn.OldPrincipalBalance,
                TransactionDate = request.TransactionDate
            });
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = LOAN_ACC_PRINCIPAL_CONTROL.Id,
                Account = LOAN_ACC_PRINCIPAL_CONTROL,
                Credit = txn.OldPrincipalBalance,
                TransactionDate = request.TransactionDate
            });




            //No need for this, for reporting/audit purposes, a record of unearned interest is needed
            ////Reduce unearned interest balance

            ////loan tracking:unearned interest Product
            ////DR Prod.UnearnedInterest periodInterest
            ////CR LOAN_PROD_UNEARNED_INTEREST_CONTROL periodInterest
            //journal.JournalEntries.Add(new JournalEntry
            //{
            //    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
            //    EntryType = TransactionEntryType.DEBIT,
            //    AccountId = LOAN_PROD_UNEARNED_INTEREST.Id,
            //    Account = LOAN_PROD_UNEARNED_INTEREST,
            //    Debit = interestOffset,
            //    TransactionDate = request.TransactionDate
            //});

            //journal.JournalEntries.Add(new JournalEntry
            //{
            //    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
            //    EntryType = TransactionEntryType.CREDIT,
            //    AccountId = LOAN_PROD_UNEARNED_INTEREST_CONTROL.Id,
            //    Account = LOAN_PROD_UNEARNED_INTEREST_CONTROL,
            //    Credit = interestOffset,
            //    TransactionDate = request.TransactionDate
            //});

            ////loan tracking:unearned interest Account
            ////DR Acc.UnearnedInterest periodInterest
            ////CR LOAN_ACC_UNEARNED_INTEREST_CONTROL periodInterest
            //journal.JournalEntries.Add(new JournalEntry
            //{
            //    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
            //    EntryType = TransactionEntryType.DEBIT,
            //    AccountId = LOAN_ACC_UNEARNED_INTEREST.Id,
            //    Account = LOAN_ACC_UNEARNED_INTEREST,
            //    Debit = interestOffset,
            //    TransactionDate = request.TransactionDate
            //});

            //journal.JournalEntries.Add(new JournalEntry
            //{
            //    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
            //    EntryType = TransactionEntryType.CREDIT,
            //    AccountId = LOAN_ACC_UNEARNED_INTEREST_CONTROL.Id,
            //    Account = LOAN_ACC_UNEARNED_INTEREST_CONTROL,
            //    Credit = interestOffset,
            //    TransactionDate = request.TransactionDate
            //});




            if (charges.Any())
            {
                // Get total charge amount
                foreach (var charge in charges)
                {
                    //track charges Product
                    //DR LOAN_PROD_CHARGE_ACCRUAL_CONTROL 1,000
                    //CR LoanProduct.ChargesAccrualAccount 1,000
                    journal.JournalEntries.Add(new JournalEntry
                    {
                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                        EntryType = TransactionEntryType.DEBIT,
                        AccountId = LOAN_PROD_CHARGE_ACCRUAL_CONTROL.Id,
                        Account = LOAN_PROD_CHARGE_ACCRUAL_CONTROL,
                        Debit = charge.TotalCharge,
                        TransactionDate = request.TransactionDate
                    });

                    journal.JournalEntries.Add(new JournalEntry
                    {
                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                        EntryType = TransactionEntryType.CREDIT,
                        AccountId = LOAN_PROD_CHARGES_ACCRUAL.Id,
                        Account = LOAN_PROD_CHARGES_ACCRUAL,
                        Credit = charge.TotalCharge,
                        TransactionDate = request.TransactionDate
                    });


                    //track charges Account
                    //DR LOAN_ACC_CHARGE_ACCRUAL_CONTROL 1,000
                    //CR LoanAccount.ChargesAccruedAccount 1,000

                    journal.JournalEntries.Add(new JournalEntry
                    {
                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                        EntryType = TransactionEntryType.DEBIT,
                        AccountId = LOAN_ACC_CHARGE_ACCRUAL_CONTROL.Id,
                        Account = LOAN_ACC_CHARGE_ACCRUAL_CONTROL,
                        Debit = charge.TotalCharge,
                        TransactionDate = request.TransactionDate
                    });

                    journal.JournalEntries.Add(new JournalEntry
                    {
                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                        EntryType = TransactionEntryType.CREDIT,
                        AccountId = LOAN_ACC_CHARGES_ACCRUAL.Id,
                        Account = LOAN_ACC_CHARGES_ACCRUAL,
                        Credit = charge.TotalCharge,
                        TransactionDate = request.TransactionDate
                    });
                }
            }



            logger.LogInformation("saving journal entries");
            dbContext.TransactionJournals.Add(journal);
            dbContext.SaveChanges();

            logger.LogInformation("posting journal entries");
            journal.Post();

            txn.TransactionJournalId = journal.Id;
            txn.IsProcessed = true;
            txn.ProcessedDate = request.TransactionDate;
            txn.Status = TransactionStatus.SUCCESS;

            logger.LogInformation("updating journal entries");

            dbContext.Update(journal);
            dbContext.SaveChanges();



            rsp.TransactionDate = request.TransactionDate;
            rsp.TransactionType = request.TransactionType;
            rsp.EntityId = request.EntityId;

            rsp.IsApproved = true;
            rsp.ApprovedOn = request.TransactionDate;
            rsp.IsPosted = true;
            rsp.PostedOn = request.TransactionDate;



        }
        catch (Exception ex)
        {
            logger.LogError("error processing journal entries");

            logger.LogError(ex, ex.Message);
            rsp.ErrorDate = DateTime.Now;
            rsp.ErrorDetails = ex.Message;
            rsp.ErrorFlag = true;

            //throw ex;

        }


        return rsp;


    }

    private LoanTransactionViewModel ProcessLoanOffsetTransfer(LoanTransactionCommand request, CancellationToken cancellationToken)
    {



        /*

		
LOAN OFFSET TRANSFER->PARTIAL
DR LOAN_CUST_BANK_CONTROL offsetAmount
CR Customer cash account offsetAmount

DR Customer cash account offsetAmount
CR LoanProduct.BankDepositAccount.LedgerAccount offsetAmount


//loan tracking:principal Product
DR LoanProduct.PrincipalAccount offsetAmount
CR LOAN_PROD_PRINCIPAL_CONTROL offsetAmount


//loan tracking:principal Account
DR OldLoanAccount.PrincipalBalanceAccount offsetAmount
CR LOAN_ACC_PRINCIPAL_CONTROL offsetAmount


//track charges Product
DR LOAN_PROD_CHARGE_ACCRUAL_CONTROL offsetCharge
CR LoanProduct.ChargesAccrualAccount offsetCharge


//track charges Account
DR LOAN_ACC_CHARGE_ACCRUAL_CONTROL offsetCharge
CR OldLoanAccount.ChargesAccruedAccount offsetCharge



LOAN OFFSET TRANSFER->FULL
DR LOAN_CUST_BANK_CONTROL offsetAmount
CR Customer cash account offsetAmount

DR Customer cash account offsetAmount
CR LoanProduct.BankDepositAccount.LedgerAccount offsetAmount


//loan tracking:principal Product
DR LoanProduct.PrincipalAccount offsetAmount
CR LOAN_PROD_PRINCIPAL_CONTROL offsetAmount


//loan tracking:principal Account
DR LoanAccount.PrincipalBalanceAccount offsetAmount
CR LOAN_ACC_PRINCIPAL_CONTROL offsetAmount


//track charges Product
DR LOAN_PROD_CHARGE_ACCRUAL_CONTROL offsetCharge
CR LoanProduct.ChargesAccrualAccount offsetCharge


//track charges Account
DR LOAN_ACC_CHARGE_ACCRUAL_CONTROL offsetCharge
CR LoanAccount.ChargesAccruedAccount offsetCharge


LOAN OFFSET TRANSFER->LIEU OF PAYROLL
DR LOAN_CUST_BANK_CONTROL offsetAmount
CR Customer cash account offsetAmount

DR Customer cash account offsetAmount
CR LoanProduct.BankDepositAccount.LedgerAccount offsetAmount


//loan tracking:principal Product
DR LoanProduct.PrincipalAccount offsetAmount
CR LOAN_PROD_PRINCIPAL_CONTROL offsetAmount


//loan tracking:principal Account
DR LoanAccount.PrincipalBalanceAccount offsetAmount
CR LOAN_ACC_PRINCIPAL_CONTROL offsetAmount


//track charges Product
DR LOAN_PROD_CHARGE_ACCRUAL_CONTROL offsetCharge
CR LoanProduct.ChargesAccrualAccount offsetCharge


//track charges Account
DR LOAN_ACC_CHARGE_ACCRUAL_CONTROL offsetCharge
CR LoanAccount.ChargesAccruedAccount offsetCharge




		*/




        LoanTransactionViewModel rsp = new LoanTransactionViewModel();


        try
        {

            TransactionJournal journal = new TransactionJournal();
            journal.TransactionNo = NHiloHelper.GetNextKey(nameof(TransactionJournal)).ToString();
            journal.Title = $"Loan Offset by Bank Transfer";
            journal.TransactionType = request.TransactionType;
            journal.TransactionDate = request.TransactionDate;
            journal.DocumentRef = nameof(LoanAccount);
            journal.DocumentRefId = request.LoanAccountId;
            journal.EntityRef = nameof(LoanOffset);
            journal.EntityRefId = request.EntityId;

            var txn = dbContext.LoanOffsets.Where(p => p.Id == request.EntityId)
                .Include(p => p.LoanAccount).ThenInclude(p => p.LoanApplication).ThenInclude(p => p.LoanProduct)
                .Include(p => p.CustomerBankAccount).ThenInclude(p => p.LedgerAccount)
                .Include(p => p.SpecialDepositAccount).ThenInclude(p => p.DepositAccount)
                .Include(p => p.SavingsAccount).ThenInclude(p => p.LedgerDepositAccount)
                .FirstOrDefault();

            var charges = dbContext.LoanOffSetCharges.Where(p => p.LoanOffsetId == request.EntityId);

            var loanAccount = dbContext.LoanAccounts.Where(r => r.Id == request.LoanAccountId)
                .Include(p => p.LoanApplication).ThenInclude(p => p.LoanProduct)
                .ThenInclude(p => p.BankDepositAccount).ThenInclude(p => p.LedgerAccount)
                .Include(c => c.Customer).ThenInclude(c => c.CashAccount)
                .FirstOrDefault();

            var _loanAccountQuery = dbContext.LoanAccounts.Where(r => r.Id == request.LoanAccountId);

            var _loanProductQuery = _loanAccountQuery.Include(x => x.LoanApplication)
                .ThenInclude(x => x.LoanProduct);



            LedgerAccount CUST_CASH_ACC = loanAccount.Customer.CashAccount;

            // loanAccount.PrincipalBalanceAccountId
            LedgerAccount LOAN_ACC_PRINCIPAL_BALANCE = _loanAccountQuery.Include(x => x.PrincipalBalanceAccount)
                .Select(c => c.PrincipalBalanceAccount).FirstOrDefault();

            // loanAccount.UnearnedInterestAccountId
            LedgerAccount LOAN_ACC_UNEARNED_INTEREST = _loanAccountQuery.Include(x => x.UnearnedInterestAccount)
                .Select(c => c.UnearnedInterestAccount).FirstOrDefault();

            // loanAccount.EarnedInterestAccountId
            LedgerAccount LOAN_ACC_EARNED_INTEREST = _loanAccountQuery.Include(x => x.EarnedInterestAccount)
                .Select(c => c.EarnedInterestAccount).FirstOrDefault();

            // loanAccount.ChargesAccruedAccountId
            LedgerAccount LOAN_ACC_CHARGES_ACCRUAL = _loanAccountQuery.Include(x => x.ChargesAccruedAccount)
                .Select(c => c.ChargesAccruedAccount).FirstOrDefault();


            // loanAccount.LoanApplication.LoanProduct.PrincipalAccount.Id
            LedgerAccount LOAN_PROD_PRINCIPAL = _loanProductQuery.ThenInclude(x => x.PrincipalAccount)
                .Select(c => c.LoanApplication.LoanProduct.PrincipalAccount)
                .FirstOrDefault();


            // "loanAccount.LoanApplication.LoanProduct.UnearnedInterestAccountId"
            LedgerAccount LOAN_PROD_UNEARNED_INTEREST = _loanProductQuery.ThenInclude(x => x.UnearnedInterestAccount)
                .Select(c => c.LoanApplication.LoanProduct.UnearnedInterestAccount)
                .FirstOrDefault();

            LedgerAccount LOAN_PROD_EARNED_INTEREST = _loanProductQuery.ThenInclude(x => x.InterestIncomeAccount)
               .Select(c => c.LoanApplication.LoanProduct.InterestIncomeAccount)
               .FirstOrDefault();

            // loanAccount.LoanApplication.LoanProduct.ChargesAccrualAccountId
            LedgerAccount LOAN_PROD_CHARGES_ACCRUAL = _loanProductQuery.ThenInclude(x => x.ChargesAccrualAccount)
                .Select(c => c.LoanApplication.LoanProduct.ChargesAccrualAccount)
                .FirstOrDefault();





            LedgerAccount LOAN_PROD_PRINCIPAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PROD_PRINCIPAL_CONTROL.ToString());

            LedgerAccount LOAN_ACC_PRINCIPAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_ACC_PRINCIPAL_CONTROL.ToString());

            LedgerAccount LOAN_PROD_UNEARNED_INTEREST_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PROD_UNEARNED_INTEREST_CONTROL.ToString());


            LedgerAccount LOAN_PROD_EARNED_INTEREST_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PROD_EARNED_INTEREST_CONTROL.ToString());



            LedgerAccount LOAN_ACC_UNEARNED_INTEREST_CONTROL =
               dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_ACC_UNEARNED_INTEREST_CONTROL.ToString());


            LedgerAccount LOAN_ACC_EARNED_INTEREST_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_ACC_EARNED_INTEREST_CONTROL.ToString());



            LedgerAccount LOAN_PROD_CHARGE_ACCRUAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PROD_CHARGE_ACCRUAL_CONTROL.ToString());

            LedgerAccount LOAN_ACC_CHARGE_ACCRUAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_ACC_CHARGE_ACCRUAL_CONTROL.ToString());


            LedgerAccount LOAN_CUST_BANK_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_CUST_BANK_CONTROL.ToString());


            var loanhelper = new LoanHelper(loanAccount.Principal, loanAccount.LoanApplication.LoanProduct.InterestRate,
                loanAccount.LoanApplication.LoanProduct.InterestMethod,
                loanAccount.LoanApplication.LoanProduct.InterestCalculationMethod,
                loanAccount.TenureUnit, loanAccount.TenureValue,
                loanAccount.LoanApplication.LoanProduct.RepaymentPeriod,
                loanAccount.LoanApplication.LoanProduct.DaysInYear,
                loanAccount.RepaymentCommencementDate.UtcDateTime);

            var paymentSchedules = loanhelper.GetAmortizationTable(loanAccount.LoanApplication.LoanProduct.InterestCalculationMethod);
            var firstPayment = paymentSchedules.FirstOrDefault();


            decimal offsetAmount = txn.OffsetAmount;
            decimal principalOffset = txn.OffsetAmount;
            decimal interestOffset = 0;



            //DR LOAN_CUST_BANK_CONTROL offsetAmount
            //CR Customer cash account offsetAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = LOAN_CUST_BANK_CONTROL.Id,
                Account = LOAN_CUST_BANK_CONTROL,
                Debit = txn.OffsetAmount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = CUST_CASH_ACC.Id,
                Account = CUST_CASH_ACC,
                Credit = txn.OffsetAmount,
                TransactionDate = request.TransactionDate
            });

            //DR Customer cash account offsetAmount
            //CR LoanProduct.BankDepositAccount.LedgerAccount offsetAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = CUST_CASH_ACC.Id,
                Account = CUST_CASH_ACC,
                Debit = txn.OffsetAmount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = loanAccount.LoanApplication.LoanProduct.BankDepositAccount.LedgerAccountId,
                Account = loanAccount.LoanApplication.LoanProduct.BankDepositAccount.LedgerAccount,
                Credit = txn.OffsetAmount,
                TransactionDate = request.TransactionDate
            });




            //loan tracking:principal Product
            //DR LoanProduct.PrincipalAccount offsetAmount
            //CR LOAN_PROD_PRINCIPAL_CONTROL offsetAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = LOAN_PROD_PRINCIPAL.Id,
                Account = LOAN_PROD_PRINCIPAL,
                Debit = txn.OffsetAmount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = LOAN_PROD_PRINCIPAL_CONTROL.Id,
                Account = LOAN_PROD_PRINCIPAL_CONTROL,
                Credit = txn.OffsetAmount,
                TransactionDate = request.TransactionDate
            });

            //loan tracking:principal Account
            //DR OldLoanAccount.PrincipalBalanceAccount offsetAmount
            //CR LOAN_ACC_PRINCIPAL_CONTROL offsetAmount

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = LOAN_ACC_PRINCIPAL_BALANCE.Id,
                Account = LOAN_ACC_PRINCIPAL_BALANCE,
                Debit = txn.OffsetAmount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = LOAN_ACC_PRINCIPAL_CONTROL.Id,
                Account = LOAN_ACC_PRINCIPAL_CONTROL,
                Credit = txn.OffsetAmount,
                TransactionDate = request.TransactionDate
            });


            switch (txn.AllowedOffsetType)
            {
                case AllowedOffsetType.PARTIAL:
                    {

                        loanAccount.ParentAccount.IsClosed = true;
                        loanAccount.ParentAccount.DateClosed = DateTime.Now;

                        break;
                    }

                case AllowedOffsetType.FULL:
                    {
                        loanAccount.IsClosed = true;
                        loanAccount.DateClosed = DateTime.Now;
                        break;
                    }

                case AllowedOffsetType.IN_LIEU_OF_PAYROLL:
                    {
                        LoanRepaymentSchedule loanRepaymentSchedule;


                        var scheduleId = txn.RepaymentSchedules.FirstOrDefault();
                        if (scheduleId != null)
                        {
                            loanRepaymentSchedule = dbContext.LoanRepaymentSchedules.FirstOrDefault(p => p.Id == scheduleId);
                            if (loanRepaymentSchedule != null)
                            {
                                principalOffset = loanRepaymentSchedule.PeriodPrincipal;
                                interestOffset = loanRepaymentSchedule.PeriodInterest;

                                //Reduce unearned interest balance

                                //loan tracking:unearned interest Product
                                //DR Prod.UnearnedInterest periodInterest
                                //CR LOAN_PROD_UNEARNED_INTEREST_CONTROL periodInterest
                                journal.JournalEntries.Add(new JournalEntry
                                {
                                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                    EntryType = TransactionEntryType.DEBIT,
                                    AccountId = LOAN_PROD_UNEARNED_INTEREST.Id,
                                    Account = LOAN_PROD_UNEARNED_INTEREST,
                                    Debit = interestOffset,
                                    TransactionDate = request.TransactionDate
                                });

                                journal.JournalEntries.Add(new JournalEntry
                                {
                                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                    EntryType = TransactionEntryType.CREDIT,
                                    AccountId = LOAN_PROD_UNEARNED_INTEREST_CONTROL.Id,
                                    Account = LOAN_PROD_UNEARNED_INTEREST_CONTROL,
                                    Credit = interestOffset,
                                    TransactionDate = request.TransactionDate
                                });

                                //loan tracking:unearned interest Account
                                //DR Acc.UnearnedInterest periodInterest
                                //CR LOAN_ACC_UNEARNED_INTEREST_CONTROL periodInterest
                                journal.JournalEntries.Add(new JournalEntry
                                {
                                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                    EntryType = TransactionEntryType.DEBIT,
                                    AccountId = LOAN_ACC_UNEARNED_INTEREST.Id,
                                    Account = LOAN_ACC_UNEARNED_INTEREST,
                                    Debit = interestOffset,
                                    TransactionDate = request.TransactionDate
                                });

                                journal.JournalEntries.Add(new JournalEntry
                                {
                                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                    EntryType = TransactionEntryType.CREDIT,
                                    AccountId = LOAN_ACC_UNEARNED_INTEREST_CONTROL.Id,
                                    Account = LOAN_ACC_UNEARNED_INTEREST_CONTROL,
                                    Credit = interestOffset,
                                    TransactionDate = request.TransactionDate
                                });


                                //Increase earned interest balance

                                //loan tracking:interest income Product
                                //DR LOAN_PROD_EARNED_INTEREST_CONTROL periodInterest
                                //CR Prod.EarnedInterest periodInterest
                                journal.JournalEntries.Add(new JournalEntry
                                {
                                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                    EntryType = TransactionEntryType.DEBIT,
                                    AccountId = LOAN_PROD_EARNED_INTEREST_CONTROL.Id,
                                    Account = LOAN_PROD_EARNED_INTEREST_CONTROL,
                                    Debit = interestOffset,
                                    TransactionDate = request.TransactionDate
                                });

                                journal.JournalEntries.Add(new JournalEntry
                                {
                                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                    EntryType = TransactionEntryType.CREDIT,
                                    AccountId = LOAN_PROD_EARNED_INTEREST.Id,
                                    Account = LOAN_PROD_EARNED_INTEREST,
                                    Credit = interestOffset,
                                    TransactionDate = request.TransactionDate
                                });

                                //loan tracking:interest income Account
                                //DR LOAN_ACC_EARNED_INTEREST_CONTROL periodInterest
                                //CR Acc.EarnedInterest periodInterest
                                journal.JournalEntries.Add(new JournalEntry
                                {
                                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                    EntryType = TransactionEntryType.DEBIT,
                                    AccountId = LOAN_ACC_EARNED_INTEREST_CONTROL.Id,
                                    Account = LOAN_ACC_EARNED_INTEREST_CONTROL,
                                    Debit = interestOffset,
                                    TransactionDate = request.TransactionDate
                                });

                                journal.JournalEntries.Add(new JournalEntry
                                {
                                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                    EntryType = TransactionEntryType.CREDIT,
                                    AccountId = LOAN_ACC_EARNED_INTEREST.Id,
                                    Account = LOAN_ACC_EARNED_INTEREST,
                                    Credit = interestOffset,
                                    TransactionDate = request.TransactionDate
                                });
                            }
                        }


                        var unPaidSchedules = dbContext.LoanRepaymentSchedules.Any(x => x.LoanAccountId == txn.LoanAccountId && !x.IsPaid);
                        if (!unPaidSchedules)
                        {
                            loanAccount.IsClosed = true;
                            loanAccount.DateClosed = DateTime.Now;
                        }

                        break;


                    }
            }




            if (charges.Any())
            {
                // Get total charge amount
                foreach (var charge in charges)
                {
                    //track charges Product
                    //DR LOAN_PROD_CHARGE_ACCRUAL_CONTROL 1,000
                    //CR LoanProduct.ChargesAccrualAccount 1,000
                    journal.JournalEntries.Add(new JournalEntry
                    {
                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                        EntryType = TransactionEntryType.DEBIT,
                        AccountId = LOAN_PROD_CHARGE_ACCRUAL_CONTROL.Id,
                        Account = LOAN_PROD_CHARGE_ACCRUAL_CONTROL,
                        Debit = charge.TotalCharge,
                        TransactionDate = request.TransactionDate
                    });

                    journal.JournalEntries.Add(new JournalEntry
                    {
                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                        EntryType = TransactionEntryType.CREDIT,
                        AccountId = LOAN_PROD_CHARGES_ACCRUAL.Id,
                        Account = LOAN_PROD_CHARGES_ACCRUAL,
                        Credit = charge.TotalCharge,
                        TransactionDate = request.TransactionDate
                    });


                    //track charges Account
                    //DR LOAN_ACC_CHARGE_ACCRUAL_CONTROL 1,000
                    //CR LoanAccount.ChargesAccruedAccount 1,000

                    journal.JournalEntries.Add(new JournalEntry
                    {
                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                        EntryType = TransactionEntryType.DEBIT,
                        AccountId = LOAN_ACC_CHARGE_ACCRUAL_CONTROL.Id,
                        Account = LOAN_ACC_CHARGE_ACCRUAL_CONTROL,
                        Debit = charge.TotalCharge,
                        TransactionDate = request.TransactionDate
                    });

                    journal.JournalEntries.Add(new JournalEntry
                    {
                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                        EntryType = TransactionEntryType.CREDIT,
                        AccountId = LOAN_ACC_CHARGES_ACCRUAL.Id,
                        Account = LOAN_ACC_CHARGES_ACCRUAL,
                        Credit = charge.TotalCharge,
                        TransactionDate = request.TransactionDate
                    });
                }
            }



            dbContext.TransactionJournals.Add(journal);
            dbContext.SaveChanges();

            journal.Post();

            txn.TransactionJournalId = journal.Id;
            txn.IsProcessed = true;
            txn.ProcessedDate = request.TransactionDate;
            txn.Status = TransactionStatus.SUCCESS;


            dbContext.Update(journal);
            dbContext.SaveChanges();


            rsp.TransactionDate = request.TransactionDate;
            rsp.TransactionType = request.TransactionType;
            rsp.EntityId = request.EntityId;

            rsp.IsApproved = true;
            rsp.ApprovedOn = request.TransactionDate;
            rsp.IsPosted = true;
            rsp.PostedOn = request.TransactionDate;



        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            rsp.ErrorDate = DateTime.Now;
            rsp.ErrorDetails = ex.Message;
            rsp.ErrorFlag = true;

            //throw ex;

        }


        return rsp;


    }

    private LoanTransactionViewModel ProcessLoanOffsetSavings(LoanTransactionCommand request, CancellationToken cancellationToken)
    {

        /*

		


LOAN OFFSET SAVINGS->PARTIAL

//bank reconciliation : 
DR OldLoanAccount.Customer.SavingsAccount.DepositAccount.LedgerAccount offsetAmount
CR Product.BankDepositAccount.LedgerAccount offsetAmount


//loan tracking:principal Product
DR LoanProduct.PrincipalAccount offsetAmount
CR LOAN_PROD_PRINCIPAL_CONTROL offsetAmount


//loan tracking:principal Account
DR OldLoanAccount.PrincipalBalanceAccount offsetAmount
CR LOAN_ACC_PRINCIPAL_CONTROL offsetAmount


//track charges Product
DR LOAN_PROD_CHARGE_ACCRUAL_CONTROL offsetCharge
CR LoanProduct.ChargesAccrualAccount offsetCharge


//track charges Account
DR LOAN_ACC_CHARGE_ACCRUAL_CONTROL offsetCharge
CR OldLoanAccount.ChargesAccruedAccount offsetCharge



LOAN OFFSET SAVINGS->FULL

//bank reconciliation : 
DR LoanAccount.Customer.SavingsAccount.DepositAccount.LedgerAccount offsetAmount
CR Product.BankDepositAccount.LedgerAccount offsetAmount


//loan tracking:principal Product
DR LoanProduct.PrincipalAccount offsetAmount
CR LOAN_PROD_PRINCIPAL_CONTROL offsetAmount


//loan tracking:principal Account
DR LoanAccount.PrincipalBalanceAccount offsetAmount
CR LOAN_ACC_PRINCIPAL_CONTROL offsetAmount


//track charges Product
DR LOAN_PROD_CHARGE_ACCRUAL_CONTROL offsetCharge
CR LoanProduct.ChargesAccrualAccount offsetCharge


//track charges Account
DR LOAN_ACC_CHARGE_ACCRUAL_CONTROL offsetCharge
CR LoanAccount.ChargesAccruedAccount offsetCharge





LOAN OFFSET SAVINGS->LIEU OF PAYROLL
//bank reconciliation : 
DR LoanAccount.Customer.SavingsAccount.DepositAccount.LedgerAccount offsetAmount
CR Product.BankDepositAccount.LedgerAccount offsetAmount


//loan tracking:principal Product
DR LoanProduct.PrincipalAccount offsetAmount
CR LOAN_PROD_PRINCIPAL_CONTROL offsetAmount


//loan tracking:principal Account
DR LoanAccount.PrincipalBalanceAccount offsetAmount
CR LOAN_ACC_PRINCIPAL_CONTROL offsetAmount


//track charges Product
DR LOAN_PROD_CHARGE_ACCRUAL_CONTROL offsetCharge
CR LoanProduct.ChargesAccrualAccount offsetCharge


//track charges Account
DR LOAN_ACC_CHARGE_ACCRUAL_CONTROL offsetCharge
CR LoanAccount.ChargesAccruedAccount offsetCharge



//loan tracking:unearned interest Product
DR Prod.UnearnedInterest periodInterest
CR LOAN_PROD_UNEARNED_INTEREST_CONTROL periodInterest


//loan tracking:unearned interest Account
DR Acc.UnearnedInterest periodInterest
CR LOAN_ACC_UNEARNED_INTEREST_CONTROL periodInterest


//loan tracking:interest income Product
DR LOAN_PROD_EARNED_INTEREST_CONTROL periodInterest
CR Prod.EarnedInterest periodInterest


//loan tracking:interest income Account
DR LOAN_ACC_EARNED_INTEREST_CONTROL periodInterest
CR Acc.EarnedInterest periodInterest





		*/




        LoanTransactionViewModel rsp = new LoanTransactionViewModel();


        try
        {

            TransactionJournal journal = new TransactionJournal();
            journal.TransactionNo = NHiloHelper.GetNextKey(nameof(TransactionJournal)).ToString();
            journal.Title = $"Loan Offset by Savings";
            journal.TransactionType = request.TransactionType;
            journal.TransactionDate = request.TransactionDate;
            journal.DocumentRef = nameof(LoanAccount);
            journal.DocumentRefId = request.LoanAccountId;
            journal.EntityRef = nameof(LoanOffset);
            journal.EntityRefId = request.EntityId;

            var txn = dbContext.LoanOffsets.Where(p => p.Id == request.EntityId)
                .Include(p => p.LoanAccount).ThenInclude(p => p.LoanApplication).ThenInclude(p => p.LoanProduct)
                .Include(p => p.CustomerBankAccount).ThenInclude(p => p.LedgerAccount)
                //.Include(p => p.SpecialDepositAccount).ThenInclude(p => p.DepositAccount)
                .Include(p => p.SavingsAccount).ThenInclude(p => p.LedgerDepositAccount)
                .Include(p => p.SavingsAccount).ThenInclude(p => p.DepositProduct).ThenInclude(p => p.ProductDepositAccount)
                .FirstOrDefault();

            var charges = dbContext.LoanOffSetCharges.Where(p => p.LoanOffsetId == request.EntityId);

            var loanAccount = dbContext.LoanAccounts.Where(r => r.Id == request.LoanAccountId)
                .Include(p => p.LoanApplication).ThenInclude(p => p.LoanProduct)
                .ThenInclude(p => p.BankDepositAccount).ThenInclude(p => p.LedgerAccount)
                .Include(c => c.Customer).ThenInclude(c => c.CashAccount)
                .Include(c => c.ParentAccount)
                .FirstOrDefault();

            var _loanAccountQuery = dbContext.LoanAccounts.Where(r => r.Id == request.LoanAccountId);

            var _loanProductQuery = _loanAccountQuery.Include(x => x.LoanApplication)
                .ThenInclude(x => x.LoanProduct);



            LedgerAccount CUST_CASH_ACC = loanAccount.Customer.CashAccount;

            // loanAccount.PrincipalBalanceAccountId
            LedgerAccount LOAN_ACC_PRINCIPAL_BALANCE = _loanAccountQuery.Include(x => x.PrincipalBalanceAccount)
                .Select(c => c.PrincipalBalanceAccount).FirstOrDefault();

            // loanAccount.UnearnedInterestAccountId
            LedgerAccount LOAN_ACC_UNEARNED_INTEREST = _loanAccountQuery.Include(x => x.UnearnedInterestAccount)
                .Select(c => c.UnearnedInterestAccount).FirstOrDefault();

            // loanAccount.EarnedInterestAccountId
            LedgerAccount LOAN_ACC_EARNED_INTEREST = _loanAccountQuery.Include(x => x.EarnedInterestAccount)
                .Select(c => c.EarnedInterestAccount).FirstOrDefault();

            // loanAccount.ChargesAccruedAccountId
            LedgerAccount LOAN_ACC_CHARGES_ACCRUAL = _loanAccountQuery.Include(x => x.ChargesAccruedAccount)
                .Select(c => c.ChargesAccruedAccount).FirstOrDefault();


            // loanAccount.LoanApplication.LoanProduct.PrincipalAccount.Id
            LedgerAccount LOAN_PROD_PRINCIPAL = _loanProductQuery.ThenInclude(x => x.PrincipalAccount)
                .Select(c => c.LoanApplication.LoanProduct.PrincipalAccount)
                .FirstOrDefault();


            // "loanAccount.LoanApplication.LoanProduct.UnearnedInterestAccountId"
            LedgerAccount LOAN_PROD_UNEARNED_INTEREST = _loanProductQuery.ThenInclude(x => x.UnearnedInterestAccount)
                .Select(c => c.LoanApplication.LoanProduct.UnearnedInterestAccount)
                .FirstOrDefault();

            LedgerAccount LOAN_PROD_EARNED_INTEREST = _loanProductQuery.ThenInclude(x => x.InterestIncomeAccount)
               .Select(c => c.LoanApplication.LoanProduct.InterestIncomeAccount)
               .FirstOrDefault();

            // loanAccount.LoanApplication.LoanProduct.ChargesAccrualAccountId
            LedgerAccount LOAN_PROD_CHARGES_ACCRUAL = _loanProductQuery.ThenInclude(x => x.ChargesAccrualAccount)
                .Select(c => c.LoanApplication.LoanProduct.ChargesAccrualAccount)
                .FirstOrDefault();





            LedgerAccount LOAN_PROD_PRINCIPAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PROD_PRINCIPAL_CONTROL.ToString());

            LedgerAccount LOAN_ACC_PRINCIPAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_ACC_PRINCIPAL_CONTROL.ToString());

            LedgerAccount LOAN_PROD_UNEARNED_INTEREST_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PROD_UNEARNED_INTEREST_CONTROL.ToString());


            LedgerAccount LOAN_PROD_EARNED_INTEREST_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PROD_EARNED_INTEREST_CONTROL.ToString());



            LedgerAccount LOAN_ACC_UNEARNED_INTEREST_CONTROL =
               dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_ACC_UNEARNED_INTEREST_CONTROL.ToString());


            LedgerAccount LOAN_ACC_EARNED_INTEREST_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_ACC_EARNED_INTEREST_CONTROL.ToString());



            LedgerAccount LOAN_PROD_CHARGE_ACCRUAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PROD_CHARGE_ACCRUAL_CONTROL.ToString());
            LedgerAccount LOAN_ACC_CHARGE_ACCRUAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_ACC_CHARGE_ACCRUAL_CONTROL.ToString());


            LedgerAccount LOAN_CUST_BANK_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_CUST_BANK_CONTROL.ToString());

            LedgerAccount SAVINGS_PROD_CONTROL =
               dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SAVINGS_PROD_CONTROL.ToString());


            var loanhelper = new LoanHelper(loanAccount.Principal, loanAccount.LoanApplication.LoanProduct.InterestRate,
                loanAccount.LoanApplication.LoanProduct.InterestMethod,
                loanAccount.LoanApplication.LoanProduct.InterestCalculationMethod,
                loanAccount.TenureUnit, loanAccount.TenureValue,
                loanAccount.LoanApplication.LoanProduct.RepaymentPeriod,
                loanAccount.LoanApplication.LoanProduct.DaysInYear,
                loanAccount.RepaymentCommencementDate.UtcDateTime);

            var paymentSchedules = loanhelper.GetAmortizationTable(loanAccount.LoanApplication.LoanProduct.InterestCalculationMethod);
            var firstPayment = paymentSchedules.FirstOrDefault();


            decimal offsetAmount = txn.OffsetAmount;
            decimal principalOffset = txn.OffsetAmount;
            decimal interestOffset = 0;



            //DR Product deposit account offsetAmount
            //CR SAVINGS_PROD_CONTROL offsetAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = txn.SavingsAccount.DepositProduct.ProductDepositAccount.Id,
                Account = txn.SavingsAccount.DepositProduct.ProductDepositAccount,
                Debit = txn.OffsetAmount,
                TransactionDate = request.TransactionDate
            });
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = SAVINGS_PROD_CONTROL.Id,
                Account = SAVINGS_PROD_CONTROL,
                Credit = txn.OffsetAmount,
                TransactionDate = request.TransactionDate
            });


            //DR LoanAccount.Customer.SavingsAccount.DepositAccount.LedgerAccount offsetAmount
            //CR Product.BankDepositAccount.LedgerAccount offsetAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = txn.SavingsAccount.LedgerDepositAccount.Id,
                Account = txn.SavingsAccount.LedgerDepositAccount,
                Debit = txn.OffsetAmount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = loanAccount.LoanApplication.LoanProduct.BankDepositAccount.LedgerAccountId,
                Account = loanAccount.LoanApplication.LoanProduct.BankDepositAccount.LedgerAccount,
                Credit = txn.OffsetAmount,
                TransactionDate = request.TransactionDate
            });




            //loan tracking:principal Product
            //DR LoanProduct.PrincipalAccount offsetAmount
            //CR LOAN_PROD_PRINCIPAL_CONTROL offsetAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = LOAN_PROD_PRINCIPAL.Id,
                Account = LOAN_PROD_PRINCIPAL,
                Debit = txn.OffsetAmount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = LOAN_PROD_PRINCIPAL_CONTROL.Id,
                Account = LOAN_PROD_PRINCIPAL_CONTROL,
                Credit = txn.OffsetAmount,
                TransactionDate = request.TransactionDate
            });

            //loan tracking:principal Account
            //DR OldLoanAccount.PrincipalBalanceAccount offsetAmount
            //CR LOAN_ACC_PRINCIPAL_CONTROL offsetAmount

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = LOAN_ACC_PRINCIPAL_BALANCE.Id,
                Account = LOAN_ACC_PRINCIPAL_BALANCE,
                Debit = txn.OffsetAmount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = LOAN_ACC_PRINCIPAL_CONTROL.Id,
                Account = LOAN_ACC_PRINCIPAL_CONTROL,
                Credit = txn.OffsetAmount,
                TransactionDate = request.TransactionDate
            });


            switch (txn.AllowedOffsetType)
            {
                case AllowedOffsetType.PARTIAL:
                    {

                        loanAccount.ParentAccount.IsClosed = true;
                        loanAccount.ParentAccount.DateClosed = DateTime.Now;

                        break;
                    }

                case AllowedOffsetType.FULL:
                    {
                        loanAccount.IsClosed = true;
                        loanAccount.DateClosed = DateTime.Now;
                        break;
                    }

                case AllowedOffsetType.IN_LIEU_OF_PAYROLL:
                    {
                        LoanRepaymentSchedule loanRepaymentSchedule;


                        var scheduleId = txn.RepaymentSchedules.FirstOrDefault();
                        if (scheduleId != null)
                        {
                            loanRepaymentSchedule = dbContext.LoanRepaymentSchedules.FirstOrDefault(p => p.Id == scheduleId);
                            if (loanRepaymentSchedule != null)
                            {
                                principalOffset = loanRepaymentSchedule.PeriodPrincipal;
                                interestOffset = loanRepaymentSchedule.PeriodInterest;

                                //Reduce unearned interest balance

                                //loan tracking:unearned interest Product
                                //DR Prod.UnearnedInterest periodInterest
                                //CR LOAN_PROD_UNEARNED_INTEREST_CONTROL periodInterest
                                journal.JournalEntries.Add(new JournalEntry
                                {
                                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                    EntryType = TransactionEntryType.DEBIT,
                                    AccountId = LOAN_PROD_UNEARNED_INTEREST.Id,
                                    Account = LOAN_PROD_UNEARNED_INTEREST,
                                    Debit = interestOffset,
                                    TransactionDate = request.TransactionDate
                                });

                                journal.JournalEntries.Add(new JournalEntry
                                {
                                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                    EntryType = TransactionEntryType.CREDIT,
                                    AccountId = LOAN_PROD_UNEARNED_INTEREST_CONTROL.Id,
                                    Account = LOAN_PROD_UNEARNED_INTEREST_CONTROL,
                                    Credit = interestOffset,
                                    TransactionDate = request.TransactionDate
                                });

                                //loan tracking:unearned interest Account
                                //DR Acc.UnearnedInterest periodInterest
                                //CR LOAN_ACC_UNEARNED_INTEREST_CONTROL periodInterest
                                journal.JournalEntries.Add(new JournalEntry
                                {
                                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                    EntryType = TransactionEntryType.DEBIT,
                                    AccountId = LOAN_ACC_UNEARNED_INTEREST.Id,
                                    Account = LOAN_ACC_UNEARNED_INTEREST,
                                    Debit = interestOffset,
                                    TransactionDate = request.TransactionDate
                                });

                                journal.JournalEntries.Add(new JournalEntry
                                {
                                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                    EntryType = TransactionEntryType.CREDIT,
                                    AccountId = LOAN_ACC_UNEARNED_INTEREST_CONTROL.Id,
                                    Account = LOAN_ACC_UNEARNED_INTEREST_CONTROL,
                                    Credit = interestOffset,
                                    TransactionDate = request.TransactionDate
                                });


                                //Increase earned interest balance

                                //loan tracking:interest income Product
                                //DR LOAN_PROD_EARNED_INTEREST_CONTROL periodInterest
                                //CR Prod.EarnedInterest periodInterest
                                journal.JournalEntries.Add(new JournalEntry
                                {
                                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                    EntryType = TransactionEntryType.DEBIT,
                                    AccountId = LOAN_PROD_EARNED_INTEREST_CONTROL.Id,
                                    Account = LOAN_PROD_EARNED_INTEREST_CONTROL,
                                    Debit = interestOffset,
                                    TransactionDate = request.TransactionDate
                                });

                                journal.JournalEntries.Add(new JournalEntry
                                {
                                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                    EntryType = TransactionEntryType.CREDIT,
                                    AccountId = LOAN_PROD_EARNED_INTEREST.Id,
                                    Account = LOAN_PROD_EARNED_INTEREST,
                                    Credit = interestOffset,
                                    TransactionDate = request.TransactionDate
                                });

                                //loan tracking:interest income Account
                                //DR LOAN_ACC_EARNED_INTEREST_CONTROL periodInterest
                                //CR Acc.EarnedInterest periodInterest
                                journal.JournalEntries.Add(new JournalEntry
                                {
                                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                    EntryType = TransactionEntryType.DEBIT,
                                    AccountId = LOAN_ACC_EARNED_INTEREST_CONTROL.Id,
                                    Account = LOAN_ACC_EARNED_INTEREST_CONTROL,
                                    Debit = interestOffset,
                                    TransactionDate = request.TransactionDate
                                });

                                journal.JournalEntries.Add(new JournalEntry
                                {
                                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                    EntryType = TransactionEntryType.CREDIT,
                                    AccountId = LOAN_ACC_EARNED_INTEREST.Id,
                                    Account = LOAN_ACC_EARNED_INTEREST,
                                    Credit = interestOffset,
                                    TransactionDate = request.TransactionDate
                                });
                            }
                        }


                        var unPaidSchedules = dbContext.LoanRepaymentSchedules.Any(x => x.LoanAccountId == txn.LoanAccountId && !x.IsPaid);
                        if (!unPaidSchedules)
                        {
                            loanAccount.IsClosed = true;
                            loanAccount.DateClosed = DateTime.Now;
                        }

                        break;


                    }
            }




            if (charges.Any())
            {
                // Get total charge amount
                foreach (var charge in charges)
                {
                    //track charges Product
                    //DR LOAN_PROD_CHARGE_ACCRUAL_CONTROL 1,000
                    //CR LoanProduct.ChargesAccrualAccount 1,000
                    journal.JournalEntries.Add(new JournalEntry
                    {
                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                        EntryType = TransactionEntryType.DEBIT,
                        AccountId = LOAN_PROD_CHARGE_ACCRUAL_CONTROL.Id,
                        Account = LOAN_PROD_CHARGE_ACCRUAL_CONTROL,
                        Debit = charge.TotalCharge,
                        TransactionDate = request.TransactionDate
                    });

                    journal.JournalEntries.Add(new JournalEntry
                    {
                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                        EntryType = TransactionEntryType.CREDIT,
                        AccountId = LOAN_PROD_CHARGES_ACCRUAL.Id,
                        Account = LOAN_PROD_CHARGES_ACCRUAL,
                        Credit = charge.TotalCharge,
                        TransactionDate = request.TransactionDate
                    });


                    //track charges Account
                    //DR LOAN_ACC_CHARGE_ACCRUAL_CONTROL 1,000
                    //CR LoanAccount.ChargesAccruedAccount 1,000

                    journal.JournalEntries.Add(new JournalEntry
                    {
                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                        EntryType = TransactionEntryType.DEBIT,
                        AccountId = LOAN_ACC_CHARGE_ACCRUAL_CONTROL.Id,
                        Account = LOAN_ACC_CHARGE_ACCRUAL_CONTROL,
                        Debit = charge.TotalCharge,
                        TransactionDate = request.TransactionDate
                    });

                    journal.JournalEntries.Add(new JournalEntry
                    {
                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                        EntryType = TransactionEntryType.CREDIT,
                        AccountId = LOAN_ACC_CHARGES_ACCRUAL.Id,
                        Account = LOAN_ACC_CHARGES_ACCRUAL,
                        Credit = charge.TotalCharge,
                        TransactionDate = request.TransactionDate
                    });
                }
            }



            dbContext.TransactionJournals.Add(journal);
            dbContext.SaveChanges();

            journal.Post();

            txn.TransactionJournalId = journal.Id;
            txn.IsProcessed = true;
            txn.ProcessedDate = request.TransactionDate;
            txn.Status = TransactionStatus.SUCCESS;




            dbContext.Update(journal);
            dbContext.SaveChanges();



            rsp.TransactionDate = request.TransactionDate;
            rsp.TransactionType = request.TransactionType;
            rsp.EntityId = request.EntityId;

            rsp.IsApproved = true;
            rsp.ApprovedOn = request.TransactionDate;
            rsp.IsPosted = true;
            rsp.PostedOn = request.TransactionDate;



        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            rsp.ErrorDate = DateTime.Now;
            rsp.ErrorDetails = ex.Message;
            rsp.ErrorFlag = true;

            //throw ex;

        }


        return rsp;

    }

    private LoanTransactionViewModel ProcessLoanOffsetSpecialDeposit(LoanTransactionCommand request, CancellationToken cancellationToken)
    {
        /*

		




LOAN OFFSET SD->PARTIAL
DR OldLoanAccount.Customer.SDAccount.DepositAccount.LedgerAccount offsetAmount
CR Product.BankDepositAccount.LedgerAccount offsetAmount


//loan tracking:principal Product
DR LoanProduct.PrincipalAccount offsetAmount
CR LOAN_PROD_PRINCIPAL_CONTROL offsetAmount


//loan tracking:principal Account
DR OldLoanAccount.PrincipalBalanceAccount offsetAmount
CR LOAN_ACC_PRINCIPAL_CONTROL offsetAmount


//track charges Product
DR LOAN_PROD_CHARGE_ACCRUAL_CONTROL offsetCharge
CR LoanProduct.ChargesAccrualAccount offsetCharge


//track charges Account
DR LOAN_ACC_CHARGE_ACCRUAL_CONTROL offsetCharge
CR OldLoanAccount.ChargesAccruedAccount offsetCharge



LOAN OFFSET SD->FULL
//bank reconciliation : 
DR LoanAccount.Customer.SDAccount.DepositAccount.LedgerAccount offsetAmount
CR Product.BankDepositAccount.LedgerAccount offsetAmount


//loan tracking:principal Product
DR LoanProduct.PrincipalAccount offsetAmount
CR LOAN_PROD_PRINCIPAL_CONTROL offsetAmount


//loan tracking:principal Account
DR LoanAccount.PrincipalBalanceAccount offsetAmount
CR LOAN_ACC_PRINCIPAL_CONTROL offsetAmount


//track charges Product
DR LOAN_PROD_CHARGE_ACCRUAL_CONTROL offsetCharge
CR LoanProduct.ChargesAccrualAccount offsetCharge


//track charges Account
DR LOAN_ACC_CHARGE_ACCRUAL_CONTROL offsetCharge
CR LoanAccount.ChargesAccruedAccount offsetCharge



LOAN OFFSET SD->LIEU OF PAYROLL
//bank reconciliation : 
DR LoanAccount.Customer.SDAccount.DepositAccount.LedgerAccount offsetAmount
CR Product.BankDepositAccount.LedgerAccount offsetAmount


//loan tracking:principal Product
DR LoanProduct.PrincipalAccount offsetAmount
CR LOAN_PROD_PRINCIPAL_CONTROL offsetAmount


//loan tracking:principal Account
DR LoanAccount.PrincipalBalanceAccount offsetAmount
CR LOAN_ACC_PRINCIPAL_CONTROL offsetAmount


//track charges Product
DR LOAN_PROD_CHARGE_ACCRUAL_CONTROL offsetCharge
CR LoanProduct.ChargesAccrualAccount offsetCharge


//track charges Account
DR LOAN_ACC_CHARGE_ACCRUAL_CONTROL offsetCharge
CR LoanAccount.ChargesAccruedAccount offsetCharge





		*/




        LoanTransactionViewModel rsp = new LoanTransactionViewModel();


        try
        {

            TransactionJournal journal = new TransactionJournal();
            journal.TransactionNo = NHiloHelper.GetNextKey(nameof(TransactionJournal)).ToString();
            journal.Title = $"Loan Offset by Special Deposits";
            journal.TransactionType = request.TransactionType;
            journal.TransactionDate = request.TransactionDate;
            journal.DocumentRef = nameof(LoanAccount);
            journal.DocumentRefId = request.LoanAccountId;
            journal.EntityRef = nameof(LoanOffset);
            journal.EntityRefId = request.EntityId;

            var txn = dbContext.LoanOffsets.Where(p => p.Id == request.EntityId)
                .Include(p => p.LoanAccount).ThenInclude(p => p.LoanApplication).ThenInclude(p => p.LoanProduct)
                .Include(p => p.CustomerBankAccount).ThenInclude(p => p.LedgerAccount)
                .Include(p => p.SpecialDepositAccount).ThenInclude(p => p.DepositAccount)
                .Include(p => p.SpecialDepositAccount).ThenInclude(p => p.DepositProduct).ThenInclude(p => p.ProductDepositAccount)
                //.Include(p => p.SavingsAccount).ThenInclude(p => p.LedgerDepositAccount)
                .FirstOrDefault();

            var charges = dbContext.LoanOffSetCharges.Where(p => p.LoanOffsetId == request.EntityId);

            var loanAccount = dbContext.LoanAccounts.Where(r => r.Id == request.LoanAccountId)
                .Include(p => p.LoanApplication).ThenInclude(p => p.LoanProduct)
                .ThenInclude(p => p.BankDepositAccount).ThenInclude(p => p.LedgerAccount)
                .Include(c => c.Customer).ThenInclude(c => c.CashAccount)
                .FirstOrDefault();

            var _loanAccountQuery = dbContext.LoanAccounts.Where(r => r.Id == request.LoanAccountId);

            var _loanProductQuery = _loanAccountQuery.Include(x => x.LoanApplication)
                .ThenInclude(x => x.LoanProduct);



            LedgerAccount CUST_CASH_ACC = loanAccount.Customer.CashAccount;

            // loanAccount.PrincipalBalanceAccountId
            LedgerAccount LOAN_ACC_PRINCIPAL_BALANCE = _loanAccountQuery.Include(x => x.PrincipalBalanceAccount)
                .Select(c => c.PrincipalBalanceAccount).FirstOrDefault();

            // loanAccount.UnearnedInterestAccountId
            LedgerAccount LOAN_ACC_UNEARNED_INTEREST = _loanAccountQuery.Include(x => x.UnearnedInterestAccount)
                .Select(c => c.UnearnedInterestAccount).FirstOrDefault();

            // loanAccount.EarnedInterestAccountId
            LedgerAccount LOAN_ACC_EARNED_INTEREST = _loanAccountQuery.Include(x => x.EarnedInterestAccount)
                .Select(c => c.EarnedInterestAccount).FirstOrDefault();

            // loanAccount.ChargesAccruedAccountId
            LedgerAccount LOAN_ACC_CHARGES_ACCRUAL = _loanAccountQuery.Include(x => x.ChargesAccruedAccount)
                .Select(c => c.ChargesAccruedAccount).FirstOrDefault();


            // loanAccount.LoanApplication.LoanProduct.PrincipalAccount.Id
            LedgerAccount LOAN_PROD_PRINCIPAL = _loanProductQuery.ThenInclude(x => x.PrincipalAccount)
                .Select(c => c.LoanApplication.LoanProduct.PrincipalAccount)
                .FirstOrDefault();


            // "loanAccount.LoanApplication.LoanProduct.UnearnedInterestAccountId"
            LedgerAccount LOAN_PROD_UNEARNED_INTEREST = _loanProductQuery.ThenInclude(x => x.UnearnedInterestAccount)
                .Select(c => c.LoanApplication.LoanProduct.UnearnedInterestAccount)
                .FirstOrDefault();

            LedgerAccount LOAN_PROD_EARNED_INTEREST = _loanProductQuery.ThenInclude(x => x.InterestIncomeAccount)
               .Select(c => c.LoanApplication.LoanProduct.InterestIncomeAccount)
               .FirstOrDefault();

            // loanAccount.LoanApplication.LoanProduct.ChargesAccrualAccountId
            LedgerAccount LOAN_PROD_CHARGES_ACCRUAL = _loanProductQuery.ThenInclude(x => x.ChargesAccrualAccount)
                .Select(c => c.LoanApplication.LoanProduct.ChargesAccrualAccount)
                .FirstOrDefault();





            LedgerAccount LOAN_PROD_PRINCIPAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PROD_PRINCIPAL_CONTROL.ToString());

            LedgerAccount LOAN_ACC_PRINCIPAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_ACC_PRINCIPAL_CONTROL.ToString());

            LedgerAccount LOAN_PROD_UNEARNED_INTEREST_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PROD_UNEARNED_INTEREST_CONTROL.ToString());


            LedgerAccount LOAN_PROD_EARNED_INTEREST_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PROD_EARNED_INTEREST_CONTROL.ToString());



            LedgerAccount LOAN_ACC_UNEARNED_INTEREST_CONTROL =
               dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_ACC_UNEARNED_INTEREST_CONTROL.ToString());


            LedgerAccount LOAN_ACC_EARNED_INTEREST_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_ACC_EARNED_INTEREST_CONTROL.ToString());



            LedgerAccount LOAN_PROD_CHARGE_ACCRUAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PROD_CHARGE_ACCRUAL_CONTROL.ToString());
            LedgerAccount LOAN_ACC_CHARGE_ACCRUAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_ACC_CHARGE_ACCRUAL_CONTROL.ToString());


            LedgerAccount LOAN_CUST_BANK_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_CUST_BANK_CONTROL.ToString());


            LedgerAccount SD_PROD_CONTROL =
               dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_PROD_CONTROL.ToString());


            var loanhelper = new LoanHelper(loanAccount.Principal, loanAccount.LoanApplication.LoanProduct.InterestRate,
                loanAccount.LoanApplication.LoanProduct.InterestMethod,
                loanAccount.LoanApplication.LoanProduct.InterestCalculationMethod,
                loanAccount.TenureUnit, loanAccount.TenureValue,
                loanAccount.LoanApplication.LoanProduct.RepaymentPeriod,
                loanAccount.LoanApplication.LoanProduct.DaysInYear,
                loanAccount.RepaymentCommencementDate.UtcDateTime);

            var paymentSchedules = loanhelper.GetAmortizationTable(loanAccount.LoanApplication.LoanProduct.InterestCalculationMethod);
            var firstPayment = paymentSchedules.FirstOrDefault();


            decimal offsetAmount = txn.OffsetAmount;
            decimal principalOffset = txn.OffsetAmount;
            decimal interestOffset = 0;



            //DR Product deposit account offsetAmount
            //CR SD_PROD_CONTROL offsetAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = txn.SpecialDepositAccount.DepositProduct.ProductDepositAccount.Id,
                Account = txn.SpecialDepositAccount.DepositProduct.ProductDepositAccount,
                Debit = txn.OffsetAmount,
                TransactionDate = request.TransactionDate
            });
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = SD_PROD_CONTROL.Id,
                Account = SD_PROD_CONTROL,
                Credit = txn.OffsetAmount,
                TransactionDate = request.TransactionDate
            });




            //DR LoanAccount.Customer.SD.DepositAccount.LedgerAccount offsetAmount
            //CR Product.BankDepositAccount.LedgerAccount offsetAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = txn.SpecialDepositAccount.DepositAccount.Id,
                Account = txn.SpecialDepositAccount.DepositAccount,
                Debit = txn.OffsetAmount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = loanAccount.LoanApplication.LoanProduct.BankDepositAccount.LedgerAccountId,
                Account = loanAccount.LoanApplication.LoanProduct.BankDepositAccount.LedgerAccount,
                Credit = txn.OffsetAmount,
                TransactionDate = request.TransactionDate
            });




            //loan tracking:principal Product
            //DR LoanProduct.PrincipalAccount offsetAmount
            //CR LOAN_PROD_PRINCIPAL_CONTROL offsetAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = LOAN_PROD_PRINCIPAL.Id,
                Account = LOAN_PROD_PRINCIPAL,
                Debit = txn.OffsetAmount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = LOAN_PROD_PRINCIPAL_CONTROL.Id,
                Account = LOAN_PROD_PRINCIPAL_CONTROL,
                Credit = txn.OffsetAmount,
                TransactionDate = request.TransactionDate
            });

            //loan tracking:principal Account
            //DR OldLoanAccount.PrincipalBalanceAccount offsetAmount
            //CR LOAN_ACC_PRINCIPAL_CONTROL offsetAmount

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = LOAN_ACC_PRINCIPAL_BALANCE.Id,
                Account = LOAN_ACC_PRINCIPAL_BALANCE,
                Debit = txn.OffsetAmount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = LOAN_ACC_PRINCIPAL_CONTROL.Id,
                Account = LOAN_ACC_PRINCIPAL_CONTROL,
                Credit = txn.OffsetAmount,
                TransactionDate = request.TransactionDate
            });


            switch (txn.AllowedOffsetType)
            {
                case AllowedOffsetType.PARTIAL:
                    {

                        loanAccount.ParentAccount.IsClosed = true;
                        loanAccount.ParentAccount.DateClosed = DateTime.Now;

                        break;
                    }

                case AllowedOffsetType.FULL:
                    {
                        loanAccount.IsClosed = true;
                        loanAccount.DateClosed = DateTime.Now;
                        break;
                    }

                case AllowedOffsetType.IN_LIEU_OF_PAYROLL:
                    {
                        LoanRepaymentSchedule loanRepaymentSchedule;


                        var scheduleId = txn.RepaymentSchedules.FirstOrDefault();
                        if (scheduleId != null)
                        {
                            loanRepaymentSchedule = dbContext.LoanRepaymentSchedules.FirstOrDefault(p => p.Id == scheduleId);
                            if (loanRepaymentSchedule != null)
                            {
                                principalOffset = loanRepaymentSchedule.PeriodPrincipal;
                                interestOffset = loanRepaymentSchedule.PeriodInterest;

                                //Reduce unearned interest balance

                                //loan tracking:unearned interest Product
                                //DR Prod.UnearnedInterest periodInterest
                                //CR LOAN_PROD_UNEARNED_INTEREST_CONTROL periodInterest
                                journal.JournalEntries.Add(new JournalEntry
                                {
                                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                    EntryType = TransactionEntryType.DEBIT,
                                    AccountId = LOAN_PROD_UNEARNED_INTEREST.Id,
                                    Account = LOAN_PROD_UNEARNED_INTEREST,
                                    Debit = interestOffset,
                                    TransactionDate = request.TransactionDate
                                });

                                journal.JournalEntries.Add(new JournalEntry
                                {
                                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                    EntryType = TransactionEntryType.CREDIT,
                                    AccountId = LOAN_PROD_UNEARNED_INTEREST_CONTROL.Id,
                                    Account = LOAN_PROD_UNEARNED_INTEREST_CONTROL,
                                    Credit = interestOffset,
                                    TransactionDate = request.TransactionDate
                                });

                                //loan tracking:unearned interest Account
                                //DR Acc.UnearnedInterest periodInterest
                                //CR LOAN_ACC_UNEARNED_INTEREST_CONTROL periodInterest
                                journal.JournalEntries.Add(new JournalEntry
                                {
                                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                    EntryType = TransactionEntryType.DEBIT,
                                    AccountId = LOAN_ACC_UNEARNED_INTEREST.Id,
                                    Account = LOAN_ACC_UNEARNED_INTEREST,
                                    Debit = interestOffset,
                                    TransactionDate = request.TransactionDate
                                });

                                journal.JournalEntries.Add(new JournalEntry
                                {
                                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                    EntryType = TransactionEntryType.CREDIT,
                                    AccountId = LOAN_ACC_UNEARNED_INTEREST_CONTROL.Id,
                                    Account = LOAN_ACC_UNEARNED_INTEREST_CONTROL,
                                    Credit = interestOffset,
                                    TransactionDate = request.TransactionDate
                                });


                                //Increase earned interest balance

                                //loan tracking:interest income Product
                                //DR LOAN_PROD_EARNED_INTEREST_CONTROL periodInterest
                                //CR Prod.EarnedInterest periodInterest
                                journal.JournalEntries.Add(new JournalEntry
                                {
                                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                    EntryType = TransactionEntryType.DEBIT,
                                    AccountId = LOAN_PROD_EARNED_INTEREST_CONTROL.Id,
                                    Account = LOAN_PROD_EARNED_INTEREST_CONTROL,
                                    Debit = interestOffset,
                                    TransactionDate = request.TransactionDate
                                });

                                journal.JournalEntries.Add(new JournalEntry
                                {
                                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                    EntryType = TransactionEntryType.CREDIT,
                                    AccountId = LOAN_PROD_EARNED_INTEREST.Id,
                                    Account = LOAN_PROD_EARNED_INTEREST,
                                    Credit = interestOffset,
                                    TransactionDate = request.TransactionDate
                                });

                                //loan tracking:interest income Account
                                //DR LOAN_ACC_EARNED_INTEREST_CONTROL periodInterest
                                //CR Acc.EarnedInterest periodInterest
                                journal.JournalEntries.Add(new JournalEntry
                                {
                                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                    EntryType = TransactionEntryType.DEBIT,
                                    AccountId = LOAN_ACC_EARNED_INTEREST_CONTROL.Id,
                                    Account = LOAN_ACC_EARNED_INTEREST_CONTROL,
                                    Debit = interestOffset,
                                    TransactionDate = request.TransactionDate
                                });

                                journal.JournalEntries.Add(new JournalEntry
                                {
                                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                    EntryType = TransactionEntryType.CREDIT,
                                    AccountId = LOAN_ACC_EARNED_INTEREST.Id,
                                    Account = LOAN_ACC_EARNED_INTEREST,
                                    Credit = interestOffset,
                                    TransactionDate = request.TransactionDate
                                });
                            }
                        }


                        var unPaidSchedules = dbContext.LoanRepaymentSchedules.Any(x => x.LoanAccountId == txn.LoanAccountId && !x.IsPaid);
                        if (!unPaidSchedules)
                        {
                            loanAccount.IsClosed = true;
                            loanAccount.DateClosed = DateTime.Now;
                        }

                        break;


                    }
            }





            if (charges.Any())
            {
                // Get total charge amount
                foreach (var charge in charges)
                {
                    //track charges Product
                    //DR LOAN_PROD_CHARGE_ACCRUAL_CONTROL 1,000
                    //CR LoanProduct.ChargesAccrualAccount 1,000
                    journal.JournalEntries.Add(new JournalEntry
                    {
                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                        EntryType = TransactionEntryType.DEBIT,
                        AccountId = LOAN_PROD_CHARGE_ACCRUAL_CONTROL.Id,
                        Account = LOAN_PROD_CHARGE_ACCRUAL_CONTROL,
                        Debit = charge.TotalCharge,
                        TransactionDate = request.TransactionDate
                    });

                    journal.JournalEntries.Add(new JournalEntry
                    {
                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                        EntryType = TransactionEntryType.CREDIT,
                        AccountId = LOAN_PROD_CHARGES_ACCRUAL.Id,
                        Account = LOAN_PROD_CHARGES_ACCRUAL,
                        Credit = charge.TotalCharge,
                        TransactionDate = request.TransactionDate
                    });


                    //track charges Account
                    //DR LOAN_ACC_CHARGE_ACCRUAL_CONTROL 1,000
                    //CR LoanAccount.ChargesAccruedAccount 1,000

                    journal.JournalEntries.Add(new JournalEntry
                    {
                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                        EntryType = TransactionEntryType.DEBIT,
                        AccountId = LOAN_ACC_CHARGE_ACCRUAL_CONTROL.Id,
                        Account = LOAN_ACC_CHARGE_ACCRUAL_CONTROL,
                        Debit = charge.TotalCharge,
                        TransactionDate = request.TransactionDate
                    });

                    journal.JournalEntries.Add(new JournalEntry
                    {
                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                        EntryType = TransactionEntryType.CREDIT,
                        AccountId = LOAN_ACC_CHARGES_ACCRUAL.Id,
                        Account = LOAN_ACC_CHARGES_ACCRUAL,
                        Credit = charge.TotalCharge,
                        TransactionDate = request.TransactionDate
                    });
                }
            }



            dbContext.TransactionJournals.Add(journal);
            dbContext.SaveChanges();

            journal.Post();

            txn.TransactionJournalId = journal.Id;
            txn.IsProcessed = true;
            txn.ProcessedDate = request.TransactionDate;
            txn.Status = TransactionStatus.SUCCESS;



            dbContext.Update(journal);
            dbContext.SaveChanges();



            rsp.TransactionDate = request.TransactionDate;
            rsp.TransactionType = request.TransactionType;
            rsp.EntityId = request.EntityId;

            rsp.IsApproved = true;
            rsp.ApprovedOn = request.TransactionDate;
            rsp.IsPosted = true;
            rsp.PostedOn = request.TransactionDate;



        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            rsp.ErrorDate = DateTime.Now;
            rsp.ErrorDetails = ex.Message;
            rsp.ErrorFlag = true;

            //throw ex;

        }


        return rsp;
    }

    private LoanTransactionViewModel ProcessLoanDisbursementFull(LoanTransactionCommand request, CancellationToken cancellationToken)
    {


        /*
		* 
		   

		Loan Disbursement full v1



		//bank reconciliation : SD
		DR Disbursement.DisbursementAccount.LedgerAccount 1,000,000
		CR Disbursement.SpecialDepositAccount.DepositAccount 1,000,000

		//bank reconciliation : Cust bank
		DR Disbursement.DisbursementAccount.LedgerAccount 1,000,000
		CR Disbursement.CustomerBankAccount.LedgerAccount 1,000,000


		//loan tracking:principal Product
		DR LOAN_PROD_PRINCIPAL_CONTROL 1,000,000
		CR LoanProduct.PrincipalAccount 1,000,000


		//loan tracking:principal Account
		DR LOAN_ACC_PRINCIPAL_CONTROL 1,000,000
		CR LoanAccount.PrincipalBalanceAccount 1,000,000


		//loan tracking:unearned interest Product
		DR LOAN_PROD_UNEARNED_INTEREST_CONTROL 100,000
		CR Product.UnearnedInterest 100,000


		//loan tracking:unearned interest Account
		DR LOAN_ACC_UNEARNED_INTEREST_CONTROL 100,000
		CR Account.UnearnedInterest 100,000

		//track charges Product
		DR LOAN_PROD_CHARGE_ACCRUAL_CONTROL 1,000
		CR LoanProduct.ChargesAccrualAccount 1,000


		//track charges Account
		DR LOAN_ACC_CHARGE_ACCRUAL_CONTROL 1,000
		CR LoanAccount.ChargesAccruedAccount 1,000


		* 
		* 
		*/


        LoanTransactionViewModel rsp = new LoanTransactionViewModel();


        try
        {

            TransactionJournal journal = new TransactionJournal();
            journal.TransactionNo = NHiloHelper.GetNextKey(nameof(TransactionJournal)).ToString();
            journal.Title = $"Loan Account Disbursement";
            journal.TransactionType = request.TransactionType;
            journal.TransactionDate = request.TransactionDate;
            journal.DocumentRef = nameof(LoanAccount);
            journal.DocumentRefId = request.LoanAccountId;
            journal.EntityRef = nameof(LoanDisbursement);
            journal.EntityRefId = request.EntityId;

            var txn = dbContext.LoanDisbursements.Where(p => p.Id == request.EntityId)
                .Include(p => p.DisbursementAccount).ThenInclude(p => p.LedgerAccount)
                .Include(p => p.CustomerBankAccount).ThenInclude(p => p.LedgerAccount)
                .Include(p => p.SpecialDepositAccount).ThenInclude(p => p.DepositAccount)
                .FirstOrDefault();

            var charges = dbContext.LoanDisbursementCharges.Where(p => p.LoanDisbursementId == request.EntityId);

            var loanAccount = dbContext.LoanAccounts.Where(r => r.Id == request.LoanAccountId)
                .Include(p => p.LoanApplication).ThenInclude(p => p.LoanProduct)
                .Include(c => c.Customer).ThenInclude(c => c.CashAccount)
                .FirstOrDefault();

            var _loanAccountQuery = dbContext.LoanAccounts.Where(r => r.Id == request.LoanAccountId);

            var _loanProductQuery = _loanAccountQuery.Include(x => x.LoanApplication)
                .ThenInclude(x => x.LoanProduct);



            // loanAccount.PrincipalBalanceAccountId
            LedgerAccount LOAN_ACC_PRINCIPAL_BALANCE = _loanAccountQuery.Include(x => x.PrincipalBalanceAccount)
                .Select(c => c.PrincipalBalanceAccount).FirstOrDefault();

            // loanAccount.UnearnedInterestAccountId
            LedgerAccount LOAN_ACC_UNEARNED_INTEREST = _loanAccountQuery.Include(x => x.UnearnedInterestAccount)
                .Select(c => c.UnearnedInterestAccount).FirstOrDefault();

            // loanAccount.ChargesAccruedAccountId
            LedgerAccount LOAN_ACC_CHARGES_ACCRUAL = _loanAccountQuery.Include(x => x.ChargesAccruedAccount)
                .Select(c => c.ChargesAccruedAccount).FirstOrDefault();


            // loanAccount.LoanApplication.LoanProduct.PrincipalAccount.Id
            LedgerAccount LOAN_ACC_PROD_PRINCIPAL = _loanProductQuery.ThenInclude(x => x.PrincipalAccount)
                .Select(c => c.LoanApplication.LoanProduct.PrincipalAccount)
                .FirstOrDefault();


            // "loanAccount.LoanApplication.LoanProduct.UnearnedInterestAccountId"
            LedgerAccount LOAN_ACC_PROD_UNEARNED_INTEREST = _loanProductQuery.ThenInclude(x => x.UnearnedInterestAccount)
                .Select(c => c.LoanApplication.LoanProduct.UnearnedInterestAccount)
                .FirstOrDefault();

            // loanAccount.LoanApplication.LoanProduct.ChargesAccrualAccountId
            LedgerAccount LOAN_ACC_PROD_CHARGES_ACCRUAL = _loanProductQuery.ThenInclude(x => x.ChargesAccrualAccount)
                .Select(c => c.LoanApplication.LoanProduct.ChargesAccrualAccount)
                .FirstOrDefault();





            LedgerAccount LOAN_PROD_PRINCIPAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PROD_PRINCIPAL_CONTROL.ToString());

            LedgerAccount LOAN_ACC_PRINCIPAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_ACC_PRINCIPAL_CONTROL.ToString());

            LedgerAccount LOAN_PROD_UNEARNED_INTEREST_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PROD_UNEARNED_INTEREST_CONTROL.ToString());


            LedgerAccount LOAN_ACC_UNEARNED_INTEREST_CONTROL =
               dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_ACC_UNEARNED_INTEREST_CONTROL.ToString());

            LedgerAccount LOAN_PROD_CHARGE_ACCRUAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PROD_CHARGE_ACCRUAL_CONTROL.ToString());
            LedgerAccount LOAN_ACC_CHARGE_ACCRUAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_ACC_CHARGE_ACCRUAL_CONTROL.ToString());

            var loanhelper = new LoanHelper(loanAccount.Principal, loanAccount.LoanApplication.LoanProduct.InterestRate,
                loanAccount.LoanApplication.LoanProduct.InterestMethod,
                loanAccount.LoanApplication.LoanProduct.InterestCalculationMethod,
                loanAccount.TenureUnit, loanAccount.TenureValue,
                loanAccount.LoanApplication.LoanProduct.RepaymentPeriod,
                loanAccount.LoanApplication.LoanProduct.DaysInYear,
                loanAccount.RepaymentCommencementDate.UtcDateTime);

            var paymentSchedules = loanhelper.GetAmortizationTable(loanAccount.LoanApplication.LoanProduct.InterestCalculationMethod);
            var firstPayment = paymentSchedules.FirstOrDefault();

            if (txn.DisbursementMode == LoanDisbursementMode.SPECIAL_DEPOSIT)
            {
                LedgerAccount CUST_DEPOSIT_ACC = dbContext.LedgerAccounts.FirstOrDefault(c => c.Id == txn.SpecialDepositAccount.DepositAccountId);

                //DR Disbursement.DisbursementAccount.LedgerAccount 1,000,000
                //CR Disbursement.SpecialDepositAccount.DepositAccount 1,000,000
                journal.JournalEntries.Add(new JournalEntry
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = TransactionEntryType.DEBIT,
                    AccountId = txn.DisbursementAccount.LedgerAccountId,
                    Account = txn.DisbursementAccount.LedgerAccount,
                    Debit = txn.Amount,
                    TransactionDate = request.TransactionDate
                });

                journal.JournalEntries.Add(new JournalEntry
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = TransactionEntryType.CREDIT,
                    AccountId = CUST_DEPOSIT_ACC.Id,
                    Account = CUST_DEPOSIT_ACC,
                    Credit = txn.Amount,
                    TransactionDate = request.TransactionDate
                });

            }
            else if (txn.DisbursementMode == LoanDisbursementMode.BANK_TRANSFER)
            {
                var customerBankAccount = dbContext.CustomerBankAccounts.FirstOrDefault(p => p.Id == txn.CustomerBankAccountId);
                LedgerAccount CUST_BANK_ACC = dbContext.LedgerAccounts.FirstOrDefault(c => c.Id == customerBankAccount.LedgerAccountId);

                //DR Disbursement.DisbursementAccount.LedgerAccount 1,000,000
                //CR Disbursement.CustomerBankAccount.LedgerAccount 1,000,000
                journal.JournalEntries.Add(new JournalEntry
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = TransactionEntryType.DEBIT,
                    AccountId = txn.DisbursementAccount.LedgerAccountId,
                    Account = txn.DisbursementAccount.LedgerAccount,
                    Debit = txn.Amount,
                    TransactionDate = request.TransactionDate
                });

                journal.JournalEntries.Add(new JournalEntry
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = TransactionEntryType.CREDIT,
                    AccountId = CUST_BANK_ACC.Id,
                    Account = CUST_BANK_ACC,
                    Credit = txn.Amount,
                    TransactionDate = request.TransactionDate
                });
            }
            else
            {

            }


            //loan tracking:principal Product
            //DR LOAN_PROD_PRINCIPAL_CONTROL 1,000,000
            //CR LoanProduct.PrincipalAccount 1,000,000
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = LOAN_PROD_PRINCIPAL_CONTROL.Id,
                Account = LOAN_PROD_PRINCIPAL_CONTROL,
                Debit = loanAccount.Principal,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = LOAN_ACC_PROD_PRINCIPAL.Id,
                Account = LOAN_ACC_PROD_PRINCIPAL,
                Credit = loanAccount.Principal,
                TransactionDate = request.TransactionDate
            });


            //loan tracking:principal Account
            //DR LOAN_ACC_PRINCIPAL_CONTROL 1,000,000
            //CR LoanAccount.PrincipalBalanceAccount 1,000,000
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = LOAN_ACC_PRINCIPAL_CONTROL.Id,
                Account = LOAN_ACC_PRINCIPAL_CONTROL,
                Debit = loanAccount.Principal,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = LOAN_ACC_PRINCIPAL_BALANCE.Id,
                Account = LOAN_ACC_PRINCIPAL_BALANCE,
                Credit = loanAccount.Principal,
                TransactionDate = request.TransactionDate
            });




            //loan tracking:unearned interest Product
            //DR LOAN_PROD_UNEARNED_INTEREST_CONTROL 100,000
            //CR Product.UnearnedInterest 100,000
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = LOAN_PROD_UNEARNED_INTEREST_CONTROL.Id,
                Account = LOAN_PROD_UNEARNED_INTEREST_CONTROL,
                Debit = loanhelper.InterestEarned,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = LOAN_ACC_PROD_UNEARNED_INTEREST.Id,
                Account = LOAN_ACC_PROD_UNEARNED_INTEREST,
                Credit = loanhelper.InterestEarned,
                TransactionDate = request.TransactionDate
            });



            //loan tracking:unearned interest Account
            //DR LOAN_ACC_UNEARNED_INTEREST_CONTROL 100,000
            //CR Account.UnearnedInterest 100,000
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = LOAN_ACC_UNEARNED_INTEREST_CONTROL.Id,
                Account = LOAN_ACC_UNEARNED_INTEREST_CONTROL,
                Debit = loanhelper.InterestEarned,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = LOAN_ACC_UNEARNED_INTEREST.Id,
                Account = LOAN_ACC_UNEARNED_INTEREST,
                Credit = loanhelper.InterestEarned,
                TransactionDate = request.TransactionDate
            });


            if (charges.Any())
            {
                // Get total charge amount
                foreach (var charge in charges)
                {
                    //track charges Product
                    //DR LOAN_PROD_CHARGE_ACCRUAL_CONTROL 1,000
                    //CR LoanProduct.ChargesAccrualAccount 1,000
                    journal.JournalEntries.Add(new JournalEntry
                    {
                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                        EntryType = TransactionEntryType.DEBIT,
                        AccountId = LOAN_PROD_CHARGE_ACCRUAL_CONTROL.Id,
                        Account = LOAN_PROD_CHARGE_ACCRUAL_CONTROL,
                        Debit = charge.TotalCharge,
                        TransactionDate = request.TransactionDate
                    });

                    journal.JournalEntries.Add(new JournalEntry
                    {
                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                        EntryType = TransactionEntryType.CREDIT,
                        AccountId = LOAN_ACC_PROD_CHARGES_ACCRUAL.Id,
                        Account = LOAN_ACC_PROD_CHARGES_ACCRUAL,
                        Credit = charge.TotalCharge,
                        TransactionDate = request.TransactionDate
                    });


                    //track charges Account
                    //DR LOAN_ACC_CHARGE_ACCRUAL_CONTROL 1,000
                    //CR LoanAccount.ChargesAccruedAccount 1,000

                    journal.JournalEntries.Add(new JournalEntry
                    {
                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                        EntryType = TransactionEntryType.DEBIT,
                        AccountId = LOAN_ACC_CHARGE_ACCRUAL_CONTROL.Id,
                        Account = LOAN_ACC_CHARGE_ACCRUAL_CONTROL,
                        Debit = charge.TotalCharge,
                        TransactionDate = request.TransactionDate
                    });

                    journal.JournalEntries.Add(new JournalEntry
                    {
                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                        EntryType = TransactionEntryType.CREDIT,
                        AccountId = LOAN_ACC_CHARGES_ACCRUAL.Id,
                        Account = LOAN_ACC_CHARGES_ACCRUAL,
                        Credit = charge.TotalCharge,
                        TransactionDate = request.TransactionDate
                    });
                }
            }



            dbContext.TransactionJournals.Add(journal);
            dbContext.SaveChanges();

            journal.Post();

            txn.TransactionJournalId = journal.Id;
            txn.IsProcessed = true;
            txn.ProcessedDate = request.TransactionDate;
            txn.Status = TransactionStatus.SUCCESS;


            dbContext.Update(journal);
            dbContext.SaveChanges();



            rsp.TransactionDate = request.TransactionDate;
            rsp.TransactionType = request.TransactionType;
            rsp.EntityId = request.EntityId;

            rsp.IsApproved = true;
            rsp.ApprovedOn = request.TransactionDate;
            rsp.IsPosted = true;
            rsp.PostedOn = request.TransactionDate;



        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            rsp.ErrorDate = DateTime.Now;
            rsp.ErrorDetails = ex.Message;
            rsp.ErrorFlag = true;

            //throw ex;

        }


        return rsp;



    }


    private LoanTransactionViewModel ProcessLoanDisbursementPartial(LoanTransactionCommand request, CancellationToken cancellationToken)
    {
        return null;
    }

    private LoanTransactionViewModel ProcessLoanCashRepayment(LoanTransactionCommand request, CancellationToken cancellationToken)
    {
        return null;
    }

    private LoanTransactionViewModel ProcessLoanPayrollRepayment(LoanTransactionCommand request, CancellationToken cancellationToken)
    {
        /*
		* 
		   

		Loan repayment : payroll v1

		//bank reconciliation : 
		DR LOAN_PAYROLL_CONTROL 1,000,000
		CR Product.BankDepositAccount.LedgerAccount 1,000,000


		//loan tracking:principal Product
		DR LoanProduct.PrincipalAccount 1,000,000
		CR LOAN_PROD_PRINCIPAL_CONTROL 1,000,000


		//loan tracking:principal Account
		DR LoanAccount.PrincipalBalanceAccount 1,000,000
		CR LOAN_ACC_PRINCIPAL_CONTROL 1,000,000


		--unearned interest
		//loan tracking:unearned interest Product
		DR Product.UnearnedInterest 100,000
		CR LOAN_PROD_UNEARNED_INTEREST_CONTROL 100,000


		//loan tracking:unearned interest Account
		DR Account.UnearnedInterest 100,000
		CR LOAN_ACC_UNEARNED_INTEREST_CONTROL 100,000

		--earned interest
		//loan tracking:earned interest Product
		DR LOAN_PROD_EARNED_INTEREST_CONTROL 100,000
		CR Product.InterestIncomeAccount 100,000


		//loan tracking:earned interest Account
		DR LOAN_ACC_EARNED_INTEREST_CONTROL 100,000
		CR Account.EarnedInterestAccount 100,000


		//track charges Product
		DR LOAN_PROD_CHARGE_ACCRUAL_CONTROL 1,000
		CR LoanProduct.ChargesAccrualAccount 1,000


		//track charges Account
		DR LOAN_ACC_CHARGE_ACCRUAL_CONTROL 1,000
		CR LoanAccount.ChargesAccruedAccount 1,000


		* 
		* 
		*/


        LoanTransactionViewModel rsp = new LoanTransactionViewModel();


        try
        {

            TransactionJournal journal = new TransactionJournal();
            journal.TransactionNo = NHiloHelper.GetNextKey(nameof(TransactionJournal)).ToString();
            journal.Title = $"Loan Account Payroll Repayment";
            journal.TransactionType = request.TransactionType;
            journal.TransactionDate = request.TransactionDate;
            journal.DocumentRef = nameof(LoanAccount);
            journal.DocumentRefId = request.LoanAccountId;
            journal.EntityRef = nameof(PayrollDeductionScheduleItem);
            journal.EntityRefId = request.EntityId;

            var txn = dbContext.PayrollDeductionScheduleItems.Where(p => p.Id == request.EntityId)
                .Include(p => p.LoanRepaymentSchedule)
                .FirstOrDefault();

            //var charges = dbContext.LoanDisbursementCharges.Where(p => p.LoanDisbursementId == request.EntityId);

            var loanAccount = dbContext.LoanAccounts.Where(r => r.Id == request.LoanAccountId)
                .Include(p => p.LoanApplication)
                .ThenInclude(p => p.LoanProduct).ThenInclude(p => p.BankDepositAccount).ThenInclude(p => p.LedgerAccount)
                .Include(p => p.LoanApplication)
                .ThenInclude(p => p.LoanProduct).ThenInclude(p => p.PrincipalAccount)
                .Include(p => p.LoanApplication)
                .ThenInclude(p => p.LoanProduct).ThenInclude(p => p.InterestIncomeAccount)
                .Include(p => p.LoanApplication)
                .ThenInclude(p => p.LoanProduct).ThenInclude(p => p.UnearnedInterestAccount)
                .Include(p => p.Customer).ThenInclude(p => p.CashAccount)
                .Include(p => p.PrincipalBalanceAccount)
                .Include(p => p.InterestBalanceAccount)
                .Include(p => p.EarnedInterestAccount)
                .Include(p => p.UnearnedInterestAccount)
                .FirstOrDefault();


            LedgerAccount LOAN_PAYROLL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PAYROLL_CONTROL.ToString());

            LedgerAccount LOAN_PROD_PRINCIPAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PROD_PRINCIPAL_CONTROL.ToString());

            LedgerAccount LOAN_ACC_PRINCIPAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_ACC_PRINCIPAL_CONTROL.ToString());

            LedgerAccount LOAN_PROD_UNEARNED_INTEREST_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PROD_UNEARNED_INTEREST_CONTROL.ToString());


            LedgerAccount LOAN_ACC_UNEARNED_INTEREST_CONTROL =
               dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_ACC_UNEARNED_INTEREST_CONTROL.ToString());


            LedgerAccount LOAN_PROD_EARNED_INTEREST_CONTROL =
               dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PROD_EARNED_INTEREST_CONTROL.ToString());


            LedgerAccount LOAN_ACC_EARNED_INTEREST_CONTROL =
               dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_ACC_EARNED_INTEREST_CONTROL.ToString());


            LedgerAccount LOAN_PROD_CHARGE_ACCRUAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PROD_CHARGE_ACCRUAL_CONTROL.ToString());
            LedgerAccount LOAN_ACC_CHARGE_ACCRUAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_ACC_CHARGE_ACCRUAL_CONTROL.ToString());


            var loanhelper = new LoanHelper(loanAccount.Principal, loanAccount.LoanApplication.LoanProduct.InterestRate,
                loanAccount.LoanApplication.LoanProduct.InterestMethod,
                loanAccount.LoanApplication.LoanProduct.InterestCalculationMethod,
                loanAccount.TenureUnit, loanAccount.TenureValue,
                loanAccount.LoanApplication.LoanProduct.RepaymentPeriod,
                loanAccount.LoanApplication.LoanProduct.DaysInYear,
                loanAccount.RepaymentCommencementDate.UtcDateTime);

            var paymentSchedules = loanhelper.GetAmortizationTable(loanAccount.LoanApplication.LoanProduct.InterestCalculationMethod);
            var firstPayment = paymentSchedules.FirstOrDefault();



            //payroll reconciliation : 
            //DR LOAN_PAYROLL_CONTROL 1,000,000
            //CR Product.BankDepositAccount.LedgerAccount 1,000,000
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = LOAN_PAYROLL_CONTROL.Id,
                Account = LOAN_PAYROLL_CONTROL,
                Debit = txn.Amount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = loanAccount.LoanApplication.LoanProduct.BankDepositAccountId,
                Account = loanAccount.LoanApplication.LoanProduct.BankDepositAccount.LedgerAccount,
                Credit = txn.Amount,
                TransactionDate = request.TransactionDate
            });


            //loan tracking:principal Product
            //DR LoanProduct.PrincipalAccount 1,000,000
            //CR LOAN_PROD_PRINCIPAL_CONTROL 1,000,000
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = loanAccount.LoanApplication.LoanProduct.PrincipalAccountId,
                Account = loanAccount.LoanApplication.LoanProduct.PrincipalAccount,
                Debit = txn.LoanRepaymentSchedule.PeriodPrincipal,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = LOAN_PROD_PRINCIPAL_CONTROL.Id,
                Account = LOAN_PROD_PRINCIPAL_CONTROL,
                Credit = txn.LoanRepaymentSchedule.PeriodPrincipal,
                TransactionDate = request.TransactionDate
            });


            //loan tracking:principal Account
            //DR LoanAccount.PrincipalBalanceAccount 1,000,000
            //CR LOAN_ACC_PRINCIPAL_CONTROL 1,000,000
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = loanAccount.PrincipalBalanceAccountId,
                Account = loanAccount.PrincipalBalanceAccount,
                Debit = txn.LoanRepaymentSchedule.PeriodPrincipal,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = LOAN_ACC_PRINCIPAL_CONTROL.Id,
                Account = LOAN_ACC_PRINCIPAL_CONTROL,
                Credit = txn.LoanRepaymentSchedule.PeriodPrincipal,
                TransactionDate = request.TransactionDate
            });




            //--unearned interest
            //loan tracking: unearned interest Product
            //DR Product.UnearnedInterest 100,000
            //CR LOAN_PROD_UNEARNED_INTEREST_CONTROL 100,000
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = loanAccount.LoanApplication.LoanProduct.UnearnedInterestAccountId,
                Account = loanAccount.LoanApplication.LoanProduct.UnearnedInterestAccount,
                Debit = loanhelper.InterestEarned,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = LOAN_PROD_UNEARNED_INTEREST_CONTROL.Id,
                Account = LOAN_PROD_UNEARNED_INTEREST_CONTROL,
                Credit = loanhelper.InterestEarned,
                TransactionDate = request.TransactionDate
            });



            //loan tracking:unearned interest Account
            //DR Account.UnearnedInterest 100,000
            //CR LOAN_ACC_UNEARNED_INTEREST_CONTROL 100,000
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = loanAccount.UnearnedInterestAccountId,
                Account = loanAccount.UnearnedInterestAccount,
                Debit = loanhelper.InterestEarned,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = LOAN_ACC_UNEARNED_INTEREST_CONTROL.Id,
                Account = LOAN_ACC_UNEARNED_INTEREST_CONTROL,
                Credit = loanhelper.InterestEarned,
                TransactionDate = request.TransactionDate
            });


            //--earned interest
            //loan tracking: earned interest Product
            //DR LOAN_PROD_EARNED_INTEREST_CONTROL 100,000
            //CR Product.InterestIncomeAccount 100,000
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = LOAN_PROD_EARNED_INTEREST_CONTROL.Id,
                Account = LOAN_PROD_EARNED_INTEREST_CONTROL,
                Debit = loanhelper.InterestEarned,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = loanAccount.LoanApplication.LoanProduct.InterestIncomeAccountId,
                Account = loanAccount.LoanApplication.LoanProduct.InterestIncomeAccount,
                Credit = loanhelper.InterestEarned,
                TransactionDate = request.TransactionDate
            });




            //loan tracking:earned interest Account
            //DR LOAN_ACC_EARNED_INTEREST_CONTROL 100,000
            //CR Account.EarnedInterestAccount 100,000
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = LOAN_ACC_EARNED_INTEREST_CONTROL.Id,
                Account = LOAN_ACC_EARNED_INTEREST_CONTROL,
                Debit = loanhelper.InterestEarned,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = loanAccount.EarnedInterestAccountId,
                Account = loanAccount.EarnedInterestAccount,
                Credit = loanhelper.InterestEarned,
                TransactionDate = request.TransactionDate
            });



            //if (charges.Any())
            //{

            //    //track charges Product
            //    //DR LOAN_PROD_CHARGE_ACCRUAL_CONTROL 1,000
            //    //CR LoanProduct.ChargesAccrualAccount 1,000


            //    //track charges Account
            //    //DR LOAN_ACC_CHARGE_ACCRUAL_CONTROL 1,000
            //    //CR LoanAccount.ChargesAccruedAccount 1,000
            //}



            dbContext.TransactionJournals.Add(journal);
            dbContext.SaveChanges();

            journal.Post();

            //txn.TransactionJournalId = journal.Id;
            //txn.IsProcessed = true;
            //txn.ProcessedDate = request.TransactionDate;
            txn.CurrentStatus = TransactionStatus.SUCCESS.ToJson();

            dbContext.Update(journal);
            dbContext.SaveChanges();



            rsp.TransactionDate = request.TransactionDate;
            rsp.TransactionType = request.TransactionType;
            rsp.EntityId = request.EntityId;

            rsp.IsApproved = true;
            rsp.ApprovedOn = request.TransactionDate;
            rsp.IsPosted = true;
            rsp.PostedOn = request.TransactionDate;



        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            rsp.ErrorDate = DateTime.Now;
            rsp.ErrorDetails = ex.Message;
            rsp.ErrorFlag = true;

            //throw ex;

        }


        return rsp;
    }

    private LoanTransactionViewModel ProcessLoanTopupDisbursement(LoanTransactionCommand request, CancellationToken cancellationToken)
    {
        LoanTransactionViewModel rsp = new();

        try
        {
            TransactionJournal journal = new()
            {
                TransactionNo = NHiloHelper.GetNextKey(nameof(TransactionJournal)).ToString(),
                Title = $"Loan Account Disbursement",
                TransactionType = request.TransactionType,
                TransactionDate = request.TransactionDate,
                DocumentRef = nameof(LoanAccount),
                DocumentRefId = request.LoanAccountId,
                EntityRef = nameof(LoanDisbursement),
                EntityRefId = request.EntityId
            };

            var txn = dbContext.LoanDisbursements.Where(p => p.Id == request.EntityId)
                .Include(p => p.DisbursementAccount).ThenInclude(p => p.LedgerAccount)
                .Include(p => p.CustomerBankAccount).ThenInclude(p => p.LedgerAccount)
                .Include(p => p.SpecialDepositAccount).ThenInclude(p => p.DepositAccount)
                .FirstOrDefault();

            var loanTopup = dbContext.LoanTopups.FirstOrDefault(x => x.LoanAccountId == txn.LoanAccountId);

            var charges = dbContext.LoanDisbursementCharges.Where(p => p.LoanDisbursementId == request.EntityId);

            var newLoanAccount = dbContext.LoanAccounts.Where(r => r.Id == request.LoanAccountId)
                .Include(p => p.LoanApplication).ThenInclude(p => p.LoanProduct)
                .Include(c => c.Customer).ThenInclude(c => c.CashAccount)
                .FirstOrDefault();

            var oldLoanAccount = dbContext.LoanAccounts.Where(r => r.Id == newLoanAccount.ParentAccountId)
                .Include(p => p.PrincipalBalanceAccount)
                .Include(p => p.UnearnedInterestAccount)
                .FirstOrDefault();

            var _loanAccountQuery = dbContext.LoanAccounts.Where(r => r.Id == request.LoanAccountId);

            var _loanProductQuery = _loanAccountQuery.Include(x => x.LoanApplication)
                .ThenInclude(x => x.LoanProduct);

            // loanAccount.PrincipalBalanceAccountId
            LedgerAccount LOAN_ACC_PRINCIPAL_BALANCE = _loanAccountQuery.Include(x => x.PrincipalBalanceAccount)
                .Select(c => c.PrincipalBalanceAccount).FirstOrDefault();

            // loanAccount.UnearnedInterestAccountId
            LedgerAccount LOAN_ACC_UNEARNED_INTEREST = _loanAccountQuery.Include(x => x.UnearnedInterestAccount)
                .Select(c => c.UnearnedInterestAccount).FirstOrDefault();

            // loanAccount.ChargesAccruedAccountId
            LedgerAccount LOAN_ACC_CHARGES_ACCRUAL = _loanAccountQuery.Include(x => x.ChargesAccruedAccount)
                .Select(c => c.ChargesAccruedAccount).FirstOrDefault();


            // loanAccount.LoanApplication.LoanProduct.PrincipalAccount.Id
            LedgerAccount LOAN_ACC_PROD_PRINCIPAL = _loanProductQuery.ThenInclude(x => x.PrincipalAccount)
                .Select(c => c.LoanApplication.LoanProduct.PrincipalAccount)
                .FirstOrDefault();


            // "loanAccount.LoanApplication.LoanProduct.UnearnedInterestAccountId"
            LedgerAccount LOAN_ACC_PROD_UNEARNED_INTEREST = _loanProductQuery.ThenInclude(x => x.UnearnedInterestAccount)
                .Select(c => c.LoanApplication.LoanProduct.UnearnedInterestAccount)
                .FirstOrDefault();

            // loanAccount.LoanApplication.LoanProduct.ChargesAccrualAccountId
            LedgerAccount LOAN_ACC_PROD_CHARGES_ACCRUAL = _loanProductQuery.ThenInclude(x => x.ChargesAccrualAccount)
                .Select(c => c.LoanApplication.LoanProduct.ChargesAccrualAccount)
                .FirstOrDefault();


            LedgerAccount LOAN_PROD_PRINCIPAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PROD_PRINCIPAL_CONTROL.ToString());

            LedgerAccount LOAN_ACC_PRINCIPAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_ACC_PRINCIPAL_CONTROL.ToString());

            LedgerAccount LOAN_PROD_UNEARNED_INTEREST_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PROD_UNEARNED_INTEREST_CONTROL.ToString());


            LedgerAccount LOAN_ACC_UNEARNED_INTEREST_CONTROL =
               dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_ACC_UNEARNED_INTEREST_CONTROL.ToString());

            LedgerAccount LOAN_PROD_CHARGE_ACCRUAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PROD_CHARGE_ACCRUAL_CONTROL.ToString());
            LedgerAccount LOAN_ACC_CHARGE_ACCRUAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_ACC_CHARGE_ACCRUAL_CONTROL.ToString());

            var loanhelper = new LoanHelper(loanTopup!.NewPrincipalBalance, 
                newLoanAccount!.LoanApplication.LoanProduct.InterestRate,
                newLoanAccount.LoanApplication.LoanProduct.InterestMethod,
                newLoanAccount.LoanApplication.LoanProduct.InterestCalculationMethod,
                newLoanAccount.TenureUnit, newLoanAccount.TenureValue,
                newLoanAccount.LoanApplication.LoanProduct.RepaymentPeriod,
                newLoanAccount.LoanApplication.LoanProduct.DaysInYear,
                newLoanAccount.RepaymentCommencementDate.UtcDateTime);

            var paymentSchedules = loanhelper.GetAmortizationTable(newLoanAccount.LoanApplication.LoanProduct.InterestCalculationMethod);
            var firstPayment = paymentSchedules.FirstOrDefault();

            loanTopup.NewInterestBalance = loanhelper.InterestEarned;

            if (txn.DisbursementMode == LoanDisbursementMode.SPECIAL_DEPOSIT)
            {
                LedgerAccount CUST_DEPOSIT_ACC = dbContext.LedgerAccounts.FirstOrDefault(c => c.Id == txn.SpecialDepositAccount.DepositAccountId);


                //DR Disbursement.DisbursementAccount.LedgerAccount 1,000,000
                //CR Disbursement.SpecialDepositAccount.DepositAccount 1,000,000
                journal.JournalEntries.Add(new JournalEntry
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = TransactionEntryType.DEBIT,
                    AccountId = txn.DisbursementAccount!.LedgerAccountId,
                    Account = txn.DisbursementAccount!.LedgerAccount,
                    Debit = txn.Amount,
                    TransactionDate = request.TransactionDate
                });

                journal.JournalEntries.Add(new JournalEntry
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = TransactionEntryType.CREDIT,
                    AccountId = CUST_DEPOSIT_ACC!.Id,
                    Account = CUST_DEPOSIT_ACC!,
                    Credit = txn.Amount,
                    TransactionDate = request.TransactionDate
                });
            }
            else if (txn.DisbursementMode == LoanDisbursementMode.BANK_TRANSFER)
            {

                //DR Disbursement.DisbursementAccount.LedgerAccount 1,000,000
                //CR Disbursement.CustomerBankAccount.LedgerAccount 1,000,000
                var customerBankAccount = dbContext.CustomerBankAccounts.FirstOrDefault(p => p.Id == txn.CustomerBankAccountId);
                LedgerAccount CUST_BANK_ACC = dbContext.LedgerAccounts.FirstOrDefault(c => c.Id == customerBankAccount.LedgerAccountId);

                journal.JournalEntries.Add(new JournalEntry
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = TransactionEntryType.DEBIT,
                    AccountId = txn.DisbursementAccount!.LedgerAccountId,
                    Account = txn.DisbursementAccount!.LedgerAccount,
                    Debit = txn.Amount,
                    TransactionDate = request.TransactionDate
                });

                journal.JournalEntries.Add(new JournalEntry
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = TransactionEntryType.CREDIT,
                    AccountId = CUST_BANK_ACC!.Id,
                    Account = CUST_BANK_ACC,
                    Credit = txn.Amount,
                    TransactionDate = request.TransactionDate
                });
            }


            //loan tracking:principal Product
            //DR LOAN_PROD_PRINCIPAL_CONTROL 1,000,000
            //CR LoanProduct.PrincipalAccount 1,000,000
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = LOAN_PROD_PRINCIPAL_CONTROL!.Id,
                Account = LOAN_PROD_PRINCIPAL_CONTROL,
                Debit = txn.Amount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = LOAN_ACC_PROD_PRINCIPAL!.Id,
                Account = LOAN_ACC_PROD_PRINCIPAL,
                Credit = txn.Amount,
                TransactionDate = request.TransactionDate
            });



            //loan tracking:principal Account
            //DR LOAN_ACC_PRINCIPAL_CONTROL 1,000,000
            //CR LoanAccount.PrincipalBalanceAccount 1,000,000
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = LOAN_ACC_PRINCIPAL_CONTROL!.Id,
                Account = LOAN_ACC_PRINCIPAL_CONTROL,
                Debit = loanTopup!.NewPrincipalBalance,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = LOAN_ACC_PRINCIPAL_BALANCE!.Id,
                Account = LOAN_ACC_PRINCIPAL_BALANCE,
                Credit = loanTopup!.NewPrincipalBalance,
                TransactionDate = request.TransactionDate
            });


            //loan tracking:unearned interest Product
            //DR LOAN_PROD_UNEARNED_INTEREST_CONTROL 100,000
            //CR Product.UnearnedInterest 100,000
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = LOAN_PROD_UNEARNED_INTEREST_CONTROL!.Id,
                Account = LOAN_PROD_UNEARNED_INTEREST_CONTROL,
                Debit = loanhelper.InterestEarned,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = LOAN_ACC_PROD_UNEARNED_INTEREST!.Id,
                Account = LOAN_ACC_PROD_UNEARNED_INTEREST,
                Credit = loanhelper.InterestEarned,
                TransactionDate = request.TransactionDate
            });



            //loan tracking:unearned interest Account
            //DR LOAN_ACC_UNEARNED_INTEREST_CONTROL 100,000
            //CR Account.UnearnedInterest 100,000
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = LOAN_ACC_UNEARNED_INTEREST_CONTROL!.Id,
                Account = LOAN_ACC_UNEARNED_INTEREST_CONTROL,
                Debit = loanhelper.InterestEarned,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = LOAN_ACC_UNEARNED_INTEREST!.Id,
                Account = LOAN_ACC_UNEARNED_INTEREST,
                Credit = loanhelper.InterestEarned,
                TransactionDate = request.TransactionDate
            });


            if (charges.Any())
            {
                // Get total charge amount
                foreach (var charge in charges)
                {
                    journal.JournalEntries.Add(new JournalEntry
                    {
                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                        EntryType = TransactionEntryType.DEBIT,
                        AccountId = LOAN_PROD_CHARGE_ACCRUAL_CONTROL!.Id,
                        Account = LOAN_PROD_CHARGE_ACCRUAL_CONTROL,
                        Debit = charge.TotalCharge,
                        TransactionDate = request.TransactionDate
                    });

                    journal.JournalEntries.Add(new JournalEntry
                    {
                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                        EntryType = TransactionEntryType.CREDIT,
                        AccountId = LOAN_ACC_PROD_CHARGES_ACCRUAL!.Id,
                        Account = LOAN_ACC_PROD_CHARGES_ACCRUAL,
                        Credit = charge.TotalCharge,
                        TransactionDate = request.TransactionDate
                    });

                    journal.JournalEntries.Add(new JournalEntry
                    {
                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                        EntryType = TransactionEntryType.DEBIT,
                        AccountId = LOAN_ACC_CHARGE_ACCRUAL_CONTROL!.Id,
                        Account = LOAN_ACC_CHARGE_ACCRUAL_CONTROL,
                        Debit = charge.TotalCharge,
                        TransactionDate = request.TransactionDate
                    });

                    journal.JournalEntries.Add(new JournalEntry
                    {
                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                        EntryType = TransactionEntryType.CREDIT,
                        AccountId = LOAN_ACC_CHARGES_ACCRUAL!.Id,
                        Account = LOAN_ACC_CHARGES_ACCRUAL,
                        Credit = charge.TotalCharge,
                        TransactionDate = request.TransactionDate
                    });
                }
            }

            dbContext.TransactionJournals.Add(journal);
            dbContext.SaveChanges();

            journal.Post();

            txn.TransactionJournalId = journal.Id;
            txn.IsProcessed = true;
            txn.ProcessedDate = request.TransactionDate;
            txn.Status = TransactionStatus.SUCCESS;

            dbContext.Update(journal);
            dbContext.SaveChanges();

            rsp.TransactionDate = request.TransactionDate;
            rsp.TransactionType = request.TransactionType;
            rsp.EntityId = request.EntityId;

            rsp.IsApproved = true;
            rsp.ApprovedOn = request.TransactionDate;
            rsp.IsPosted = true;
            rsp.PostedOn = request.TransactionDate;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex!.Message);
            rsp.ErrorDate = DateTime.Now;
            rsp.ErrorDetails = ex!.Message;
            rsp.ErrorFlag = true;
        }

        return rsp;
    }


    private LoanTransactionViewModel ProcessInitializeLoanOffset(LoanTransactionCommand request, CancellationToken cancellationToken)
    {
        LoanTransactionViewModel rsp = new();

        try
        {
            TransactionJournal journal = new()
            {
                TransactionNo = NHiloHelper.GetNextKey(nameof(TransactionJournal)).ToString(),
                Title = $"Loan Offset Initialization",
                TransactionType = request.TransactionType,
                TransactionDate = request.TransactionDate,
                DocumentRef = nameof(LoanAccount),
                DocumentRefId = request.LoanAccountId,
                EntityRef = nameof(LoanOffset),
                EntityRefId = request.EntityId
            };

            //var txn = dbContext.LoanDisbursements.Where(p => p.Id == request.EntityId)
            //    .Include(p => p.DisbursementAccount).ThenInclude(p => p.LedgerAccount)
            //    .Include(p => p.CustomerBankAccount).ThenInclude(p => p.LedgerAccount)
            //    .Include(p => p.SpecialDepositAccount).ThenInclude(p => p.DepositAccount)
            //    .FirstOrDefault();

            var txn = dbContext.LoanOffsets.Where(p => p.Id == request.EntityId)
               .Include(p => p.LoanAccount).ThenInclude(p => p.LoanApplication).ThenInclude(p => p.LoanProduct)
               //.Include(p => p.CustomerBankAccount).ThenInclude(p => p.LedgerAccount)
               //.Include(p => p.SpecialDepositAccount).ThenInclude(p => p.DepositAccount)
               //.Include(p => p.SavingsAccount).ThenInclude(p => p.LedgerDepositAccount)
               //.Include(p => p.SavingsAccount).ThenInclude(p => p.DepositProduct).ThenInclude(p => p.ProductDepositAccount)
               .FirstOrDefault();


            //var charges = dbContext.LoanDisbursementCharges.Where(p => p.LoanDisbursementId == request.EntityId);

            var loanAccount = dbContext.LoanAccounts.Where(r => r.Id == request.LoanAccountId)
                .Include(p => p.LoanApplication).ThenInclude(p => p.LoanProduct)
                .Include(c => c.Customer).ThenInclude(c => c.CashAccount)
                .FirstOrDefault();

            var _loanAccountQuery = dbContext.LoanAccounts.Where(r => r.Id == request.LoanAccountId);

            var _loanProductQuery = _loanAccountQuery.Include(x => x.LoanApplication)
                .ThenInclude(x => x.LoanProduct);



            // loanAccount.PrincipalBalanceAccountId
            LedgerAccount LOAN_ACC_PRINCIPAL_BALANCE = _loanAccountQuery.Include(x => x.PrincipalBalanceAccount)
                .Select(c => c.PrincipalBalanceAccount).FirstOrDefault();

            // loanAccount.UnearnedInterestAccountId
            LedgerAccount LOAN_ACC_UNEARNED_INTEREST = _loanAccountQuery.Include(x => x.UnearnedInterestAccount)
                .Select(c => c.UnearnedInterestAccount).FirstOrDefault();

            // loanAccount.LoanApplication.LoanProduct.PrincipalAccount.Id
            LedgerAccount LOAN_ACC_PROD_PRINCIPAL = _loanProductQuery.ThenInclude(x => x.PrincipalAccount)
                .Select(c => c.LoanApplication.LoanProduct.PrincipalAccount)
                .FirstOrDefault();


            // "loanAccount.LoanApplication.LoanProduct.UnearnedInterestAccountId"
            LedgerAccount LOAN_ACC_PROD_UNEARNED_INTEREST = _loanProductQuery.ThenInclude(x => x.UnearnedInterestAccount)
                .Select(c => c.LoanApplication.LoanProduct.UnearnedInterestAccount)
                .FirstOrDefault();

            LedgerAccount LOAN_PROD_PRINCIPAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PROD_PRINCIPAL_CONTROL.ToString());

            LedgerAccount LOAN_ACC_PRINCIPAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_ACC_PRINCIPAL_CONTROL.ToString());

            LedgerAccount LOAN_PROD_UNEARNED_INTEREST_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_PROD_UNEARNED_INTEREST_CONTROL.ToString());


            LedgerAccount LOAN_ACC_UNEARNED_INTEREST_CONTROL =
               dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.LOAN_ACC_UNEARNED_INTEREST_CONTROL.ToString());

            var loanhelper = new LoanHelper(loanAccount!.Principal, loanAccount.LoanApplication.LoanProduct.InterestRate,
                loanAccount.LoanApplication.LoanProduct.InterestMethod,
                loanAccount.LoanApplication.LoanProduct.InterestCalculationMethod,
                loanAccount.TenureUnit, loanAccount.TenureValue,
                loanAccount.LoanApplication.LoanProduct.RepaymentPeriod,
                loanAccount.LoanApplication.LoanProduct.DaysInYear,
                loanAccount.RepaymentCommencementDate.UtcDateTime);


            //loan tracking:principal Product
            //DR LOAN_PROD_PRINCIPAL_CONTROL 1,000,000
            //CR LoanProduct.PrincipalAccount 1,000,000
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = LOAN_PROD_PRINCIPAL_CONTROL!.Id,
                Account = LOAN_PROD_PRINCIPAL_CONTROL,
                Debit = txn.OffsetAmount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = LOAN_ACC_PROD_PRINCIPAL!.Id,
                Account = LOAN_ACC_PROD_PRINCIPAL,
                Credit = txn.OffsetAmount,
                TransactionDate = request.TransactionDate
            });



            //loan tracking:principal Account
            //DR LOAN_ACC_PRINCIPAL_CONTROL 1,000,000
            //CR LoanAccount.PrincipalBalanceAccount 1,000,000
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = LOAN_ACC_PRINCIPAL_CONTROL!.Id,
                Account = LOAN_ACC_PRINCIPAL_CONTROL,
                Debit = txn.OffsetAmount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = LOAN_ACC_PRINCIPAL_BALANCE!.Id,
                Account = LOAN_ACC_PRINCIPAL_BALANCE,
                Credit = txn.OffsetAmount,
                TransactionDate = request.TransactionDate
            });


            //loan tracking:unearned interest Product
            //DR LOAN_PROD_UNEARNED_INTEREST_CONTROL 100,000
            //CR Product.UnearnedInterest 100,000
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = LOAN_PROD_UNEARNED_INTEREST_CONTROL!.Id,
                Account = LOAN_PROD_UNEARNED_INTEREST_CONTROL,
                Debit = loanhelper.InterestEarned,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = LOAN_ACC_PROD_UNEARNED_INTEREST!.Id,
                Account = LOAN_ACC_PROD_UNEARNED_INTEREST,
                Credit = loanhelper.InterestEarned,
                TransactionDate = request.TransactionDate
            });



            //loan tracking:unearned interest Account
            //DR LOAN_ACC_UNEARNED_INTEREST_CONTROL 100,000
            //CR Account.UnearnedInterest 100,000
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = LOAN_ACC_UNEARNED_INTEREST_CONTROL!.Id,
                Account = LOAN_ACC_UNEARNED_INTEREST_CONTROL,
                Debit = loanhelper.InterestEarned,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = LOAN_ACC_UNEARNED_INTEREST!.Id,
                Account = LOAN_ACC_UNEARNED_INTEREST,
                Credit = loanhelper.InterestEarned,
                TransactionDate = request.TransactionDate
            });


            dbContext.TransactionJournals.Add(journal);
            dbContext.SaveChanges();

            journal.Post();

            txn.TransactionJournalId = journal.Id;
            txn.IsProcessed = true;
            txn.ProcessedDate = request.TransactionDate;
            txn.Status = TransactionStatus.SUCCESS;

            dbContext.Update(journal);
            dbContext.SaveChanges();

            rsp.TransactionDate = request.TransactionDate;
            rsp.TransactionType = request.TransactionType;
            rsp.EntityId = request.EntityId;

            rsp.IsApproved = true;
            rsp.ApprovedOn = request.TransactionDate;
            rsp.IsPosted = true;
            rsp.PostedOn = request.TransactionDate;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex!.Message);
            rsp.ErrorDate = DateTime.Now;
            rsp.ErrorDetails = ex!.Message;
            rsp.ErrorFlag = true;
        }

        return rsp;
    }


}
