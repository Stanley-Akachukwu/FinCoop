using AP.ChevronCoop.AppCore.Services.Helpers;
using AP.ChevronCoop.AppDomain.Deposits.DepositTransactions;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.JournalEntries;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.MasterData.Banks;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.Pkcs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Security.Policy;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsCashAdditions;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositCashAdditions;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositWithdrawals;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestAdditions;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositInterestAdditions;
using AP.ChevronCoop.Entities.Payroll;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositFundTransfer;

namespace AP.ChevronCoop.AppCore.Deposits.DepositTransactions;

public class DepositTransactionCommandHandler :
    IRequestHandler<DepositTransactionCommand, CommandResult<DepositTransactionViewModel>>
{
    private readonly ChevronCoopDbContext dbContext;
    private readonly IEmailService _emailService;
    private readonly ILogger logger;
    private readonly IMapper _mapper;
    private readonly CoreAppSettings _options;

    public DepositTransactionCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<DepositTransactionCommandHandler> _logger, IMapper mapper, IEmailService emailService,
        IOptions<CoreAppSettings> options)
    {
        dbContext = appDbContext;
        this.logger = _logger;
        _mapper = mapper;
        _emailService = emailService;
        _options = options.Value;
    }

    public async Task<CommandResult<DepositTransactionViewModel>> Handle(DepositTransactionCommand request,
        CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<DepositTransactionViewModel>();

        var response = new DepositTransactionViewModel();
        switch (request.TransactionType)
        {
            case TransactionType.FIXED_DEPOSIT_APPLICATION_FUNDING:
                response = await ProcessFixedDepositApplicationFunding(request, cancellationToken);
                break;

            case TransactionType.SPECIAL_DEPOSIT_APPLICATION_FUNDING:
                response = ProcessSpecialDepositApplicationFunding(request, cancellationToken);
                break;

            case TransactionType.SAVING_DEPOSIT_APPLICATION_FUNDING:
                response = ProcessSavingsDepositApplicationFunding(request, cancellationToken);
                break;

            case TransactionType.FIXED_DEPOSIT_PAYROLL_FUNDING:
                response = ProcessFixedDepositPayrollFunding(request, cancellationToken);
                break;

            case TransactionType.SPECIAL_DEPOSIT_PAYROLL_FUNDING:
                response = ProcessSpecialDepositPayrollFunding(request, cancellationToken);
                break;

            case TransactionType.SAVINGS_DEPOSIT_PAYROLL_FUNDING:
                response = ProcessSavingsDepositPayrollFunding(request, cancellationToken);
                break;

            case TransactionType.SPECIAL_DEPOSIT_CASH_ADDITION:
                response = ProcessSpecialDepositCashAddition(request, cancellationToken);
                break;

            case TransactionType.SPECIAL_DEPOSIT_INTEREST_ADDITION:
                response = ProcessSpecialDepositInterestAddition(request, cancellationToken);
                break;

            case TransactionType.SPECIAL_DEPOSIT_FUND_TRANSFER:
                response = ProcessSpecialDepositFundTransfer(request, cancellationToken);
                break;

            case TransactionType.SPECIAL_DEPOSIT_WITHDRAWAL:
                response = ProcessSpecialDepositWithdrawal(request, cancellationToken);
                break;

            case TransactionType.SAVINGS_WITHDRAWAL:
                response = ProcessSavingsWithdrawal(request, cancellationToken);
                break;

            case TransactionType.SAVINGS_TRANSFER:
                response = ProcessSavingsTransfer(request, cancellationToken);
                break;

            case TransactionType.SAVINGS_CASH_ADDITION:
                response = ProcessSavingsCashAddition(request, cancellationToken);
                break;

            case TransactionType.FIXED_DEPOSIT_LIQUIDATION:
                response = await ProcessFixedDepositLiquidation(request, cancellationToken);
                break;
            case TransactionType.FIXED_DEPOSIT_INTEREST_ADDITION:
                response = ProcessFixedDepositInterestAddition(request, cancellationToken);
                break;
        }

        rsp.Response = response;
        return rsp;
    }





    private async Task<DepositTransactionViewModel> ProcessFixedDepositApplicationFunding(DepositTransactionCommand request,
        CancellationToken cancellationToken)
    {

        /*
          * 

        v1 FD APPLICATION ->BANK TRANSFER

        FD_DR FD_COY_BANK_CONTROL
        FD_CR Customer cash account fundingAmount

        FD_DR Customer cash account fundingAmount
        FD_CR Company bank account fundingAmount 

        FD_DR FD_PROD_CONTROL 
        FD_CR Product deposit account fundingAmount

        FD_DR FD_CUST_BANK_CONTROL  
        FD_CR Account deposit account fundingAmount



        v1 FD APPLICATION ->SPECIAL DEPOSIT

        DR SD Product Deposit Account
        CR FD Product deposit account fundingAmount

        DR SD Deposit Account
        CR FD Account deposit account fundingAmount
          * 
          * 
          */

        DepositTransactionViewModel rsp = new DepositTransactionViewModel();


        try
        {

            TransactionJournal journal = new TransactionJournal();
            journal.TransactionNo = NHiloHelper.GetNextKey(nameof(TransactionJournal)).ToString();
            journal.Title = $"Fixed Deposit Application Funding";
            journal.TransactionType = request.TransactionType;
            journal.TransactionDate = request.TransactionDate;
            journal.DocumentRef = nameof(FixedDepositAccount);
            journal.DocumentRefId = request.EntityId;
            journal.EntityRef = nameof(FixedDepositAccount);
            journal.EntityRefId = request.DepositAccountId;


            var depositAccount = await dbContext.FixedDepositAccounts.Where(r => r.Id == request.DepositAccountId)
                .Include(p => p.DepositAccount)
                .Include(p => p.DepositProduct)
                .ThenInclude(p => p.BankDepositAccount)
                .ThenInclude(l => l.LedgerAccount)
                .Include(p => p.DepositProduct).ThenInclude(p => p.ProductDepositAccount)
                .Include(p => p.Customer).ThenInclude(p => p.CashAccount)
                .Include(p => p.Application).ThenInclude(p => p.SpecialDepositFundingSourceAccount)
                .ThenInclude(p => p.DepositAccount)
                .Include(p => p.Application).ThenInclude(p => p.SpecialDepositFundingSourceAccount)
                .ThenInclude(p => p.DepositProduct).ThenInclude(p => p.ProductDepositAccount)
                .FirstOrDefaultAsync(cancellationToken);


            LedgerAccount FD_COY_BANK_CONTROL =
                await dbContext.LedgerAccounts.FirstOrDefaultAsync(p => p.Code == ControlAccounts.FD_COY_BANK_CONTROL.ToString(), cancellationToken);
            LedgerAccount FD_PROD_CONTROL =
               await dbContext.LedgerAccounts.FirstOrDefaultAsync(p => p.Code == ControlAccounts.FD_PROD_CONTROL.ToString(), cancellationToken);
            LedgerAccount FD_CUST_BANK_CONTROL =
                await dbContext.LedgerAccounts.FirstOrDefaultAsync(p => p.Code == ControlAccounts.FD_CUST_BANK_CONTROL.ToString(), cancellationToken);


            LedgerAccount CUST_CASH_ACC = depositAccount.Customer.CashAccount;
            LedgerAccount COY_BANK_ACC = depositAccount.DepositProduct.BankDepositAccount.LedgerAccount;
            LedgerAccount PROD_DEPOSIT_ACC = depositAccount.DepositProduct.ProductDepositAccount;
            LedgerAccount CUST_DEPOSIT_ACC = depositAccount.DepositAccount;
            LedgerAccount SD_PROD_FUNDING_ACC = depositAccount.Application.SpecialDepositFundingSourceAccount?.DepositProduct?.ProductDepositAccount;
            LedgerAccount SD_FUNDING_ACC = depositAccount.Application.SpecialDepositFundingSourceAccount?.DepositAccount;


            switch (depositAccount.Application.ModeOfPayment)
            {

                case DepositFundingSourceType.SPECIAL_DEPOSIT:
                    {

                        //DR SD Product Deposit Account
                        //CR FD Product deposit account fundingAmount
                        journal.JournalEntries.Add(new JournalEntry
                        {
                            TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                            EntryType = TransactionEntryType.DEBIT,
                            AccountId = SD_PROD_FUNDING_ACC.Id,
                            Account = SD_PROD_FUNDING_ACC,
                            Debit = depositAccount.Amount,
                            TransactionDate = request.TransactionDate
                        });

                        journal.JournalEntries.Add(new JournalEntry
                        {
                            TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                            EntryType = TransactionEntryType.CREDIT,
                            AccountId = PROD_DEPOSIT_ACC.Id,
                            Account = PROD_DEPOSIT_ACC,
                            Credit = depositAccount.Amount,
                            TransactionDate = request.TransactionDate
                        });


                        //DR SD Deposit Account
                        //CR FD Account deposit account fundingAmount
                        journal.JournalEntries.Add(new JournalEntry
                        {
                            TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                            EntryType = TransactionEntryType.DEBIT,
                            AccountId = SD_FUNDING_ACC.Id,
                            Account = SD_FUNDING_ACC,
                            Debit = depositAccount.Amount,
                            TransactionDate = request.TransactionDate
                        });

                        journal.JournalEntries.Add(new JournalEntry
                        {
                            TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                            EntryType = TransactionEntryType.CREDIT,
                            AccountId = CUST_DEPOSIT_ACC.Id,
                            Account = CUST_DEPOSIT_ACC,
                            Credit = depositAccount.Amount,
                            TransactionDate = request.TransactionDate
                        });


                        break;
                    }


                case DepositFundingSourceType.BANK_TRANSFER:
                    {

                        //DR FD_COY_BANK_CONTROL
                        //CR Customer cash account fundingAmount
                        journal.JournalEntries.Add(new JournalEntry
                        {
                            TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                            EntryType = TransactionEntryType.DEBIT,
                            AccountId = FD_COY_BANK_CONTROL.Id,
                            Account = FD_COY_BANK_CONTROL,
                            Debit = depositAccount.Amount,
                            TransactionDate = request.TransactionDate
                        });

                        journal.JournalEntries.Add(new JournalEntry
                        {
                            TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                            EntryType = TransactionEntryType.CREDIT,
                            AccountId = CUST_CASH_ACC.Id,
                            Account = CUST_CASH_ACC,
                            Credit = depositAccount.Amount,
                            TransactionDate = request.TransactionDate
                        });


                        //DR Customer cash account fundingAmount
                        //CR Company bank account fundingAmount
                        journal.JournalEntries.Add(new JournalEntry
                        {
                            TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                            EntryType = TransactionEntryType.DEBIT,
                            AccountId = CUST_CASH_ACC.Id,
                            Account = CUST_CASH_ACC,
                            Debit = depositAccount.Amount,
                            TransactionDate = request.TransactionDate
                        });

                        journal.JournalEntries.Add(new JournalEntry
                        {
                            TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                            EntryType = TransactionEntryType.CREDIT,
                            AccountId = COY_BANK_ACC.Id,
                            Account = COY_BANK_ACC,
                            Credit = depositAccount.Amount,
                            TransactionDate = request.TransactionDate
                        });

                        //DR FD_PROD_CONTROL
                        //CR Product deposit account fundingAmount
                        journal.JournalEntries.Add(new JournalEntry
                        {
                            TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                            EntryType = TransactionEntryType.DEBIT,
                            AccountId = FD_PROD_CONTROL.Id,
                            Account = FD_PROD_CONTROL,
                            Debit = depositAccount.Amount,
                            TransactionDate = request.TransactionDate
                        });

                        journal.JournalEntries.Add(new JournalEntry
                        {
                            TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                            EntryType = TransactionEntryType.CREDIT,
                            AccountId = PROD_DEPOSIT_ACC.Id,
                            Account = PROD_DEPOSIT_ACC,
                            Credit = depositAccount.Amount,
                            TransactionDate = request.TransactionDate
                        });



                        //DR FD_CUST_BANK_CONTROL fundingAmount
                        //CR Account deposit account fundingAmount
                        journal.JournalEntries.Add(new JournalEntry
                        {
                            TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                            EntryType = TransactionEntryType.DEBIT,
                            AccountId = FD_CUST_BANK_CONTROL.Id,
                            Account = FD_CUST_BANK_CONTROL,
                            Debit = depositAccount.Amount,
                            TransactionDate = request.TransactionDate
                        });

                        journal.JournalEntries.Add(new JournalEntry
                        {
                            TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                            EntryType = TransactionEntryType.CREDIT,
                            AccountId = CUST_DEPOSIT_ACC.Id,
                            Account = CUST_DEPOSIT_ACC,
                            Credit = depositAccount.Amount,
                            TransactionDate = request.TransactionDate
                        });


                        break;
                    }

                default:
                    {
                        break;
                    }
            }



            if (!string.IsNullOrEmpty(depositAccount.ParentAccountId))
            {
                //DR FD_PROD_CONTROL
                //CR Product deposit account fundingAmount
                journal.JournalEntries.Add(new JournalEntry
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = TransactionEntryType.DEBIT,
                    AccountId = FD_PROD_CONTROL.Id,
                    Account = FD_PROD_CONTROL,
                    Debit = depositAccount.Amount,
                    TransactionDate = request.TransactionDate
                });

                journal.JournalEntries.Add(new JournalEntry
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = TransactionEntryType.CREDIT,
                    AccountId = PROD_DEPOSIT_ACC.Id,
                    Account = PROD_DEPOSIT_ACC,
                    Credit = depositAccount.Amount,
                    TransactionDate = request.TransactionDate
                });



                //DR FD_CUST_BANK_CONTROL fundingAmount
                //CR Account deposit account fundingAmount
                journal.JournalEntries.Add(new JournalEntry
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = TransactionEntryType.DEBIT,
                    AccountId = FD_CUST_BANK_CONTROL.Id,
                    Account = FD_CUST_BANK_CONTROL,
                    Debit = depositAccount.Amount,
                    TransactionDate = request.TransactionDate
                });

                journal.JournalEntries.Add(new JournalEntry
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = TransactionEntryType.CREDIT,
                    AccountId = CUST_DEPOSIT_ACC.Id,
                    Account = CUST_DEPOSIT_ACC,
                    Credit = depositAccount.Amount,
                    TransactionDate = request.TransactionDate
                });

            }


            dbContext.TransactionJournals.Add(journal);
            await dbContext.SaveChangesAsync(cancellationToken);

            journal.Post();

            dbContext.Update(journal);
            await dbContext.SaveChangesAsync(cancellationToken);

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

    private DepositTransactionViewModel ProcessSpecialDepositApplicationFunding(DepositTransactionCommand request,
        CancellationToken cancellationToken)
    {
        /*
         * 

        DR SD_COY_BANK_CONTROL
        CR Customer cash account fundingAmount

        DR Customer cash account fundingAmount
        CR Company bank account fundingAmount 

        DR SD_PROD_CONTROL 
        CR Product deposit account fundingAmount

        DR SD_CUST_BANK_CONTROL  
        CR Account deposit account fundingAmount

         * 
         * 
         */

        DepositTransactionViewModel rsp = new DepositTransactionViewModel();




        try
        {

            TransactionJournal journal = new TransactionJournal();
            journal.TransactionNo = NHiloHelper.GetNextKey(nameof(TransactionJournal)).ToString();
            journal.Title = $"Special Deposit Application Funding";
            journal.TransactionType = request.TransactionType;
            journal.TransactionDate = request.TransactionDate;
            journal.DocumentRef = nameof(SpecialDepositAccount);
            journal.DocumentRefId = request.EntityId;
            journal.EntityRef = nameof(SpecialDepositAccount);
            journal.EntityRefId = request.DepositAccountId;


            var depositAccount = dbContext.SpecialDepositAccounts.Where(r => r.Id == request.DepositAccountId)
                .Include(p => p.DepositProduct)
                .ThenInclude(p => p.BankDepositAccount)
                .ThenInclude(l => l.LedgerAccount)
                .Include(p => p.DepositProduct).ThenInclude(p => p.ProductDepositAccount)
                .Include(p => p.Customer).ThenInclude(p => p.CashAccount)
                .FirstOrDefault();


            LedgerAccount SD_COY_BANK_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_COY_BANK_CONTROL.ToString());
            LedgerAccount SD_PROD_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_PROD_CONTROL.ToString());
            LedgerAccount SD_CUST_BANK_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_CUST_BANK_CONTROL.ToString());


            LedgerAccount CUST_CASH_ACC = depositAccount.Customer.CashAccount;
            LedgerAccount COY_BANK_ACC = depositAccount.DepositProduct.BankDepositAccount.LedgerAccount;
            LedgerAccount PROD_DEPOSIT_ACC = depositAccount.DepositProduct.ProductDepositAccount;
            LedgerAccount CUST_DEPOSIT_ACC = depositAccount.DepositAccount;

            //DR SD_COY_BANK_CONTROL
            //CR Customer cash account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = SD_COY_BANK_CONTROL.Id,
                Account = SD_COY_BANK_CONTROL,
                Debit = depositAccount.FundingAmount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = CUST_CASH_ACC.Id,
                Account = CUST_CASH_ACC,
                Credit = depositAccount.FundingAmount,
                TransactionDate = request.TransactionDate
            });


            //DR Customer cash account fundingAmount
            //CR Company bank account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = CUST_CASH_ACC.Id,
                Account = CUST_CASH_ACC,
                Debit = depositAccount.FundingAmount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = COY_BANK_ACC.Id,
                Account = COY_BANK_ACC,
                Credit = depositAccount.FundingAmount,
                TransactionDate = request.TransactionDate
            });

            //DR SD_PROD_CONTROL
            //CR Product deposit account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = SD_PROD_CONTROL.Id,
                Account = SD_PROD_CONTROL,
                Debit = depositAccount.FundingAmount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = PROD_DEPOSIT_ACC.Id,
                Account = PROD_DEPOSIT_ACC,
                Credit = depositAccount.FundingAmount,
                TransactionDate = request.TransactionDate
            });



            //DR SD_CUST_BANK_CONTROL fundingAmount
            //CR Account deposit account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = SD_CUST_BANK_CONTROL.Id,
                Account = SD_CUST_BANK_CONTROL,
                Debit = depositAccount.FundingAmount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = CUST_DEPOSIT_ACC.Id,
                Account = CUST_DEPOSIT_ACC,
                Credit = depositAccount.FundingAmount,
                TransactionDate = request.TransactionDate
            });



            dbContext.TransactionJournals.Add(journal);
            dbContext.SaveChanges();

            journal.Post();

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

    private DepositTransactionViewModel ProcessSavingsDepositApplicationFunding(DepositTransactionCommand request,
        CancellationToken cancellationToken)
    {

        /*
         * 

        v3

        DR SD_COY_BANK_CONTROL
        CR Customer cash account fundingAmount

        DR Customer cash account fundingAmount
        CR Company bank account fundingAmount 

        DR SD_PROD_CONTROL 
        CR Product deposit account fundingAmount

        DR SD_CUST_BANK_CONTROL  
        CR Account deposit account fundingAmount

         * 
         * 
         */

        DepositTransactionViewModel rsp = new DepositTransactionViewModel();


        try
        {

            TransactionJournal journal = new TransactionJournal();
            journal.TransactionNo = NHiloHelper.GetNextKey(nameof(TransactionJournal)).ToString();
            journal.Title = $"Savings Application Funding";
            journal.TransactionType = request.TransactionType;
            journal.TransactionDate = request.TransactionDate;
            journal.DocumentRef = nameof(SavingsAccount);
            journal.DocumentRefId = request.EntityId;
            journal.EntityRef = nameof(SavingsAccount);
            journal.EntityRefId = request.DepositAccountId;


            var depositAccount = dbContext.SavingsAccounts.Where(r => r.Id == request.DepositAccountId)
                .Include(p => p.DepositProduct)
                .ThenInclude(p => p.BankDepositAccount)
                .ThenInclude(l => l.LedgerAccount)
                .Include(p => p.DepositProduct).ThenInclude(p => p.ProductDepositAccount)
                .Include(p => p.Customer).ThenInclude(p => p.CashAccount)
                .FirstOrDefault();


            LedgerAccount SD_COY_BANK_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_COY_BANK_CONTROL.ToString());
            LedgerAccount SD_PROD_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_PROD_CONTROL.ToString());
            LedgerAccount SD_CUST_BANK_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_CUST_BANK_CONTROL.ToString());


            LedgerAccount CUST_CASH_ACC = depositAccount.Customer.CashAccount;
            LedgerAccount COY_BANK_ACC = depositAccount.DepositProduct.BankDepositAccount.LedgerAccount;
            LedgerAccount PROD_DEPOSIT_ACC = depositAccount.DepositProduct.ProductDepositAccount;
            LedgerAccount CUST_DEPOSIT_ACC = depositAccount.LedgerDepositAccount;

            //DR SD_COY_BANK_CONTROL
            //CR Customer cash account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = SD_COY_BANK_CONTROL.Id,
                Account = SD_COY_BANK_CONTROL,
                Debit = depositAccount.PayrollAmount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = CUST_CASH_ACC.Id,
                Account = CUST_CASH_ACC,
                Credit = depositAccount.PayrollAmount,
                TransactionDate = request.TransactionDate
            });


            //DR Customer cash account fundingAmount
            //CR Company bank account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = CUST_CASH_ACC.Id,
                Account = CUST_CASH_ACC,
                Debit = depositAccount.PayrollAmount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = COY_BANK_ACC.Id,
                Account = COY_BANK_ACC,
                Credit = depositAccount.PayrollAmount,
                TransactionDate = request.TransactionDate
            });

            //DR SD_PROD_CONTROL
            //CR Product deposit account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = SD_PROD_CONTROL.Id,
                Account = SD_PROD_CONTROL,
                Debit = depositAccount.PayrollAmount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = PROD_DEPOSIT_ACC.Id,
                Account = PROD_DEPOSIT_ACC,
                Credit = depositAccount.PayrollAmount,
                TransactionDate = request.TransactionDate
            });



            //DR SD_CUST_BANK_CONTROL fundingAmount
            //CR Account deposit account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = SD_CUST_BANK_CONTROL.Id,
                Account = SD_CUST_BANK_CONTROL,
                Debit = depositAccount.PayrollAmount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = CUST_DEPOSIT_ACC.Id,
                Account = CUST_DEPOSIT_ACC,
                Credit = depositAccount.PayrollAmount,
                TransactionDate = request.TransactionDate
            });



            dbContext.TransactionJournals.Add(journal);
            dbContext.SaveChanges();

            journal.Post();

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

    private DepositTransactionViewModel ProcessFixedDepositPayrollFunding(DepositTransactionCommand request,
        CancellationToken cancellationToken)
    {
        return null;
    }


    private DepositTransactionViewModel ProcessSpecialDepositPayrollFunding(DepositTransactionCommand request,
        CancellationToken cancellationToken)
    {
        /*
        * 

       DR SD_PAYROLL_CONTROL
       CR Customer cash account fundingAmount

       DR Customer cash account fundingAmount
       CR Company bank account fundingAmount 

       DR SD_PROD_CONTROL 
       CR Product deposit account fundingAmount

       DR SD_CUST_BANK_CONTROL  
       CR Account deposit account fundingAmount

        * 
        * 
        */

        DepositTransactionViewModel rsp = new DepositTransactionViewModel();




        try
        {

            TransactionJournal journal = new TransactionJournal();
            journal.TransactionNo = NHiloHelper.GetNextKey(nameof(TransactionJournal)).ToString();
            journal.Title = $"Special Deposit Payroll Funding";
            journal.TransactionType = request.TransactionType;
            journal.TransactionDate = request.TransactionDate;
            journal.DocumentRef = nameof(SpecialDepositAccount);
            journal.DocumentRefId = request.DepositAccountId;
            journal.EntityRef = nameof(PayrollDeductionScheduleItem);
            journal.EntityRefId = request.EntityId;


            var depositAccount = dbContext.SpecialDepositAccounts.Where(r => r.Id == request.DepositAccountId)
                .Include(p => p.DepositProduct)
                .ThenInclude(p => p.BankDepositAccount)
                .ThenInclude(l => l.LedgerAccount)
                .Include(p => p.DepositProduct).ThenInclude(p => p.ProductDepositAccount)
                .Include(p => p.Customer).ThenInclude(p => p.CashAccount)
                .Include(p => p.DepositAccount)
                .FirstOrDefault();


            var txn = dbContext.PayrollDeductionScheduleItems.FirstOrDefault(p => p.Id == request.EntityId);

            LedgerAccount SD_PAYROLL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_PAYROLL_CONTROL.ToString());
            LedgerAccount SD_PROD_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_PROD_CONTROL.ToString());
            LedgerAccount SD_CUST_BANK_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_CUST_BANK_CONTROL.ToString());


            LedgerAccount CUST_CASH_ACC = depositAccount.Customer.CashAccount;
            LedgerAccount COY_BANK_ACC = depositAccount.DepositProduct.BankDepositAccount.LedgerAccount;
            LedgerAccount PROD_DEPOSIT_ACC = depositAccount.DepositProduct.ProductDepositAccount;
            LedgerAccount CUST_DEPOSIT_ACC = depositAccount.DepositAccount;

            //DR SD_PAYROLL_CONTROL
            //CR Customer cash account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = SD_PAYROLL_CONTROL.Id,
                Account = SD_PAYROLL_CONTROL,
                Debit = txn.Amount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = CUST_CASH_ACC.Id,
                Account = CUST_CASH_ACC,
                Credit = txn.Amount,
                TransactionDate = request.TransactionDate
            });


            //DR Customer cash account fundingAmount
            //CR Company bank account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = CUST_CASH_ACC.Id,
                Account = CUST_CASH_ACC,
                Debit = txn.Amount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = COY_BANK_ACC.Id,
                Account = COY_BANK_ACC,
                Credit = txn.Amount,
                TransactionDate = request.TransactionDate
            });

            //DR SD_PROD_CONTROL
            //CR Product deposit account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = SD_PROD_CONTROL.Id,
                Account = SD_PROD_CONTROL,
                Debit = txn.Amount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = PROD_DEPOSIT_ACC.Id,
                Account = PROD_DEPOSIT_ACC,
                Credit = txn.Amount,
                TransactionDate = request.TransactionDate
            });



            //DR SD_CUST_BANK_CONTROL fundingAmount
            //CR Account deposit account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = SD_CUST_BANK_CONTROL.Id,
                Account = SD_CUST_BANK_CONTROL,
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



            dbContext.TransactionJournals.Add(journal);
            dbContext.SaveChanges();

            journal.Post();

            //txn.TransactionJournalId = journal.Id;
            //txn.IsProcessed = true;
            //txn.ProcessedDate = request.TransactionDate;
            txn.CurrentStatus = TransactionStatus.SUCCESS.ToString();


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

    private DepositTransactionViewModel ProcessSavingsDepositPayrollFunding(DepositTransactionCommand request,
        CancellationToken cancellationToken)
    {
        /*
         * 

        v1 SAVINGS FUNDING->PAYROLL
        DR SAVINGS_PAYROLL_CONTROL
        CR Customer cash account fundingAmount

        DR Customer cash account fundingAmount
        CR Company bank account fundingAmount 

        DR SAVINGS_PROD_CONTROL 
        CR Product deposit account fundingAmount

        DR SAVINGS_CUST_BANK_CONTROL  
        CR Account deposit account fundingAmount

         * 
         * 
         */

        DepositTransactionViewModel rsp = new DepositTransactionViewModel();

        try
        {

            TransactionJournal journal = new TransactionJournal();
            journal.TransactionNo = NHiloHelper.GetNextKey(nameof(TransactionJournal)).ToString();
            journal.Title = $"Savings Deposit Payroll Funding";
            journal.TransactionType = request.TransactionType;
            journal.TransactionDate = request.TransactionDate;
            journal.DocumentRef = nameof(SavingsAccount);
            journal.DocumentRefId = request.DepositAccountId;
            journal.EntityRef = nameof(PayrollDeductionScheduleItem);
            journal.EntityRefId = request.EntityId;


            var depositAccount = dbContext.SavingsAccounts.Where(r => r.Id == request.DepositAccountId)
                .Include(p => p.DepositProduct)
                .ThenInclude(p => p.BankDepositAccount)
                .ThenInclude(l => l.LedgerAccount)
                .Include(p => p.DepositProduct).ThenInclude(p => p.ProductDepositAccount)
                .Include(p => p.Customer).ThenInclude(p => p.CashAccount)
                .Include(p => p.LedgerDepositAccount)
                .FirstOrDefault();


            var txn = dbContext.PayrollDeductionScheduleItems.FirstOrDefault(p => p.Id == request.EntityId);

            LedgerAccount SAVINGS_PAYROLL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SAVINGS_PAYROLL_CONTROL.ToString());
            LedgerAccount SAVINGS_PROD_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SAVINGS_PROD_CONTROL.ToString());
            LedgerAccount SAVINGS_CUST_BANK_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SAVINGS_CUST_BANK_CONTROL.ToString());


            LedgerAccount CUST_CASH_ACC = depositAccount.Customer.CashAccount;
            LedgerAccount COY_BANK_ACC = depositAccount.DepositProduct.BankDepositAccount.LedgerAccount;
            LedgerAccount PROD_DEPOSIT_ACC = depositAccount.DepositProduct.ProductDepositAccount;
            LedgerAccount CUST_DEPOSIT_ACC = depositAccount.LedgerDepositAccount;

            //DR SAVINGS_PAYROLL_CONTROL
            //CR Customer cash account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = SAVINGS_PAYROLL_CONTROL.Id,
                Account = SAVINGS_PAYROLL_CONTROL,
                Debit = txn.Amount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = CUST_CASH_ACC.Id,
                Account = CUST_CASH_ACC,
                Credit = txn.Amount,
                TransactionDate = request.TransactionDate
            });


            //DR Customer cash account fundingAmount
            //CR Company bank account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = CUST_CASH_ACC.Id,
                Account = CUST_CASH_ACC,
                Debit = txn.Amount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = COY_BANK_ACC.Id,
                Account = COY_BANK_ACC,
                Credit = txn.Amount,
                TransactionDate = request.TransactionDate
            });

            //DR SAVINGS_PROD_CONTROL
            //CR Product deposit account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = SAVINGS_PROD_CONTROL.Id,
                Account = SAVINGS_PROD_CONTROL,
                Debit = txn.Amount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = PROD_DEPOSIT_ACC.Id,
                Account = PROD_DEPOSIT_ACC,
                Credit = txn.Amount,
                TransactionDate = request.TransactionDate
            });



            //DR SAVINGS_CUST_BANK_CONTROL fundingAmount
            //CR Account deposit account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = SAVINGS_CUST_BANK_CONTROL.Id,
                Account = SAVINGS_CUST_BANK_CONTROL,
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



            dbContext.TransactionJournals.Add(journal);
            dbContext.SaveChanges();

            journal.Post();

            //txn.TransactionJournalId = journal.Id;
            //txn.IsProcessed = true;
            //txn.ProcessedDate = request.TransactionDate;
            txn.CurrentStatus = TransactionStatus.SUCCESS.ToString();


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

    private DepositTransactionViewModel ProcessSpecialDepositCashAddition(DepositTransactionCommand request,
        CancellationToken cancellationToken)
    {
        /*
          * 

         DR SD_COY_BANK_CONTROL
         CR Customer cash account fundingAmount

         DR Customer cash account fundingAmount
         CR Company bank account fundingAmount 

         DR SD_PROD_CONTROL 
         CR Product deposit account fundingAmount

         DR SD_CUST_BANK_CONTROL  
         CR Account deposit account fundingAmount

          * 
          * 
          */


        DepositTransactionViewModel rsp = new DepositTransactionViewModel();

        try
        {

            TransactionJournal journal = new TransactionJournal();
            journal.TransactionNo = NHiloHelper.GetNextKey(nameof(TransactionJournal)).ToString();
            journal.Title = $"Special Deposit Cash Addition";
            journal.TransactionType = request.TransactionType;
            journal.TransactionDate = request.TransactionDate;
            journal.DocumentRef = nameof(SpecialDepositAccount);
            journal.DocumentRefId = request.DepositAccountId;
            journal.EntityRef = nameof(SpecialDepositCashAddition);
            journal.EntityRefId = request.EntityId;

            var txn = dbContext.SpecialDepositCashAdditions.FirstOrDefault(p => p.Id == request.EntityId);

            var depositAccount = dbContext.SpecialDepositAccounts.Where(r => r.Id == request.DepositAccountId)
                .Include(p => p.DepositProduct)
                .ThenInclude(p => p.BankDepositAccount)
                .ThenInclude(l => l.LedgerAccount)
                .Include(p => p.DepositProduct).ThenInclude(p => p.ProductDepositAccount)
                .Include(p => p.Customer).ThenInclude(p => p.CashAccount)
                .Include(p => p.DepositAccount)
                .FirstOrDefault();


            LedgerAccount SD_COY_BANK_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_COY_BANK_CONTROL.ToString());
            LedgerAccount SD_PROD_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_PROD_CONTROL.ToString());
            LedgerAccount SD_CUST_BANK_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_CUST_BANK_CONTROL.ToString());


            LedgerAccount SD_PROD_INTEREST_ACCRUAL_CONTROL =
               dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_PROD_INTEREST_ACCRUAL_CONTROL.ToString());
            LedgerAccount SD_ACC_INTEREST_ACCRUAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_ACC_INTEREST_ACCRUAL_CONTROL.ToString());
            LedgerAccount SD_PROD_INTEREST_ADD_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_PROD_INTEREST_ADD_CONTROL.ToString());


            LedgerAccount CUST_CASH_ACC = depositAccount.Customer.CashAccount;
            LedgerAccount COY_BANK_ACC = depositAccount.DepositProduct.BankDepositAccount.LedgerAccount;
            LedgerAccount PROD_DEPOSIT_ACC = depositAccount.DepositProduct.ProductDepositAccount;
            LedgerAccount CUST_DEPOSIT_ACC = depositAccount.DepositAccount;


            //DR SD_COY_BANK_CONTROL
            //CR Customer cash account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = SD_COY_BANK_CONTROL.Id,
                Account = SD_COY_BANK_CONTROL,
                Debit = txn.Amount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = CUST_CASH_ACC.Id,
                Account = CUST_CASH_ACC,
                Credit = txn.Amount,
                TransactionDate = request.TransactionDate
            });


            //DR Customer cash account fundingAmount
            //CR Company bank account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = CUST_CASH_ACC.Id,
                Account = CUST_CASH_ACC,
                Debit = txn.Amount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = COY_BANK_ACC.Id,
                Account = COY_BANK_ACC,
                Credit = txn.Amount,
                TransactionDate = request.TransactionDate
            });


            //DR SD_PROD_CONTROL
            //CR Product deposit account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = SD_PROD_CONTROL.Id,
                Account = SD_PROD_CONTROL,
                Debit = txn.Amount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = PROD_DEPOSIT_ACC.Id,
                Account = PROD_DEPOSIT_ACC,
                Credit = txn.Amount,
                TransactionDate = request.TransactionDate
            });



            //DR SD_CUST_BANK_CONTROL fundingAmount
            //CR Account deposit account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = SD_CUST_BANK_CONTROL.Id,
                Account = SD_CUST_BANK_CONTROL,
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




    private DepositTransactionViewModel ProcessSpecialDepositInterestAddition(DepositTransactionCommand request,
       CancellationToken cancellationToken)
    {
        /*
         * 

        v1

        SD Interest addition
        DR SD/FD_PROD_INTEREST_ACCRUAL_CONTROL
        CR Product interest payable account fundingAmount

        DR SD/FD_ACC_INTEREST_ACCRUAL_CONTROL  
        CR Account interest earned account fundingAmount

        DR SD/FD_PROD_INTEREST_ADD_CONTROL
        CR Prod deposit account fundingAmount

        DR SD/FD_ACC_INTEREST_ADD_CONTROL
        CR Account deposit account fundingAmount

         * 
         * 
         */


        DepositTransactionViewModel rsp = new DepositTransactionViewModel();

        try
        {

            TransactionJournal journal = new TransactionJournal();
            journal.TransactionNo = NHiloHelper.GetNextKey(nameof(TransactionJournal)).ToString();
            journal.Title = $"Special Deposit Interest Addition";
            journal.TransactionType = request.TransactionType;
            journal.TransactionDate = request.TransactionDate;
            journal.DocumentRef = nameof(SpecialDepositAccount);
            journal.DocumentRefId = request.DepositAccountId;
            journal.EntityRef = nameof(SpecialDepositInterestAddition);
            journal.EntityRefId = request.EntityId;

            var txn = dbContext.SpecialDepositInterestAdditions.FirstOrDefault(p => p.Id == request.EntityId);

            var depositAccount = dbContext.SpecialDepositAccounts.Where(r => r.Id == request.DepositAccountId)
                .Include(p => p.DepositProduct)
                .ThenInclude(p => p.BankDepositAccount)
                .ThenInclude(l => l.LedgerAccount)
                .Include(p => p.DepositProduct).ThenInclude(p => p.ProductDepositAccount)
                .Include(p => p.DepositProduct).ThenInclude(p => p.InterestPayableAccount)
                .Include(p => p.Customer).ThenInclude(p => p.CashAccount)
                .Include(p => p.DepositAccount).Include(d => d.InterestEarnedAccount)
                .FirstOrDefault();


            LedgerAccount SD_COY_BANK_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_COY_BANK_CONTROL.ToString());
            LedgerAccount SD_PROD_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_PROD_CONTROL.ToString());
            LedgerAccount SD_CUST_BANK_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_CUST_BANK_CONTROL.ToString());


            LedgerAccount SD_PROD_INTEREST_ACCRUAL_CONTROL =
               dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_PROD_INTEREST_ACCRUAL_CONTROL.ToString());
            LedgerAccount SD_ACC_INTEREST_ACCRUAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_ACC_INTEREST_ACCRUAL_CONTROL.ToString());
            LedgerAccount SD_PROD_INTEREST_ADD_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_PROD_INTEREST_ADD_CONTROL.ToString());
            LedgerAccount SD_ACC_INTEREST_ADD_CONTROL =
               dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_ACC_INTEREST_ADD_CONTROL.ToString());


            LedgerAccount CUST_CASH_ACC = depositAccount.Customer.CashAccount;
            LedgerAccount COY_BANK_ACC = depositAccount.DepositProduct.BankDepositAccount.LedgerAccount;
            LedgerAccount PROD_DEPOSIT_ACC = depositAccount.DepositProduct.ProductDepositAccount;
            LedgerAccount PROD_INTEREST_PAYABLE = depositAccount.DepositProduct.InterestPayableAccount;
            LedgerAccount CUST_DEPOSIT_ACC = depositAccount.DepositAccount;


            //DR SD/ FD_PROD_INTEREST_ACCRUAL_CONTROL
            //CR Product interest payable account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = SD_PROD_INTEREST_ACCRUAL_CONTROL.Id,
                Account = SD_PROD_INTEREST_ACCRUAL_CONTROL,
                Debit = txn.InterestEarned,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = PROD_INTEREST_PAYABLE.Id,
                Account = PROD_INTEREST_PAYABLE,
                Credit = txn.InterestEarned,
                TransactionDate = request.TransactionDate
            });


            //DR SD/ FD_ACC_INTEREST_ACCRUAL_CONTROL
            //CR Account interest earned account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = SD_ACC_INTEREST_ACCRUAL_CONTROL.Id,
                Account = SD_ACC_INTEREST_ACCRUAL_CONTROL,
                Debit = txn.InterestEarned,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = depositAccount.InterestEarnedAccountId,
                Account = depositAccount.InterestEarnedAccount,
                Credit = txn.InterestEarned,
                TransactionDate = request.TransactionDate
            });


            //DR SD/ FD_PROD_INTEREST_ADD_CONTROL
            //CR Prod deposit account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                Account = SD_PROD_INTEREST_ADD_CONTROL,
                AccountId = SD_PROD_INTEREST_ADD_CONTROL.Id,
                Debit = txn.InterestEarned,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                Account = PROD_DEPOSIT_ACC,
                AccountId = PROD_DEPOSIT_ACC.Id,
                Credit = txn.InterestEarned,
                TransactionDate = request.TransactionDate
            });



            //DR SD/ FD_ACC_INTEREST_ADD_CONTROL
            //CR Account deposit account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                Account = SD_ACC_INTEREST_ADD_CONTROL,
                AccountId = SD_ACC_INTEREST_ADD_CONTROL.Id,
                Debit = txn.InterestEarned,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                Account = CUST_DEPOSIT_ACC,
                AccountId = CUST_DEPOSIT_ACC.Id,
                Credit = txn.InterestEarned,
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
            logger.LogError(ex, ex.Message);
            rsp.ErrorDate = DateTime.Now;
            rsp.ErrorDetails = ex.Message;
            rsp.ErrorFlag = true;

            //throw ex;

        }


        return rsp;

    }

    private DepositTransactionViewModel ProcessSpecialDepositFundTransfer(DepositTransactionCommand request,
        CancellationToken cancellationToken)
    {

        /*
        * 
        * 
        
        SD Transfer v1->SV/SD/FD
        DR Product deposit account fundingAmount
        CR SV/FD Product Deposit Account

        DR Account deposit account fundingAmount
        CR SV/FD Deposit Account


        * 
        * 
        */


        DepositTransactionViewModel rsp = new DepositTransactionViewModel();

        try
        {

            TransactionJournal journal = new TransactionJournal();
            journal.TransactionNo = NHiloHelper.GetNextKey(nameof(TransactionJournal)).ToString();
            journal.Title = $"Special Deposit Funds Transfer";
            journal.TransactionType = request.TransactionType;
            journal.TransactionDate = request.TransactionDate;
            journal.DocumentRef = nameof(SpecialDepositAccount);
            journal.DocumentRefId = request.DepositAccountId;
            journal.EntityRef = nameof(SpecialDepositFundTransfer);
            journal.EntityRefId = request.EntityId;

            var txn = dbContext.SpecialDepositFundTransfers.Where(p => p.Id == request.EntityId)
                .Include(p => p.SavingsDestinationAccount).ThenInclude(p => p.DepositProduct)
                .ThenInclude(p => p.ProductDepositAccount)
                .Include(p => p.SavingsDestinationAccount).ThenInclude(p => p.LedgerDepositAccount)
                .Include(p => p.FixedDepositDestinationAccount).ThenInclude(p => p.DepositProduct)
                .ThenInclude(p => p.ProductDepositAccount)
                .Include(p => p.FixedDepositDestinationAccount).ThenInclude(p => p.DepositAccount)
                .FirstOrDefault();

            var depositAccount = dbContext.SpecialDepositAccounts.Where(r => r.Id == request.DepositAccountId)
                .Include(p => p.DepositProduct)
                .ThenInclude(p => p.BankDepositAccount)
                .ThenInclude(l => l.LedgerAccount)
                .Include(p => p.DepositProduct).ThenInclude(p => p.ProductDepositAccount)
                .Include(p => p.Customer).ThenInclude(p => p.CashAccount)
                .Include(p => p.DepositAccount)
                .FirstOrDefault();


            LedgerAccount SD_COY_BANK_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_COY_BANK_CONTROL.ToString());
            LedgerAccount SD_PROD_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_PROD_CONTROL.ToString());
            LedgerAccount SD_CUST_BANK_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_CUST_BANK_CONTROL.ToString());

            LedgerAccount SAVINGS_PROD_DEPOSIT_ACC = txn.SavingsDestinationAccount?.DepositProduct?.ProductDepositAccount;
            LedgerAccount SAVINGS_ACC_DEPOSIT_ACC = txn.SavingsDestinationAccount?.LedgerDepositAccount;
            LedgerAccount FD_PROD_DEPOSIT_ACC = txn.FixedDepositDestinationAccount?.DepositProduct?.ProductDepositAccount;
            LedgerAccount FD_ACC_DEPOSIT_ACC = txn.FixedDepositDestinationAccount?.DepositAccount;

            LedgerAccount CUST_CASH_ACC = depositAccount.Customer?.CashAccount;
            LedgerAccount COY_BANK_ACC = depositAccount.DepositProduct?.BankDepositAccount?.LedgerAccount;
            LedgerAccount PROD_DEPOSIT_ACC = depositAccount.DepositProduct?.ProductDepositAccount;
            LedgerAccount CUST_DEPOSIT_ACC = depositAccount.DepositAccount;


            if (txn.DestinationAccountType == DestinationAccountType.SAVINGS_ACCOUNT)
            {

                //DR Product deposit account fundingAmount
                //CR SV / FD Product Deposit Account
                journal.JournalEntries.Add(new JournalEntry
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = TransactionEntryType.DEBIT,
                    AccountId = PROD_DEPOSIT_ACC.Id,
                    Account = PROD_DEPOSIT_ACC,
                    Debit = txn.Amount,
                    TransactionDate = request.TransactionDate
                });

                journal.JournalEntries.Add(new JournalEntry
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = TransactionEntryType.CREDIT,
                    AccountId = SAVINGS_PROD_DEPOSIT_ACC.Id,
                    Account = SAVINGS_PROD_DEPOSIT_ACC,
                    Credit = txn.Amount,
                    TransactionDate = request.TransactionDate
                });


                //DR Account deposit account fundingAmount
                //CR SV / FD Deposit Account
                journal.JournalEntries.Add(new JournalEntry
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = TransactionEntryType.DEBIT,
                    AccountId = CUST_DEPOSIT_ACC.Id,
                    Account = CUST_DEPOSIT_ACC,
                    Debit = txn.Amount,
                    TransactionDate = request.TransactionDate
                });

                journal.JournalEntries.Add(new JournalEntry
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = TransactionEntryType.CREDIT,
                    AccountId = SAVINGS_ACC_DEPOSIT_ACC.Id,
                    Account = SAVINGS_ACC_DEPOSIT_ACC,
                    Credit = txn.Amount,
                    TransactionDate = request.TransactionDate
                });



            }
            else if (txn.DestinationAccountType == DestinationAccountType.FIXED_DEPOSIT_ACCOUNT)
            {

                //DR Product deposit account fundingAmount
                //CR SV / FD Product Deposit Account
                journal.JournalEntries.Add(new JournalEntry
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = TransactionEntryType.DEBIT,
                    AccountId = PROD_DEPOSIT_ACC.Id,
                    Account = PROD_DEPOSIT_ACC,
                    Debit = txn.Amount,
                    TransactionDate = request.TransactionDate
                });

                journal.JournalEntries.Add(new JournalEntry
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = TransactionEntryType.CREDIT,
                    AccountId = FD_PROD_DEPOSIT_ACC.Id,
                    Account = FD_PROD_DEPOSIT_ACC,
                    Credit = txn.Amount,
                    TransactionDate = request.TransactionDate
                });


                //DR Account deposit account fundingAmount
                //CR SV / FD Deposit Account
                journal.JournalEntries.Add(new JournalEntry
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = TransactionEntryType.DEBIT,
                    AccountId = CUST_DEPOSIT_ACC.Id,
                    Account = CUST_DEPOSIT_ACC,
                    Debit = txn.Amount,
                    TransactionDate = request.TransactionDate
                });

                journal.JournalEntries.Add(new JournalEntry
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = TransactionEntryType.CREDIT,
                    AccountId = FD_ACC_DEPOSIT_ACC.Id,
                    Account = FD_ACC_DEPOSIT_ACC,
                    Credit = txn.Amount,
                    TransactionDate = request.TransactionDate
                });


            }
            else
            {

            }



            dbContext.TransactionJournals.Add(journal);
            dbContext.SaveChanges();


            //var entryAccountIds = journal.JournalEntries.Select(journalEntry => journalEntry.AccountId);
            //var entryLedgerAccounts = dbContext.LedgerAccounts.Where(p => entryAccountIds.Contains(p.Id));

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

    private DepositTransactionViewModel ProcessSpecialDepositWithdrawal(DepositTransactionCommand request,
        CancellationToken cancellationToken)
    {

        /*
        * 
        * 
        
        Withdrawals v1 ->existing bank account  
        DR Company bank account fundingAmount 
        CR Customer bank account fundingAmount

        DR Product deposit account fundingAmount
        CR SD_PROD_CONTROL 

        DR Account deposit account fundingAmount
        CR SD_CUST_BANK_CONTROL 



        cash addition
        DR SD_COY_BANK_CONTROL
        CR Customer cash account fundingAmount

        DR Customer cash account fundingAmount
        CR Company bank account fundingAmount 

        DR SD_PROD_CONTROL 
        CR Product deposit account fundingAmount

        DR SD_CUST_BANK_CONTROL  
        CR Account deposit account fundingAmount


        Withdrawals v2 ->existing bank account
        DR Customer cash account fundingAmount
        CR SD_COY_BANK_CONTROL


        * 
        * 
        */



        DepositTransactionViewModel rsp = new DepositTransactionViewModel();

        try
        {

            TransactionJournal journal = new TransactionJournal();
            journal.TransactionNo = NHiloHelper.GetNextKey(nameof(TransactionJournal)).ToString();
            journal.Title = $"Special Deposit Withdrawal";
            journal.TransactionType = request.TransactionType;
            journal.TransactionDate = request.TransactionDate;
            journal.DocumentRef = nameof(SpecialDepositAccount);
            journal.DocumentRefId = request.DepositAccountId;
            journal.EntityRef = nameof(SpecialDepositWithdrawal);
            journal.EntityRefId = request.EntityId;

            var txn = dbContext.SpecialDepositWithdrawals
                .Include(z => z.CustomerDestinationBankAccount)
                .FirstOrDefault(p => p.Id == request.EntityId);



            var depositAccount = dbContext.SpecialDepositAccounts.Where(r => r.Id == request.DepositAccountId)
                .Include(p => p.DepositProduct)
                .ThenInclude(p => p.BankDepositAccount)
                .ThenInclude(l => l.LedgerAccount)
                .Include(p => p.DepositProduct).ThenInclude(p => p.ProductDepositAccount)
                .Include(p => p.Customer).ThenInclude(p => p.CashAccount)
                .Include(p => p.DepositAccount)
                .FirstOrDefault();



            LedgerAccount SD_COY_BANK_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_COY_BANK_CONTROL.ToString());
            LedgerAccount SD_PROD_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_PROD_CONTROL.ToString());
            LedgerAccount SD_CUST_BANK_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_CUST_BANK_CONTROL.ToString());

            LedgerAccount SD_PROD_WTD_CONTROL =
               dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_PROD_WTD_CONTROL.ToString());
            LedgerAccount SD_ACC_WTD_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_ACC_WTD_CONTROL.ToString());


            LedgerAccount CUST_CASH_ACC = depositAccount.Customer.CashAccount;
            LedgerAccount COY_BANK_ACC = depositAccount.DepositProduct.BankDepositAccount.LedgerAccount;
            LedgerAccount PROD_DEPOSIT_ACC = depositAccount.DepositProduct.ProductDepositAccount;
            LedgerAccount CUST_DEPOSIT_ACC = depositAccount.DepositAccount;

            var customerBankAccount = dbContext.CustomerBankAccounts.FirstOrDefault(p => p.CustomerId == depositAccount.Customer.Id && p.Id == txn.CustomerDestinationBankAccountId);
            LedgerAccount CUST_BANK_ACC = dbContext.LedgerAccounts.FirstOrDefault(c => c.Id == customerBankAccount.LedgerAccountId);




            ////experiment take out
            ////DR Customer cash account fundingAmount
            ////CR SD_COY_BANK_CONTROL
            ////before DR SD_COY_BANK_CONTROL
            ////before CR Customer cash account fundingAmount
            //journal.JournalEntries.Add(new JournalEntry
            //{
            //    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
            //    EntryType = TransactionEntryType.DEBIT,
            //    AccountId = CUST_CASH_ACC.Id,
            //    Debit = txn.Amount,
            //    TransactionDate = request.TransactionDate
            //});

            //journal.JournalEntries.Add(new JournalEntry
            //{
            //    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
            //    EntryType = TransactionEntryType.CREDIT,
            //    AccountId = SD_COY_BANK_CONTROL.Id,
            //    Credit = txn.Amount,
            //    TransactionDate = request.TransactionDate
            //});



            //DR Company bank account fundingAmount
            //CR Customer bank account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = COY_BANK_ACC.Id,
                Account = COY_BANK_ACC,
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


            //DR Product deposit account fundingAmount
            //CR SD_PROD_CONTROL
            //CR SD_PROD_WTD_CONTROL
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = PROD_DEPOSIT_ACC.Id,
                Account = PROD_DEPOSIT_ACC,
                Debit = txn.Amount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = SD_PROD_CONTROL.Id,
                Account = SD_PROD_CONTROL,
                Credit = txn.Amount,
                TransactionDate = request.TransactionDate
            });



            //DR Account deposit account fundingAmount
            //CR SD_CUST_BANK_CONTROL fundingAmount
            //CR SD_ACC_WTD_CONTROL
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = CUST_DEPOSIT_ACC.Id,
                Account = CUST_DEPOSIT_ACC,
                Debit = txn.Amount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = SD_CUST_BANK_CONTROL.Id,
                Account = SD_CUST_BANK_CONTROL,
                Credit = txn.Amount,
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
            logger.LogError(ex, ex.Message);
            rsp.ErrorDate = DateTime.Now;
            rsp.ErrorDetails = ex.Message;
            rsp.ErrorFlag = true;

            //throw ex;

        }


        return rsp;

    }

    private DepositTransactionViewModel ProcessSavingsWithdrawal(DepositTransactionCommand request,
        CancellationToken cancellationToken)
    {
        return null;
    }

    private DepositTransactionViewModel ProcessSavingsTransfer(DepositTransactionCommand request,
        CancellationToken cancellationToken)
    {
        return null;
    }

    private DepositTransactionViewModel ProcessSavingsCashAddition(DepositTransactionCommand request,
        CancellationToken cancellationToken)
    {

        /*
         * 

        v3

        DR SD_COY_BANK_CONTROL
        CR Customer cash account fundingAmount

        DR Customer cash account fundingAmount
        CR Company bank account fundingAmount 

        DR SD_PROD_CONTROL 
        CR Product deposit account fundingAmount

        DR SD_CUST_BANK_CONTROL  
        CR Account deposit account fundingAmount

         * 
         * 
         */


        DepositTransactionViewModel rsp = new DepositTransactionViewModel();

        try
        {

            TransactionJournal journal = new TransactionJournal();
            journal.TransactionNo = NHiloHelper.GetNextKey(nameof(TransactionJournal)).ToString();
            journal.Title = $"Savings Cash Addition";
            journal.TransactionType = request.TransactionType;
            journal.TransactionDate = request.TransactionDate;
            journal.DocumentRef = nameof(SavingsAccount);
            journal.DocumentRefId = request.DepositAccountId;
            journal.EntityRef = nameof(SavingsCashAddition);
            journal.EntityRefId = request.EntityId;

            var txn = dbContext.SavingsCashAdditions.FirstOrDefault(p => p.Id == request.EntityId);

            var depositAccount = dbContext.SavingsAccounts.Where(r => r.Id == request.DepositAccountId)
                .Include(p => p.DepositProduct)
                .ThenInclude(p => p.BankDepositAccount)
                .ThenInclude(l => l.LedgerAccount)
                .Include(p => p.DepositProduct).ThenInclude(p => p.ProductDepositAccount)
                .Include(p => p.Customer).ThenInclude(p => p.CashAccount)
                .FirstOrDefault();


            LedgerAccount SD_COY_BANK_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_COY_BANK_CONTROL.ToString());
            LedgerAccount SD_PROD_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_PROD_CONTROL.ToString());
            LedgerAccount SD_CUST_BANK_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_CUST_BANK_CONTROL.ToString());


            LedgerAccount CUST_CASH_ACC = depositAccount.Customer.CashAccount;
            LedgerAccount COY_BANK_ACC = depositAccount.DepositProduct.BankDepositAccount.LedgerAccount;
            LedgerAccount PROD_DEPOSIT_ACC = depositAccount.DepositProduct.ProductDepositAccount;
            LedgerAccount CUST_DEPOSIT_ACC = depositAccount.LedgerDepositAccount;

            //DR SD_COY_BANK_CONTROL
            //CR Customer cash account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = SD_COY_BANK_CONTROL.Id,
                Account = SD_COY_BANK_CONTROL,
                Debit = txn.Amount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = CUST_CASH_ACC.Id,
                Account = CUST_CASH_ACC,
                Credit = txn.Amount,
                TransactionDate = request.TransactionDate
            });


            //DR Customer cash account fundingAmount
            //CR Company bank account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = CUST_CASH_ACC.Id,
                Account = CUST_CASH_ACC,
                Debit = txn.Amount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = COY_BANK_ACC.Id,
                Account = COY_BANK_ACC,
                Credit = txn.Amount,
                TransactionDate = request.TransactionDate
            });

            //DR SD_PROD_CONTROL
            //CR Product deposit account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = SD_PROD_CONTROL.Id,
                Account = SD_PROD_CONTROL,
                Debit = txn.Amount,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = PROD_DEPOSIT_ACC.Id,
                Account = PROD_DEPOSIT_ACC,
                Credit = txn.Amount,
                TransactionDate = request.TransactionDate
            });



            //DR SD_CUST_BANK_CONTROL fundingAmount
            //CR Account deposit account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = SD_CUST_BANK_CONTROL.Id,
                Account = SD_CUST_BANK_CONTROL,
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

    private async Task<DepositTransactionViewModel> ProcessFixedDepositLiquidation(DepositTransactionCommand request,
        CancellationToken cancellationToken)
    {


        DepositTransactionViewModel rsp = new DepositTransactionViewModel();


        try
        {

            TransactionJournal journal = new TransactionJournal();
            journal.TransactionNo = NHiloHelper.GetNextKey(nameof(TransactionJournal)).ToString();
            journal.Title = $"Fixed Deposit Application Funding";
            journal.TransactionType = request.TransactionType;
            journal.TransactionDate = request.TransactionDate;
            journal.DocumentRef = nameof(FixedDepositAccount);
            journal.DocumentRefId = request.DepositAccountId;
            journal.EntityRef = nameof(ProcessFixedDepositLiquidation);
            journal.EntityRefId = request.EntityId;


            var currentDepositAccount = await dbContext.FixedDepositAccounts.Where(r => r.Id == request.DepositAccountId)
                .Include(p => p.ParentAccount)
                .Include(p => p.DepositAccount)
                .Include(p => p.InterestEarnedAccount)
                .Include(p => p.InterestPayoutAccount)
                .Include(p => p.ChargesAccruedAccount)
                .Include(p => p.Customer).ThenInclude(p => p.CashAccount)
                .FirstOrDefaultAsync(cancellationToken);

            FixedDepositAccount OldDepositAccount = currentDepositAccount.ParentAccount;


            if (!string.IsNullOrEmpty(currentDepositAccount.ParentAccountId))
            {
                OldDepositAccount = await dbContext.FixedDepositAccounts.Where(r => r.Id == currentDepositAccount.ParentAccountId)
               .Include(p => p.ParentAccount)
               .Include(p => p.DepositAccount)
               .Include(p => p.InterestEarnedAccount)
               .Include(p => p.InterestPayoutAccount)
               .Include(p => p.ChargesAccruedAccount)
               .FirstOrDefaultAsync(cancellationToken);
            }



            //var accountQuery = dbContext.FixedDepositAccounts.Where(r => r.Id == request.DepositAccountId);

            var product = await dbContext.DepositProducts.Where(p => p.Id == currentDepositAccount.DepositProductId)
                .Include(p => p.BankDepositAccount).ThenInclude(p => p.LedgerAccount)
                .Include(p => p.ProductDepositAccount)
                .Include(p => p.ChargesAccrualAccount)
                .Include(p => p.InterestPayableAccount)
                .Include(p => p.InterestPayoutAccount)
                .FirstOrDefaultAsync(cancellationToken);

            var txn = await dbContext.FixedDepositLiquidations.Where(r => r.Id == request.EntityId)
                .Include(p => p.SavingsLiquidationAccount).ThenInclude(p => p.LedgerDepositAccount)
                .Include(p => p.SavingsLiquidationAccount).ThenInclude(p => p.DepositProduct).ThenInclude(p => p.ProductDepositAccount)
                .Include(p => p.SpecialDepositLiquidationAccount).ThenInclude(p => p.DepositAccount)
                .Include(p => p.SpecialDepositLiquidationAccount).ThenInclude(p => p.DepositProduct).ThenInclude(p => p.ProductDepositAccount)
                .Include(p => p.CustomerBankLiquidationAccount).ThenInclude(p => p.LedgerAccount)
               .FirstOrDefaultAsync(cancellationToken);

            var charges = dbContext.FixedDepositLiquidationCharges.Where(r => r.FixedDepositLiquidationId == request.EntityId);



            LedgerAccount FD_COY_BANK_CONTROL =
                await dbContext.LedgerAccounts.FirstOrDefaultAsync(p => p.Code == ControlAccounts.FD_COY_BANK_CONTROL.ToString(), cancellationToken);
            LedgerAccount FD_PROD_CONTROL =
               await dbContext.LedgerAccounts.FirstOrDefaultAsync(p => p.Code == ControlAccounts.FD_PROD_CONTROL.ToString(), cancellationToken);
            LedgerAccount FD_CUST_BANK_CONTROL =
                await dbContext.LedgerAccounts.FirstOrDefaultAsync(p => p.Code == ControlAccounts.FD_CUST_BANK_CONTROL.ToString(), cancellationToken);


            LedgerAccount CUST_CASH_ACC = currentDepositAccount.Customer.CashAccount;
            LedgerAccount COY_BANK_ACC = currentDepositAccount.DepositProduct.BankDepositAccount.LedgerAccount;
            LedgerAccount PROD_DEPOSIT_ACC = currentDepositAccount.DepositProduct.ProductDepositAccount;
            LedgerAccount FD_ACC_DEPOSIT_ACC = currentDepositAccount.DepositAccount;
            //LedgerAccount SD_PROD_FUNDING_ACC = currentDepositAccount.Application.SpecialDepositFundingSourceAccount.DepositProduct.ProductDepositAccount;
            //LedgerAccount SD_FUNDING_ACC = currentDepositAccount.Application.SpecialDepositFundingSourceAccount.DepositAccount;


            LedgerAccount FD_PROD_CHARGE_ACCRUAL_CONTROL =
               dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.FD_PROD_CHARGE_ACCRUAL_CONTROL.ToString());

            LedgerAccount FD_ACC_CHARGE_ACCRUAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.FD_ACC_CHARGE_ACCRUAL_CONTROL.ToString());

            LedgerAccount FD_PROD_CHARGES_ACCRUAL = currentDepositAccount.DepositProduct.ChargesAccrualAccount;
            LedgerAccount FD_ACC_CHARGES_ACCRUAL = currentDepositAccount.ChargesAccruedAccount;


            LedgerAccount SAVINGS_PROD_CONTROL =
                 dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SAVINGS_PROD_CONTROL.ToString());

            LedgerAccount SAVINGS_CUST_BANK_CONTROL =
                 dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SAVINGS_CUST_BANK_CONTROL.ToString());


            LedgerAccount SD_PROD_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_PROD_CONTROL.ToString());

            LedgerAccount SD_CUST_BANK_CONTROL =
                 dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.SD_CUST_BANK_CONTROL.ToString());

            bool hasRollOverLeg = currentDepositAccount.MaturityInstructionType == MaturityInstructionType.ROLLOVER_PRINCIPAL_AND_INTEREST
                || currentDepositAccount.MaturityInstructionType == MaturityInstructionType.ROLLOVER_PRINCIPAL_AND_LIQUIDATE_INTEREST;



            decimal oldDepositBalance = 0, currentDepositBalance = 0;
            decimal oldInterestBalance = 0, currentInterestBalance = 0;

            currentDepositBalance = currentDepositAccount.DepositAccount.LedgerBalance;
            currentInterestBalance = currentDepositAccount.InterestEarnedAccount.LedgerBalance;


            if (!string.IsNullOrEmpty(currentDepositAccount.ParentAccountId))
            {
                //1. zero out old account initial deposit for both products and accounts
                //2. move interest earned/payable to payable for both products and accounts
                //3. close old FD account

                /*

               :old account
               DR Prod.InterestEarnedAccount earnedInterest
               CR Prod.InterestPayoutAccount earnedInterest

               DR Acc.InterestEarnedAccount earnedInterest
               CR Acc.InterestPayoutAccount earnedInterest

               DR Product deposit account deposit
               CR FD_PROD_CONTROL deposit

               DR Account deposit account deposit
               CR FD_CUST_BANK_CONTROL deposit
               */


                oldDepositBalance = OldDepositAccount.DepositAccount.LedgerBalance;
                oldInterestBalance = OldDepositAccount.InterestEarnedAccount.LedgerBalance;

                //DR Prod.InterestEarnedAccount earnedInterest
                //CR Prod.InterestPayoutAccount earnedInterest
                journal.JournalEntries.Add(new JournalEntry
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = TransactionEntryType.DEBIT,
                    AccountId = product.InterestPayableAccount.Id,
                    Account = product.InterestPayableAccount,
                    Debit = oldInterestBalance,
                    TransactionDate = request.TransactionDate
                });

                journal.JournalEntries.Add(new JournalEntry
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = TransactionEntryType.CREDIT,
                    AccountId = product.InterestPayoutAccount.Id,
                    Account = product.InterestPayoutAccount,
                    Credit = oldInterestBalance,
                    TransactionDate = request.TransactionDate
                });


                //DR Acc.InterestEarnedAccount earnedInterest
                //CR Acc.InterestPayoutAccount earnedInterest
                journal.JournalEntries.Add(new JournalEntry
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = TransactionEntryType.DEBIT,
                    AccountId = OldDepositAccount.InterestEarnedAccount.Id,
                    Account = OldDepositAccount.InterestEarnedAccount,
                    Debit = oldInterestBalance,
                    TransactionDate = request.TransactionDate
                });

                journal.JournalEntries.Add(new JournalEntry
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = TransactionEntryType.CREDIT,
                    AccountId = OldDepositAccount.InterestPayoutAccount.Id,
                    Account = OldDepositAccount.InterestPayoutAccount,
                    Credit = oldInterestBalance,
                    TransactionDate = request.TransactionDate
                });



                //DR Product deposit account deposit
                //CR FD_PROD_CONTROL deposit
                journal.JournalEntries.Add(new JournalEntry
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = TransactionEntryType.DEBIT,
                    AccountId = product.ProductDepositAccount.Id,
                    Account = product.ProductDepositAccount,
                    Debit = oldDepositBalance,
                    TransactionDate = request.TransactionDate
                });

                journal.JournalEntries.Add(new JournalEntry
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = TransactionEntryType.CREDIT,
                    AccountId = FD_PROD_CONTROL.Id,
                    Account = FD_PROD_CONTROL,
                    Credit = oldDepositBalance,
                    TransactionDate = request.TransactionDate
                });


                //DR Account deposit account deposit
                //CR FD_CUST_BANK_CONTROL deposit
                journal.JournalEntries.Add(new JournalEntry
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = TransactionEntryType.DEBIT,
                    AccountId = OldDepositAccount.DepositAccount.Id,
                    Account = OldDepositAccount.DepositAccount,
                    Debit = oldDepositBalance,
                    TransactionDate = request.TransactionDate
                });

                journal.JournalEntries.Add(new JournalEntry
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = TransactionEntryType.CREDIT,
                    AccountId = FD_CUST_BANK_CONTROL.Id,
                    Account = FD_CUST_BANK_CONTROL,
                    Credit = oldDepositBalance,
                    TransactionDate = request.TransactionDate
                });



                //close old FD account
                OldDepositAccount.IsClosed = true;
                OldDepositAccount.DateClosed = DateTime.Now;

            }


            switch (currentDepositAccount.MaturityInstructionType)
            {

                case MaturityInstructionType.ROLLOVER_PRINCIPAL_AND_INTEREST:
                    {

                        //DR FD_PROD_CONTROL deposit+interest
                        //CR FD Product deposit account deposit+interest
                        journal.JournalEntries.Add(new JournalEntry
                        {
                            TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                            EntryType = TransactionEntryType.DEBIT,
                            AccountId = FD_PROD_CONTROL.Id,
                            Account = FD_PROD_CONTROL,
                            Debit = oldDepositBalance + oldInterestBalance,
                            TransactionDate = request.TransactionDate
                        });

                        journal.JournalEntries.Add(new JournalEntry
                        {
                            TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                            EntryType = TransactionEntryType.CREDIT,
                            AccountId = product.ProductDepositAccount.Id,
                            Account = product.ProductDepositAccount,
                            Credit = oldDepositBalance + oldInterestBalance,
                            TransactionDate = request.TransactionDate
                        });

                        //DR FD_CUST_BANK_CONTROL account deposit+interest
                        //CR FD Account deposit account deposit+interest
                        journal.JournalEntries.Add(new JournalEntry
                        {
                            TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                            EntryType = TransactionEntryType.DEBIT,
                            AccountId = FD_CUST_BANK_CONTROL.Id,
                            Account = FD_CUST_BANK_CONTROL,
                            Debit = oldDepositBalance + oldInterestBalance,
                            TransactionDate = request.TransactionDate
                        });

                        journal.JournalEntries.Add(new JournalEntry
                        {
                            TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                            EntryType = TransactionEntryType.CREDIT,
                            AccountId = currentDepositAccount.DepositAccount.Id,
                            Account = currentDepositAccount.DepositAccount,
                            Credit = oldDepositBalance + oldInterestBalance,
                            TransactionDate = request.TransactionDate
                        });


                        break;
                    }

                case MaturityInstructionType.ROLLOVER_PRINCIPAL_AND_LIQUIDATE_INTEREST:
                    {

                        //DR FD_PROD_CONTROL deposit
                        //CR FD Product deposit account deposit
                        journal.JournalEntries.Add(new JournalEntry
                        {
                            TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                            EntryType = TransactionEntryType.DEBIT,
                            AccountId = FD_PROD_CONTROL.Id,
                            Account = FD_PROD_CONTROL,
                            Debit = oldDepositBalance,
                            TransactionDate = request.TransactionDate
                        });

                        journal.JournalEntries.Add(new JournalEntry
                        {
                            TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                            EntryType = TransactionEntryType.CREDIT,
                            AccountId = product.ProductDepositAccount.Id,
                            Account = product.ProductDepositAccount,
                            Credit = oldDepositBalance,
                            TransactionDate = request.TransactionDate
                        });

                        //DR FD_CUST_BANK_CONTROL account deposit
                        //CR FD Account deposit account deposit
                        journal.JournalEntries.Add(new JournalEntry
                        {
                            TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                            EntryType = TransactionEntryType.DEBIT,
                            AccountId = FD_CUST_BANK_CONTROL.Id,
                            Account = FD_CUST_BANK_CONTROL,
                            Debit = oldDepositBalance,
                            TransactionDate = request.TransactionDate
                        });

                        journal.JournalEntries.Add(new JournalEntry
                        {
                            TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                            EntryType = TransactionEntryType.CREDIT,
                            AccountId = currentDepositAccount.DepositAccount.Id,
                            Account = currentDepositAccount.DepositAccount,
                            Credit = oldDepositBalance,
                            TransactionDate = request.TransactionDate
                        });



                        switch (currentDepositAccount.LiquidationAccountType)
                        {
                            case WithdrawalAccountType.SAVINGS_ACCOUNT:
                                {


                                    //DR SV_PROD_CONTROL interest
                                    //CR SV Product deposit account interest
                                    journal.JournalEntries.Add(new JournalEntry
                                    {
                                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                        EntryType = TransactionEntryType.DEBIT,
                                        AccountId = SAVINGS_PROD_CONTROL.Id,
                                        Account = SAVINGS_PROD_CONTROL,
                                        Debit = oldInterestBalance,
                                        TransactionDate = request.TransactionDate
                                    });

                                    journal.JournalEntries.Add(new JournalEntry
                                    {
                                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                        EntryType = TransactionEntryType.CREDIT,
                                        AccountId = txn.SavingsLiquidationAccount.DepositProduct.ProductDepositAccount.Id,
                                        Account = txn.SavingsLiquidationAccount.DepositProduct.ProductDepositAccount,
                                        Credit = oldInterestBalance,
                                        TransactionDate = request.TransactionDate
                                    });

                                    //DR SV_CUST_BANK_CONTROL   interest
                                    //CR SV Account deposit account interest
                                    journal.JournalEntries.Add(new JournalEntry
                                    {
                                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                        EntryType = TransactionEntryType.DEBIT,
                                        AccountId = SAVINGS_CUST_BANK_CONTROL.Id,
                                        Account = SAVINGS_CUST_BANK_CONTROL,
                                        Debit = oldInterestBalance,
                                        TransactionDate = request.TransactionDate
                                    });

                                    journal.JournalEntries.Add(new JournalEntry
                                    {
                                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                        EntryType = TransactionEntryType.CREDIT,
                                        AccountId = txn.SavingsLiquidationAccount.LedgerDepositAccount.Id,
                                        Account = txn.SavingsLiquidationAccount.LedgerDepositAccount,
                                        Credit = oldInterestBalance,
                                        TransactionDate = request.TransactionDate
                                    });


                                    break;
                                }
                            case WithdrawalAccountType.SPECIAL_DEPOSIT_ACCOUNT:
                                {

                                    //DR SD_PROD_CONTROL interest
                                    //CR SD Product deposit account interest
                                    journal.JournalEntries.Add(new JournalEntry
                                    {
                                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                        EntryType = TransactionEntryType.DEBIT,
                                        AccountId = SD_PROD_CONTROL.Id,
                                        Account = SD_PROD_CONTROL,
                                        Debit = oldInterestBalance,
                                        TransactionDate = request.TransactionDate
                                    });

                                    journal.JournalEntries.Add(new JournalEntry
                                    {
                                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                        EntryType = TransactionEntryType.CREDIT,
                                        AccountId = txn.SpecialDepositLiquidationAccount.DepositProduct.ProductDepositAccount.Id,
                                        Account = txn.SpecialDepositLiquidationAccount.DepositProduct.ProductDepositAccount,
                                        Credit = oldInterestBalance,
                                        TransactionDate = request.TransactionDate
                                    });

                                    //DR SD_CUST_BANK_CONTROL   interest
                                    //CR SD Account deposit account interest
                                    journal.JournalEntries.Add(new JournalEntry
                                    {
                                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                        EntryType = TransactionEntryType.DEBIT,
                                        AccountId = SD_CUST_BANK_CONTROL.Id,
                                        Account = SD_CUST_BANK_CONTROL,
                                        Debit = oldInterestBalance,
                                        TransactionDate = request.TransactionDate
                                    });

                                    journal.JournalEntries.Add(new JournalEntry
                                    {
                                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                        EntryType = TransactionEntryType.CREDIT,
                                        AccountId = txn.SpecialDepositLiquidationAccount.DepositAccount.Id,
                                        Account = txn.SpecialDepositLiquidationAccount.DepositAccount,
                                        Credit = oldInterestBalance,
                                        TransactionDate = request.TransactionDate
                                    });

                                    break;
                                }
                            case WithdrawalAccountType.EXISTING_BANK_ACCOUNT:
                                {
                                    //DR Product.BankDepositAccount interest
                                    //CR Customer bank account  interest
                                    journal.JournalEntries.Add(new JournalEntry
                                    {
                                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                        EntryType = TransactionEntryType.DEBIT,
                                        AccountId = product.BankDepositAccount.LedgerAccount.Id,
                                        Account = product.BankDepositAccount.LedgerAccount,
                                        Debit = oldInterestBalance,
                                        TransactionDate = request.TransactionDate
                                    });

                                    journal.JournalEntries.Add(new JournalEntry
                                    {
                                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                        EntryType = TransactionEntryType.CREDIT,
                                        AccountId = txn.CustomerBankLiquidationAccount.LedgerAccount.Id,
                                        Account = txn.CustomerBankLiquidationAccount.LedgerAccount,
                                        Credit = oldInterestBalance,
                                        TransactionDate = request.TransactionDate
                                    });

                                    break;
                                }
                        }

                        break;
                    }

                case MaturityInstructionType.LIQUIDATE_PRINCIPAL_AND_INTEREST:
                    {

                        //1. zero out account initial deposit for both products and accounts
                        //2. move interest earned/payable to payable for both products and accounts
                        //3. close current FD account

                        /*

                        :current account
                        DR Prod.InterestEarnedAccount earnedInterest
                        CR Prod.InterestPayoutAccount earnedInterest

                        DR Acc.InterestEarnedAccount earnedInterest
                        CR Acc.InterestPayoutAccount earnedInterest

                        DR Product deposit account deposit
                        CR FD_PROD_CONTROL deposit

                        DR Account deposit account deposit
                        CR FD_CUST_BANK_CONTROL deposit
                        */




                        //DR Prod.InterestEarnedAccount earnedInterest
                        //CR Prod.InterestPayoutAccount earnedInterest
                        journal.JournalEntries.Add(new JournalEntry
                        {
                            TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                            EntryType = TransactionEntryType.DEBIT,
                            AccountId = product.InterestPayableAccount.Id,
                            Account = product.InterestPayableAccount,
                            Debit = currentInterestBalance,
                            TransactionDate = request.TransactionDate
                        });

                        journal.JournalEntries.Add(new JournalEntry
                        {
                            TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                            EntryType = TransactionEntryType.CREDIT,
                            AccountId = product.InterestPayoutAccount.Id,
                            Account = product.InterestPayoutAccount,
                            Credit = currentInterestBalance,
                            TransactionDate = request.TransactionDate
                        });


                        //DR Acc.InterestEarnedAccount earnedInterest
                        //CR Acc.InterestPayoutAccount earnedInterest
                        journal.JournalEntries.Add(new JournalEntry
                        {
                            TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                            EntryType = TransactionEntryType.DEBIT,
                            AccountId = currentDepositAccount.InterestEarnedAccount.Id,
                            Account = currentDepositAccount.InterestEarnedAccount,
                            Debit = currentInterestBalance,
                            TransactionDate = request.TransactionDate
                        });

                        journal.JournalEntries.Add(new JournalEntry
                        {
                            TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                            EntryType = TransactionEntryType.CREDIT,
                            AccountId = currentDepositAccount.InterestPayoutAccount.Id,
                            Account = currentDepositAccount.InterestPayoutAccount,
                            Credit = currentInterestBalance,
                            TransactionDate = request.TransactionDate
                        });



                        //DR Product deposit account deposit
                        //CR FD_PROD_CONTROL deposit
                        journal.JournalEntries.Add(new JournalEntry
                        {
                            TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                            EntryType = TransactionEntryType.DEBIT,
                            AccountId = product.ProductDepositAccount.Id,
                            Account = product.ProductDepositAccount,
                            Debit = currentDepositBalance,
                            TransactionDate = request.TransactionDate
                        });

                        journal.JournalEntries.Add(new JournalEntry
                        {
                            TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                            EntryType = TransactionEntryType.CREDIT,
                            AccountId = FD_PROD_CONTROL.Id,
                            Account = FD_PROD_CONTROL,
                            Credit = currentDepositBalance,
                            TransactionDate = request.TransactionDate
                        });


                        //DR Account deposit account deposit
                        //CR FD_CUST_BANK_CONTROL deposit
                        journal.JournalEntries.Add(new JournalEntry
                        {
                            TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                            EntryType = TransactionEntryType.DEBIT,
                            AccountId = currentDepositAccount.DepositAccount.Id,
                            Account = currentDepositAccount.DepositAccount,
                            Debit = currentDepositBalance,
                            TransactionDate = request.TransactionDate
                        });

                        journal.JournalEntries.Add(new JournalEntry
                        {
                            TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                            EntryType = TransactionEntryType.CREDIT,
                            AccountId = FD_CUST_BANK_CONTROL.Id,
                            Account = FD_CUST_BANK_CONTROL,
                            Credit = currentDepositBalance,
                            TransactionDate = request.TransactionDate
                        });



                        switch (currentDepositAccount.LiquidationAccountType)
                        {
                            case WithdrawalAccountType.SAVINGS_ACCOUNT:
                                {

                                    //DR SV_PROD_CONTROL deposit + interest
                                    //CR SV Product deposit account deposit+interest
                                    journal.JournalEntries.Add(new JournalEntry
                                    {
                                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                        EntryType = TransactionEntryType.DEBIT,
                                        AccountId = SAVINGS_PROD_CONTROL.Id,
                                        Account = SAVINGS_PROD_CONTROL,
                                        Debit = currentDepositBalance + currentInterestBalance,
                                        TransactionDate = request.TransactionDate
                                    });

                                    journal.JournalEntries.Add(new JournalEntry
                                    {
                                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                        EntryType = TransactionEntryType.CREDIT,
                                        AccountId = txn.SavingsLiquidationAccount.DepositProduct.ProductDepositAccount.Id,
                                        Account = txn.SavingsLiquidationAccount.DepositProduct.ProductDepositAccount,
                                        Credit = currentDepositBalance + currentInterestBalance,
                                        TransactionDate = request.TransactionDate
                                    });

                                    //DR SV_CUST_BANK_CONTROL  deposit + interest
                                    //CR SV Account deposit account deposit+interest
                                    journal.JournalEntries.Add(new JournalEntry
                                    {
                                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                        EntryType = TransactionEntryType.DEBIT,
                                        AccountId = SAVINGS_CUST_BANK_CONTROL.Id,
                                        Account = SAVINGS_CUST_BANK_CONTROL,
                                        Debit = currentDepositBalance + currentInterestBalance,
                                        TransactionDate = request.TransactionDate
                                    });

                                    journal.JournalEntries.Add(new JournalEntry
                                    {
                                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                        EntryType = TransactionEntryType.CREDIT,
                                        AccountId = txn.SavingsLiquidationAccount.LedgerDepositAccount.Id,
                                        Account = txn.SavingsLiquidationAccount.LedgerDepositAccount,
                                        Credit = currentDepositBalance + currentInterestBalance,
                                        TransactionDate = request.TransactionDate
                                    });


                                    break;
                                }
                            case WithdrawalAccountType.SPECIAL_DEPOSIT_ACCOUNT:
                                {

                                    //DR SD_PROD_CONTROL deposit + interest
                                    //CR SD Product deposit account deposit+interest
                                    journal.JournalEntries.Add(new JournalEntry
                                    {
                                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                        EntryType = TransactionEntryType.DEBIT,
                                        AccountId = SD_PROD_CONTROL.Id,
                                        Account = SD_PROD_CONTROL,
                                        Debit = currentDepositBalance + currentInterestBalance,
                                        TransactionDate = request.TransactionDate
                                    });

                                    journal.JournalEntries.Add(new JournalEntry
                                    {
                                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                        EntryType = TransactionEntryType.CREDIT,
                                        AccountId = txn.SpecialDepositLiquidationAccount.DepositProduct.ProductDepositAccount.Id,
                                        Account = txn.SpecialDepositLiquidationAccount.DepositProduct.ProductDepositAccount,
                                        Credit = currentDepositBalance + currentInterestBalance,
                                        TransactionDate = request.TransactionDate
                                    });

                                    //DR SD_CUST_BANK_CONTROL  deposit + interest
                                    //CR SD Account deposit account deposit+interest
                                    journal.JournalEntries.Add(new JournalEntry
                                    {
                                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                        EntryType = TransactionEntryType.DEBIT,
                                        AccountId = SD_CUST_BANK_CONTROL.Id,
                                        Account = SD_CUST_BANK_CONTROL,
                                        Debit = currentDepositBalance + currentInterestBalance,
                                        TransactionDate = request.TransactionDate
                                    });

                                    journal.JournalEntries.Add(new JournalEntry
                                    {
                                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                        EntryType = TransactionEntryType.CREDIT,
                                        AccountId = txn.SpecialDepositLiquidationAccount.DepositAccount.Id,
                                        Account = txn.SpecialDepositLiquidationAccount.DepositAccount,
                                        Credit = currentDepositBalance + currentInterestBalance,
                                        TransactionDate = request.TransactionDate
                                    });


                                    break;
                                }
                            case WithdrawalAccountType.EXISTING_BANK_ACCOUNT:
                                {
                                    //DR Product.BankDepositAccount deposit+interest
                                    //CR Customer bank account deposit + interest
                                    journal.JournalEntries.Add(new JournalEntry
                                    {
                                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                        EntryType = TransactionEntryType.DEBIT,
                                        AccountId = product.BankDepositAccount.LedgerAccount.Id,
                                        Account = product.BankDepositAccount.LedgerAccount,
                                        Debit = currentDepositBalance + currentInterestBalance,
                                        TransactionDate = request.TransactionDate
                                    });

                                    journal.JournalEntries.Add(new JournalEntry
                                    {
                                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                                        EntryType = TransactionEntryType.CREDIT,
                                        AccountId = txn.CustomerBankLiquidationAccount.LedgerAccount.Id,
                                        Account = txn.CustomerBankLiquidationAccount.LedgerAccount,
                                        Credit = currentDepositBalance + currentInterestBalance,
                                        TransactionDate = request.TransactionDate
                                    });

                                    break;
                                }




                        }


                        //close current FD account
                        currentDepositAccount.IsClosed = true;
                        currentDepositAccount.DateClosed = DateTime.Now;

                        break;
                    }


                default:
                    {
                        break;
                    }
            }





            if (charges.Any())
            {
                // Get total charge amount
                foreach (var charge in charges)
                {
                    //track charges Product
                    //DR FD_PROD_CHARGE_ACCRUAL_CONTROL 1,000
                    //CR FDProduct.ChargesAccrualAccount 1,000
                    journal.JournalEntries.Add(new JournalEntry
                    {
                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                        EntryType = TransactionEntryType.DEBIT,
                        AccountId = FD_PROD_CHARGE_ACCRUAL_CONTROL.Id,
                        Account = FD_PROD_CHARGE_ACCRUAL_CONTROL,
                        Debit = charge.LiquidationCharge,
                        TransactionDate = request.TransactionDate
                    });

                    journal.JournalEntries.Add(new JournalEntry
                    {
                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                        EntryType = TransactionEntryType.CREDIT,
                        AccountId = FD_PROD_CHARGES_ACCRUAL.Id,
                        Account = FD_PROD_CHARGES_ACCRUAL,
                        Credit = charge.LiquidationCharge,
                        TransactionDate = request.TransactionDate
                    });


                    //track charges Account
                    //DR FD_ACC_CHARGE_ACCRUAL_CONTROL 1,000
                    //CR FDAccount.ChargesAccruedAccount 1,000

                    journal.JournalEntries.Add(new JournalEntry
                    {
                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                        EntryType = TransactionEntryType.DEBIT,
                        AccountId = FD_ACC_CHARGE_ACCRUAL_CONTROL.Id,
                        Account = FD_ACC_CHARGE_ACCRUAL_CONTROL,
                        Debit = charge.LiquidationCharge,
                        TransactionDate = request.TransactionDate
                    });

                    journal.JournalEntries.Add(new JournalEntry
                    {
                        TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                        EntryType = TransactionEntryType.CREDIT,
                        AccountId = FD_ACC_CHARGES_ACCRUAL.Id,
                        Account = FD_ACC_CHARGES_ACCRUAL,
                        Credit = charge.LiquidationCharge,
                        TransactionDate = request.TransactionDate
                    });
                }
            }



            dbContext.TransactionJournals.Add(journal);
            await dbContext.SaveChangesAsync(cancellationToken);

            journal.Post();

            dbContext.Update(journal);
            await dbContext.SaveChangesAsync(cancellationToken);

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


    private DepositTransactionViewModel ProcessFixedDepositInterestAddition(DepositTransactionCommand request,
      CancellationToken cancellationToken)
    {
        /*
          * 

         v1

         SD Interest addition
         DR SD/FD_PROD_INTEREST_ACCRUAL_CONTROL
         CR Product interest payable account fundingAmount

         DR SD/FD_ACC_INTEREST_ACCRUAL_CONTROL  
         CR Account interest earned account fundingAmount

         DR SD/FD_PROD_INTEREST_ADD_CONTROL
         CR Prod deposit account fundingAmount

         DR SD/FD_ACC_INTEREST_ADD_CONTROL
         CR Account deposit account fundingAmount

          * 
          * 
          */


        DepositTransactionViewModel rsp = new DepositTransactionViewModel();

        try
        {

            TransactionJournal journal = new TransactionJournal();
            journal.TransactionNo = NHiloHelper.GetNextKey(nameof(TransactionJournal)).ToString();
            journal.Title = $"Fixed Deposit Interest Addition";
            journal.TransactionType = request.TransactionType;
            journal.TransactionDate = request.TransactionDate;
            journal.DocumentRef = nameof(FixedDepositAccount);
            journal.DocumentRefId = request.DepositAccountId;
            journal.EntityRef = nameof(FixedDepositInterestAddition);
            journal.EntityRefId = request.EntityId;

            var txn = dbContext.FixedDepositInterestAdditions.FirstOrDefault(p => p.Id == request.EntityId);

            var depositAccount = dbContext.FixedDepositAccounts.Where(r => r.Id == request.DepositAccountId)
                .Include(p => p.DepositProduct)
                .ThenInclude(p => p.BankDepositAccount)
                .ThenInclude(l => l.LedgerAccount)
                .Include(p => p.DepositProduct).ThenInclude(p => p.ProductDepositAccount)
                .Include(p => p.DepositProduct).ThenInclude(p => p.InterestPayableAccount)
                .Include(p => p.Customer).ThenInclude(p => p.CashAccount)
                .FirstOrDefault();


            LedgerAccount FD_COY_BANK_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.FD_COY_BANK_CONTROL.ToString());
            LedgerAccount FD_PROD_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.FD_PROD_CONTROL.ToString());
            LedgerAccount FD_CUST_BANK_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.FD_CUST_BANK_CONTROL.ToString());


            LedgerAccount FD_PROD_INTEREST_ACCRUAL_CONTROL =
               dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.FD_PROD_INTEREST_ACCRUAL_CONTROL.ToString());
            LedgerAccount FD_ACC_INTEREST_ACCRUAL_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.FD_ACC_INTEREST_ACCRUAL_CONTROL.ToString());
            LedgerAccount FD_PROD_INTEREST_ADD_CONTROL =
                dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.FD_PROD_INTEREST_ADD_CONTROL.ToString());
            LedgerAccount FD_ACC_INTEREST_ADD_CONTROL =
               dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == ControlAccounts.FD_ACC_INTEREST_ADD_CONTROL.ToString());


            LedgerAccount CUST_CASH_ACC = depositAccount.Customer.CashAccount;
            LedgerAccount COY_BANK_ACC = depositAccount.DepositProduct.BankDepositAccount.LedgerAccount;
            LedgerAccount PROD_DEPOSIT_ACC = depositAccount.DepositProduct.ProductDepositAccount;
            LedgerAccount PROD_INTEREST_PAYABLE = depositAccount.DepositProduct.InterestPayableAccount;
            LedgerAccount CUST_DEPOSIT_ACC = depositAccount.DepositAccount;


            //DR SD/ FD_PROD_INTEREST_ACCRUAL_CONTROL
            //CR Product interest payable account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = FD_PROD_INTEREST_ACCRUAL_CONTROL.Id,
                Account = FD_PROD_INTEREST_ACCRUAL_CONTROL,
                Debit = txn.InterestEarned,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = PROD_INTEREST_PAYABLE.Id,
                Account = PROD_INTEREST_PAYABLE,
                Credit = txn.InterestEarned,
                TransactionDate = request.TransactionDate
            });


            //DR SD/ FD_ACC_INTEREST_ACCRUAL_CONTROL
            //CR Account interest earned account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = FD_ACC_INTEREST_ACCRUAL_CONTROL.Id,
                Account = FD_ACC_INTEREST_ACCRUAL_CONTROL,
                Debit = txn.InterestEarned,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = depositAccount.InterestEarnedAccountId,
                Account = depositAccount.InterestEarnedAccount,
                Credit = txn.InterestEarned,
                TransactionDate = request.TransactionDate
            });


            //DR SD/ FD_PROD_INTEREST_ADD_CONTROL
            //CR Prod deposit account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = FD_PROD_INTEREST_ADD_CONTROL.Id,
                Account = FD_PROD_INTEREST_ADD_CONTROL,
                Debit = txn.InterestEarned,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = PROD_DEPOSIT_ACC.Id,
                Account = PROD_DEPOSIT_ACC,
                Credit = txn.InterestEarned,
                TransactionDate = request.TransactionDate
            });



            //DR SD/ FD_ACC_INTEREST_ADD_CONTROL
            //CR Account deposit account fundingAmount
            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.DEBIT,
                AccountId = FD_ACC_INTEREST_ADD_CONTROL.Id,
                Account = FD_ACC_INTEREST_ADD_CONTROL,
                Debit = txn.InterestEarned,
                TransactionDate = request.TransactionDate
            });

            journal.JournalEntries.Add(new JournalEntry
            {
                TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                EntryType = TransactionEntryType.CREDIT,
                AccountId = CUST_DEPOSIT_ACC.Id,
                Account = CUST_DEPOSIT_ACC,
                Credit = txn.InterestEarned,
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
            logger.LogError(ex, ex.Message);
            rsp.ErrorDate = DateTime.Now;
            rsp.ErrorDetails = ex.Message;
            rsp.ErrorFlag = true;

            //throw ex;

        }


        return rsp;

    }

}