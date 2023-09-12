using AP.ChevronCoop.AppCore.Services.ApprovalServices;
using AP.ChevronCoop.AppCore.Services.Helpers;
using AP.ChevronCoop.AppCore.Services.LogServices;
using AP.ChevronCoop.AppDomain.Accounting.LedgerAccounts;
using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductCharges;
using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductInterestRanges;
using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProducts;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.Deposits.Products.CustomerDepositProductPublications;
using AP.ChevronCoop.Entities.Deposits.Products.DepartmentDepositProductPublications;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProductCharges;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProductInterestRanges;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AP.ChevronCoop.AppCore.Deposits.DepositProducts.DepositProducts
{

    public class DepositProductCommandHandler :
        IRequestHandler<QueryDepositProductCommand, CommandResult<IQueryable<DepositProduct>>>,
        IRequestHandler<CreateDepositProductCommand, CommandResult<DepositProductViewModel>>,
        IRequestHandler<GetDepositProductCommand, CommandResult<GetDepositProductViewModel>>,
        IRequestHandler<UpdateDepositProductCommand, CommandResult<DepositProductViewModel>>,
        IRequestHandler<PublishDepositProductCommand, CommandResult<DepositProductViewModel>>,
        IRequestHandler<UpdateDepositProductStatusCommand, CommandResult<DepositProductViewModel>>,
        IRequestHandler<DeleteDepositProductCommand, CommandResult<string>>
    {
        private readonly ILoggerService _logger;
        private readonly ChevronCoopDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IManageApprovalService _approvalLog;

        public DepositProductCommandHandler(ChevronCoopDbContext appDbContext, IMediator mediator,
            UserManager<ApplicationUser> userManager,
            ILoggerService logger, IMapper _mapper, IManageApprovalService approvalLog)
        {
            dbContext = appDbContext;
            _logger = logger;
            mapper = _mapper;
            _mediator = mediator;
            _userManager = userManager;
            _approvalLog = approvalLog;
        }

        public Task<CommandResult<IQueryable<DepositProduct>>> Handle(QueryDepositProductCommand request,
            CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<IQueryable<DepositProduct>>();
            rsp.Response = dbContext.DepositProducts.Include(p => p.InterestRanges).Include(p => p.Charges);
            return Task.FromResult(rsp);
        }

        public async Task<CommandResult<GetDepositProductViewModel>> Handle(GetDepositProductCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<GetDepositProductViewModel>();
            var depositProduct =
                await dbContext.DepositProducts.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            var response = mapper.Map<GetDepositProductViewModel>(depositProduct);

            var glAccountIds = new HashSet<string>
            {
                response.ProductDepositAccountId,
                response.BankDepositAccountId,
                response.ChargesAccrualAccountId,
                response.ChargesIncomeAccountId,
                response.ChargesAccrualAccountId,
                response.InterestPayableAccountId,
                response.InterestPayoutAccountId
            };

            var ledgerAccounts = await dbContext.LedgerAccounts
                .Where(x => glAccountIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

            response.LedgerAccounts = mapper.Map<List<LedgerAccountViewModel>>(ledgerAccounts) ??
                                      new List<LedgerAccountViewModel>();

            var productCharges = await dbContext.DepositProductCharges.Where(x => x.ProductId == request.Id)
                .ToListAsync(cancellationToken);
            var interestRanges = await dbContext.DepositProductInterestRanges.Where(x => x.ProductId == request.Id)
                .ToListAsync(cancellationToken);

            response.ProductCharges = mapper.Map<List<DepositProductChargeViewModel>>(productCharges) ??
                                      new List<DepositProductChargeViewModel>();
            response.InterestRanges = mapper.Map<List<DepositProductInterestRangeViewModel>>(interestRanges) ??
                                      new List<DepositProductInterestRangeViewModel>();

            rsp.Response = response;
            return rsp;
        }

        public async Task<CommandResult<DepositProductViewModel>> Handle(CreateDepositProductCommand request,
            CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<DepositProductViewModel>();
            var entity = mapper.Map<DepositProduct>(request);
            entity.Status = ProductStatus.PENDING_APPROVAL;
            if (request.Tenure == Tenure.NONE) request.TenureValue = 0;

            var ProductDepositAccount = new LedgerAccount();
            ProductDepositAccount.Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString();
            ProductDepositAccount.Name = $"Deposit Product - Deposit Account ({request.Code})";
            ProductDepositAccount.IsOfficeAccount = true;
            ProductDepositAccount.AccountType = COAType.CONTROL;
            ProductDepositAccount.AllowManualEntry = true;
            ProductDepositAccount.CurrencyId = request.DefaultCurrencyId;
            dbContext.LedgerAccounts.Add(ProductDepositAccount);

            entity.ProductDepositAccount = ProductDepositAccount;


            var ChargesIncomeAccount = new LedgerAccount();
            ChargesIncomeAccount.Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString();
            ChargesIncomeAccount.Name = $"Deposit Product - Charges Income Account ({request.Code})";
            ChargesIncomeAccount.IsOfficeAccount = true;
            ChargesIncomeAccount.AccountType = COAType.CONTROL;
            ChargesIncomeAccount.AllowManualEntry = true;
            ChargesIncomeAccount.CurrencyId = request.DefaultCurrencyId;
            dbContext.LedgerAccounts.Add(ChargesIncomeAccount);

            entity.ChargesIncomeAccount = ChargesIncomeAccount;


            var ChargesAccrualAccount = new LedgerAccount();
            ChargesAccrualAccount.Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString();
            ChargesAccrualAccount.Name = $"Deposit Product - Accrued Charges Account ({request.Code})";
            ChargesAccrualAccount.IsOfficeAccount = true;
            ChargesAccrualAccount.AccountType = COAType.CONTROL;
            ChargesAccrualAccount.AllowManualEntry = true;
            ChargesAccrualAccount.CurrencyId = request.DefaultCurrencyId;
            dbContext.LedgerAccounts.Add(ChargesAccrualAccount);

            entity.ChargesAccrualAccount = ChargesAccrualAccount;


            var InterestPayableAccount = new LedgerAccount();
            InterestPayableAccount.Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString();
            InterestPayableAccount.Name = $"Deposit Product - Interest Payable Account ({request.Code})";
            InterestPayableAccount.IsOfficeAccount = true;
            InterestPayableAccount.AccountType = COAType.CONTROL;
            InterestPayableAccount.AllowManualEntry = true;
            InterestPayableAccount.CurrencyId = request.DefaultCurrencyId;
            dbContext.LedgerAccounts.Add(InterestPayableAccount);

            entity.InterestPayableAccount = InterestPayableAccount;


            var InterestPayoutAccount = new LedgerAccount();
            InterestPayoutAccount.Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString();
            InterestPayoutAccount.Name = $"Deposit Product - Interest Payout Account ({request.Code})";
            InterestPayoutAccount.IsOfficeAccount = true;
            InterestPayoutAccount.AccountType = COAType.CONTROL;
            InterestPayoutAccount.AllowManualEntry = true;
            InterestPayoutAccount.CurrencyId = request.DefaultCurrencyId;
            dbContext.LedgerAccounts.Add(InterestPayoutAccount);

            entity.InterestPayoutAccount = InterestPayoutAccount;


            var ChargesWaivedAccount = new LedgerAccount();
            ChargesWaivedAccount.Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString();
            ChargesWaivedAccount.Name = $"Deposit Product - Charges Waived Account ({request.Code})";
            ChargesWaivedAccount.IsOfficeAccount = true;
            ChargesWaivedAccount.AccountType = COAType.CONTROL;
            ChargesWaivedAccount.AllowManualEntry = true;
            ChargesWaivedAccount.CurrencyId = request.DefaultCurrencyId;
            dbContext.LedgerAccounts.Add(ChargesWaivedAccount);

            entity.ChargesWaivedAccount = ChargesWaivedAccount;


            if (request.ProductCharges != null && request.ProductCharges.Count > 0)
            {
                foreach (var charge in request.ProductCharges)
                {
                    var productCharge = new DepositProductCharge
                    {
                        ChargeId = charge.ChargeId
                    };

                    entity.Charges.Add(productCharge);
                }
            }

            if (request.InterestRanges != null)
            {
                //always clear, bcos Automapper will bind InterestRanges on command object to InterestRanges field on entity
                entity.InterestRanges?.Clear();

                foreach (var range in request.InterestRanges)
                {
                    var interestRange = new DepositProductInterestRange
                    {
                        LowerLimit = range.LowerLimit,
                        UpperLimit = range.UpperLimit,
                        InterestRate = range.InterestRate
                    };

                    entity.InterestRanges.Add(interestRange);
                }
            }


            entity.CreatedByUserId = request.CreatedByUserId;
            entity.Status = ProductStatus.PENDING_APPROVAL;
            dbContext.DepositProducts.Add(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            // Create approval
           
            var approvalRequest = new CreateApprovalModel
            {
                Module = "DepositProduct",
                Payload = JsonConvert.SerializeObject(request),
                Comment = "Create deposit product approval initiated",
                ApprovalType = ApprovalType.DEPOSIT_PRODUCT,
                Description = $"Deposit Product - {entity.Name} ({entity.Code})",
                CreatedBy = request.ApplicationUserId,
                EntityId = entity.Id,
                EntityType = typeof(CreateDepositProductCommand)
            };

            var approval = await _approvalLog.CreateApproval(approvalRequest, true);

            if (approval.StatusCode == StatusCodes.Status201Created)
            {
                entity.ApprovalId = approval.Response.Id;
                dbContext.DepositProducts.Update(entity);
            }

            await dbContext.SaveChangesAsync(cancellationToken);


            rsp.Response = mapper.Map<DepositProductViewModel>(entity);
            return rsp;
        }

        public async Task<CommandResult<DepositProductViewModel>> Handle(UpdateDepositProductCommand request,
            CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<DepositProductViewModel>();
            var entity = await dbContext.DepositProducts.FindAsync(request.Id);
            mapper.Map(request, entity);


            // TODO: optimize this code   InterestRanges   ProductCharges
            var depositProductCharges = await dbContext.DepositProductCharges.Where(x => x.ProductId == request.Id)
                .ToListAsync(cancellationToken);
            dbContext.DepositProductCharges.RemoveRange(depositProductCharges);

            if (request.ProductCharges != null && request.ProductCharges.Count > 0)
                foreach (var charge in request.ProductCharges)
                {
                    var productCharge = new DepositProductCharge
                    {
                        ChargeId = charge.ChargeId
                    };

                    entity.Charges.Add(productCharge);
                }

            var depositProductInterestRanges = await dbContext.DepositProductInterestRanges
                .Where(x => x.ProductId == request.Id).ToListAsync(cancellationToken);
            dbContext.DepositProductInterestRanges.RemoveRange(depositProductInterestRanges);

            if (request.InterestRanges != null && request.InterestRanges.Count > 0)
            {
                //always clear, bcos Automapper will bind InterestRanges on command object to InterestRanges field on entity
                entity.InterestRanges?.Clear();

                foreach (var range in request.InterestRanges)
                {
                    var interestRange = new DepositProductInterestRange
                    {
                        LowerLimit = range.LowerLimit,
                        UpperLimit = range.UpperLimit,
                        InterestRate = range.InterestRate
                    };

                    entity.InterestRanges.Add(interestRange);
                }
            }

            entity.UpdatedByUserId = request.UpdatedByUserId;
            dbContext.DepositProducts.Update(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            rsp.Response = mapper.Map<DepositProductViewModel>(entity);
            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteDepositProductCommand request,
            CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.DepositProducts.FindAsync(request.Id);

            dbContext.DepositProducts.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }

        public async Task<CommandResult<DepositProductViewModel>> Handle(PublishDepositProductCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<DepositProductViewModel>();

            var depositProduct = await dbContext.DepositProducts.FirstOrDefaultAsync(x => x.Id == request.ProductId);

            depositProduct.PublicationType = request.PublicationType;
            depositProduct.PublishedByUserId = request.PublishedByUserId;
            depositProduct.Status = ProductStatus.PUBLISHED;

            if (request.PublicationType == PublicationType.CUSTOMER)
            {
                var publications = new List<CustomerDepositProductPublication>();
                foreach (var target in request.Targets)
                    publications.Add(new CustomerDepositProductPublication
                    {
                        CustomerId = target,
                        ProductId = request.ProductId,
                        PublicationType = PublicationType.CUSTOMER,
                        CreatedByUserId = request.PublishedByUserId,
                        Description = $"{nameof(PublicationType.CUSTOMER)} product publication"
                    });

                await dbContext.CustomerDepositProductPublications.AddRangeAsync(publications, cancellationToken);
            }

            if (request.PublicationType == PublicationType.DEPARTMENT)
            {
                var publications = new List<DepartmentDepositProductPublication>();
                foreach (var target in request.Targets)
                    publications.Add(new DepartmentDepositProductPublication
                    {
                        DepartmentId = target,
                        ProductId = request.ProductId,
                        PublicationType = PublicationType.DEPARTMENT,
                        CreatedByUserId = request.PublishedByUserId,
                        Description = $"{nameof(PublicationType.DEPARTMENT)} product publication"
                    });

                await dbContext.DepartmentDepositProductPublications.AddRangeAsync(publications, cancellationToken);
            }
            if (request.PublicationType == PublicationType.ALL)
            {
                var publication = new CustomerDepositProductPublication
                {
                    ProductId = request.ProductId,
                    PublicationType = PublicationType.ALL,
                    CreatedByUserId = request.PublishedByUserId,
                    Description = $"{nameof(PublicationType.ALL)} product publication"
                };
                 
                await dbContext.CustomerDepositProductPublications.AddAsync(publication);

            }

            dbContext.DepositProducts.Update(depositProduct);

            await dbContext.SaveChangesAsync(cancellationToken);

            rsp.Response = mapper.Map<DepositProductViewModel>(depositProduct);
            return rsp;
        }
        public async Task<CommandResult<DepositProductViewModel>> Handle(UpdateDepositProductStatusCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<DepositProductViewModel>();

            var entity = await dbContext.DepositProducts.FindAsync(request.DepositProductId);
            entity.Status = request.Status;
            dbContext.DepositProducts.Update(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            rsp.Response = mapper.Map<DepositProductViewModel>(entity);
            return rsp;
        }
    }
}


