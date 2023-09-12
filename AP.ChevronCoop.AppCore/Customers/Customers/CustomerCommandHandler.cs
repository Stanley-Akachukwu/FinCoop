using AP.ChevronCoop.AppCore.Services.AccountAutoCreationServices;
using AP.ChevronCoop.AppCore.Services.Helpers;
using AP.ChevronCoop.AppDomain.Customers.Customers;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBeneficiaries;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerNextOfKins;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Security;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace AP.ChevronCoop.AppCore.Customers.Customers
{
    public class CustomerCommandHandler :
     IRequestHandler<QueryCustomerCommand, CommandResult<IQueryable<Customer>>>,
    IRequestHandler<CreateCustomerCommand, CommandResult<CustomerViewModel>>,
    IRequestHandler<UpdateCustomerCommand, CommandResult<CustomerViewModel>>,
    IRequestHandler<DeleteCustomerCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        private readonly IAutoCreateAccountService _autoCreateAccountService;

        public CustomerCommandHandler(ChevronCoopDbContext appDbContext, IAutoCreateAccountService autoCreateAccountService,
        ILogger<CustomerCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;
            _autoCreateAccountService = autoCreateAccountService;
        }


        public Task<CommandResult<IQueryable<Customer>>> Handle(QueryCustomerCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<Customer>>();
            rsp.Response = dbContext.Customers;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<CustomerViewModel>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<CustomerViewModel>();
            // var entity = new Customer();

            var memberProfile = await dbContext.MemberProfiles.FirstOrDefaultAsync(x => x.Id == request.ProfileId, cancellationToken: cancellationToken);

            memberProfile!.Status = MemberProfileStatus.ACTIVE;
            dbContext.MemberProfiles.Update(memberProfile);

            var customer = await dbContext.Customers.FirstOrDefaultAsync(x => x.ApplicationUserId == memberProfile.ApplicationUserId);
            if (customer != null)
            {
               
                mapper.Map(memberProfile, customer);
                customer.Address = memberProfile.Address;
                customer.PrimaryEmail = memberProfile.PrimaryEmail;
                customer.PrimaryPhone = memberProfile.PrimaryEmail;
                customer.SecondaryEmail = memberProfile.SecondaryEmail;
                customer.SecondaryPhone = memberProfile.SecondaryPhone;
                customer.DateOfEmployment = memberProfile.DateOfEmployment;
                customer.Country = memberProfile.Country;
                customer.Gender = memberProfile.Gender;
                customer.State = memberProfile.State;
                customer.FirstName = memberProfile.FirstName;
                customer.MiddleName = memberProfile.MiddleName;
                customer.LastName = memberProfile.LastName;
                customer.MemberId = memberProfile.MembershipId;
                customer.IdentificationNumber = memberProfile.IdentificationNumber;
                customer.DepartmentId = memberProfile.DepartmentId;
                customer.IdentificationType = memberProfile.IdentificationType;
                customer.YearsOfService = memberProfile.YearsOfService;
                customer.StateOfOrigin = memberProfile.StateOfOrigin;
                customer.JobRole = memberProfile.JobRole;
                customer.OfficeAddress = memberProfile.OfficeAddress;
                customer.MemberType = memberProfile.MemberType;
                customer.PassportUrl = memberProfile.PassportUrl;
                customer.ResidentialAddress = memberProfile.ResidentialAddress;
                customer.RetireeNumber = memberProfile.RetireeNumber;
                customer.CAI = memberProfile.CAI;
                customer.DOB = memberProfile.DOB;
                
                customer.Status = MemberProfileStatus.ACTIVE;

                dbContext.Customers.Update(customer);
                await dbContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                customer = mapper.Map<Customer>(memberProfile);
                customer.CustomerNo = NHiloHelper.GetNextKey(nameof(Customer)).ToString();  
                customer.MemberId = memberProfile.MembershipId;

                var currency = dbContext.Currencies.FirstOrDefault(x => x.Code.ToLower() == "ngn");

                var CashAccount = new LedgerAccount();
                var accountCode = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString();
                CashAccount.Code = accountCode;
                CashAccount.Name = $"Customer Cash Account ({customer.CustomerNo}-{accountCode})";
                CashAccount.IsOfficeAccount = true;
                CashAccount.AccountType = COAType.CONTROL;
                CashAccount.AllowManualEntry = true;
                CashAccount.CurrencyId = currency.Id;
                dbContext.LedgerAccounts.Add(CashAccount);

                customer.CashAccount = CashAccount;
                dbContext.Customers.Add(customer);

                var memberBeneficiaries = await dbContext.MemberBeneficiaries.Where(x => x.ProfileId == memberProfile.Id).ToListAsync();
                if (memberBeneficiaries.Any())
                {
                    var customerBeneficiariesMap = new List<CustomerBeneficiary>();
                    foreach (var memberBeneficiary in memberBeneficiaries)
                    {
                        var customerBeneficiaryMap = mapper.Map<CustomerBeneficiary>(memberBeneficiary);
                        customerBeneficiaryMap.CustomerId = customer.Id;
                        customerBeneficiariesMap.Add(customerBeneficiaryMap);
                    }

                    await dbContext.CustomerBeneficiaries.AddRangeAsync(customerBeneficiariesMap, cancellationToken);
                }
                
            
                var memberBankAccounts = await dbContext.MemberBankAccounts.Where(x => x.ProfileId == memberProfile.Id).ToListAsync();
                if (memberBankAccounts.Any())
                {
                    var customerBankAccountsMap = new List<CustomerBankAccount>();
                    foreach (var memberBankAccount in memberBankAccounts)
                    {
                        var bankAccountMap = mapper.Map<CustomerBankAccount>(memberBankAccount);
                        bankAccountMap.CustomerId = customer.Id;
                            
                        var ledger = new LedgerAccount();
                        ledger.Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString();
                        ledger.Name = $"Customer Bank Account ({customer.CustomerNo}-{bankAccountMap.AccountNumber})";
                        ledger.IsOfficeAccount = true;
                        ledger.AccountType = COAType.CONTROL;
                        ledger.AllowManualEntry = true;
                        ledger.CurrencyId = currency.Id;
                        dbContext.LedgerAccounts.Add(ledger);

                        bankAccountMap.LedgerAccount = ledger;
                        customerBankAccountsMap.Add(bankAccountMap);
                    }

                    await dbContext.CustomerBankAccounts.AddRangeAsync(customerBankAccountsMap, cancellationToken);
                }

                var memberNextOfKins = await dbContext.MemberNextOfKins.Where(x => x.ProfileId == memberProfile.Id).ToListAsync();
                if (memberNextOfKins.Any())
                {
                    var customerNextOfKinsMap = new List<CustomerNextOfKin>();
                    foreach (var nextOfKin in memberNextOfKins)
                    {
                        var nextOfKinMap = mapper.Map<CustomerNextOfKin>(nextOfKin);
                        nextOfKinMap.CustomerId = customer.Id;
                        customerNextOfKinsMap.Add(nextOfKinMap);
                    }

                    await dbContext.CustomerNextOfKins.AddRangeAsync(customerNextOfKinsMap, cancellationToken);
                }
                await dbContext.SaveChangesAsync(cancellationToken);


                var result = await _autoCreateAccountService.CreateSpecialAndSavingsAccountAsync(memberProfile);
                
                if (result.Response.Item1)
                {
                    rsp.Message = result.Response.Item2;
                }
            } 

            rsp.Response = mapper.Map<CustomerViewModel>(customer);

            return rsp;
        }

        public async Task<CommandResult<CustomerViewModel>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<CustomerViewModel>();
            var entity = await dbContext.Customers.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.Customers.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<CustomerViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.Customers.FindAsync(request.Id);

            dbContext.Customers.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }








}
