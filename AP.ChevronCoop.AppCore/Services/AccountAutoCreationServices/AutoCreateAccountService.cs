using AP.ChevronCoop.AppCore.Services.Helpers;
using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccountApplications;
using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccounts;
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccountApplications;
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccountApplications;
using AP.ChevronCoop.Entities.Security;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AP.ChevronCoop.AppCore.Services.AccountAutoCreationServices
{
    public class AutoCreateAccountService : IAutoCreateAccountService
    {
        private readonly ChevronCoopDbContext _dbContext;
        private readonly IMediator _mediator;
        private readonly CoreAppSettings coreAppSettings;
        public AutoCreateAccountService(ChevronCoopDbContext dbContext, IMediator mediator , IOptions<CoreAppSettings> options )
        {
            _dbContext = dbContext;
            _mediator = mediator;
            coreAppSettings = options.Value;
        }
        public Task<bool> CheckDefaultProductsAsync()
        {
            var exist = false;

            var savings = _dbContext.DepositProducts.Where(p => p.ProductType == DepositProductType.SAVINGS && p.IsDefaultProduct == true).FirstOrDefault();

            var sDeposit = _dbContext.DepositProducts.Where(p => p.ProductType == DepositProductType.SPECIAL_DEPOSIT && p.IsDefaultProduct == true).FirstOrDefault();
            
            if(savings!=null && sDeposit!=null)
                exist = true;

            return Task.FromResult(exist);

        }

        public async Task<CommandResult<(bool Completed, string Message)>> CreateSpecialAndSavingsAccountAsync(MemberProfile memberProfile)
        {
            var rsp = new CommandResult<(bool, string)>();
            var msg = "Created.";
            var customer = _dbContext.Customers.Where(m => m.ApplicationUserId == memberProfile.ApplicationUserId).FirstOrDefault();
            var customerBankAccount = _dbContext.CustomerBankAccounts.Where(m => m.CustomerId == customer.Id).FirstOrDefault();

            var savingsProduct = _dbContext.DepositProducts.Where(p => p.ProductType == DepositProductType.SAVINGS && p.IsDefaultProduct == true).FirstOrDefault();

            var sDepositProduct = _dbContext.DepositProducts.Where(p => p.ProductType == DepositProductType.SPECIAL_DEPOSIT && p.IsDefaultProduct == true).FirstOrDefault();

          var sdInterest = _dbContext.DepositProductInterestRanges.FirstOrDefault(p => p.ProductId == sDepositProduct.Id);
            var savingsdInterest = _dbContext.DepositProductInterestRanges.FirstOrDefault(p => p.ProductId == savingsProduct.Id);

         var interestRate =   sdInterest?.InterestRate == null ? 0 : sdInterest.InterestRate;

            decimal savingsPayrollAmount = GetSavingsAmount(customer);

            var savingsCommand = new CreateSavingsAccountApplicationCommand
            {
                Amount = savingsPayrollAmount,
                CreatedByUserId = memberProfile.ApplicationUserId,
                CustomerId = customer.Id,
                DepositProductId = savingsProduct.Id,
            };

            var savingsApplicationResponse = await _mediator.Send(savingsCommand);

            var depositCommand = new CreateSpecialDepositAccountApplicationCommand
            {
                Amount = 0,
                PaymentAccountNumber = customerBankAccount.AccountNumber,
                PaymentBankName = customerBankAccount.AccountName,
                ModeOfPayment = DepositFundingSourceType.NONE,
                InterestRate = interestRate,
                CreatedByUserId = memberProfile.ApplicationUserId,
                CustomerId = customer.Id,
                DepositProductId = sDepositProduct.Id,
            };

            var specialDepositApplicationReponse = await _mediator.Send(depositCommand);

            var savingsApplication = _dbContext.SavingsAccountApplications.Where(s => s.CustomerId == customer.Id).Include(s=>s.Customer).FirstOrDefault();
            if (savingsApplication != null)
            {
                var command = new CreateSavingsAccountCommand
                {
                    ApplicationId = savingsApplication.Id,
                    PayrollAmount = savingsApplication.Amount,
                    CreatedByUserId = savingsApplication.CreatedByUserId,
                    CustomerId = savingsApplication.CustomerId,
                    DepositProductId = savingsApplication.DepositProductId,
                };

                var createSavingsRsp = await _mediator.Send(command);

                if(createSavingsRsp.StatusCode==StatusCodes.Status200OK)
                {
                    var approval = _dbContext.Approvals.Where(s => s.EntityId == savingsApplication.Id).FirstOrDefault();
                    if (approval!=null)
                    {
                        approval.Status = ApprovalStatus.APPROVED;
                      await  _dbContext.SaveChangesAsync();
                    }
                }
            }

        

        var specialDepositApplication = _dbContext.SpecialDepositAccountApplications.Where(s => s.CustomerId == customer.Id).FirstOrDefault();
            if (specialDepositApplication != null)
            {
                var creatSDAcccountommand = new CreateSpecialDepositAccountCommand
                {
                    ApplicationId = specialDepositApplication.Id,
                    CreatedByUserId = specialDepositApplication.CreatedByUserId,
                    CustomerId = specialDepositApplication.CustomerId,
                    DepositProductId = specialDepositApplication.DepositProductId,
                    AccountNo = NHiloHelper.GetNextKey(nameof(SpecialDepositAccountApplication)).ToString(),
                    FundingAmount = specialDepositApplication.Amount,
                    InterestRate = interestRate,
                };
                var createSDRsp = await _mediator.Send(creatSDAcccountommand);

                if (createSDRsp.StatusCode == StatusCodes.Status200OK)
                {
                    var approval = _dbContext.Approvals.Where(s => s.EntityId == specialDepositApplication.Id).FirstOrDefault();
                    if (approval != null)
                    {
                        approval.Status = ApprovalStatus.APPROVED;
                        await _dbContext.SaveChangesAsync();
                    }
                }
            }

            rsp.Response = new(true, msg);
            return rsp;
        }
  
        public async Task<CommandResult<(bool Completed, string Message)>> GetCreateSpecialAndSavingsAccountResultAsync(string applicationUserId)
        {
            var rsp = new CommandResult<(bool, string)>();
            var msg = "";
            var customer = await _dbContext.Customers.Where(c => c.ApplicationUserId == applicationUserId).FirstOrDefaultAsync();
            if (customer == null) 
            {
                rsp.Response = new(true, "Customer not found.");
                return rsp;
            }
           
            var savingsAccount = _dbContext.SavingsAccounts.Where(s => s.CustomerId == customer.Id).FirstOrDefault();
            var specialDepositAccount = _dbContext.SpecialDepositAccounts.Where(s => s.CustomerId == customer.Id).FirstOrDefault();


            if (specialDepositAccount == null && savingsAccount == null)
            {
                rsp.Response = new(false, $"Failed t o create default Accounts for {customer?.FirstName}{customer?.MiddleName}{customer?.LastName} ({customer?.CustomerNo}).");
                return rsp;
            }

            if (specialDepositAccount == null || savingsAccount == null)
            {
                var sv = savingsAccount != null ? $"({savingsAccount?.AccountNo}  created" : "accounts not created";
                var sd = specialDepositAccount != null ? $"({specialDepositAccount?.AccountNo} accounts created" : "accounts not created";

                msg = $"You have processed your approval successfully. One of the default accounts not created. Savings AccountNo -({sv}) and Special Deposit AccountNo - {sd})  for {customer?.FirstName}{customer?.MiddleName}{customer?.LastName} ({customer?.CustomerNo}).";
                rsp.Response = new(true, msg);
                return rsp;
            }

            msg = $"You have processed your approval successfully. Savings({savingsAccount.AccountNo}) and Special Deposit({specialDepositAccount.AccountNo}) accounts created for {customer?.FirstName}{customer?.MiddleName}{customer?.LastName} ({customer?.CustomerNo}).";

            rsp.Response = new(true, msg);
            return rsp;
        }


        #region private methods

        private decimal GetSavingsAmount(Customer customer)
        {
            decimal amount = 0m;
            switch (customer.MemberType)
            {
                case MemberType.REGULAR:
                    amount = coreAppSettings.SavingRegularDefaultPayrollAmount;
                    break;
                case MemberType.RETIREE:
                    amount = coreAppSettings.SavingRetireDefaultPayrollAmount;
                    break;
                default:
                    amount = 0;
                    break;
            }
            return amount;
        }
        #endregion
    }
}
