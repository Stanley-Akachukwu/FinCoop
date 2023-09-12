using System.Net;
using System.Reflection;
using AP.ChevronCoop.AppCore.Services.ApprovalServices;
using AP.ChevronCoop.AppCore.Services.Helpers;
using AP.ChevronCoop.AppDomain.Accounting.LedgerAccounts;
using AP.ChevronCoop.AppDomain.Customers.Customers;
using AP.ChevronCoop.AppDomain.Loans.LoanProductCharges;
using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using AP.ChevronCoop.AppDomain.MasterData.Departments;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.Loans.DepartmentLoanProductPublications;
using AP.ChevronCoop.Entities.Loans.LoanProductCharges;
using AP.ChevronCoop.Entities.Loans.LoanProductPublications.DepartmentLoanProductPublications;
using AP.ChevronCoop.Entities.Loans.LoanProductPublications.MemberLoanProductPublications;
using AP.ChevronCoop.Entities.Loans.LoanProducts;
using AP.ChevronCoop.Entities.Security;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalLogs;
using AutoMapper;
using EllipticCurve.Utils;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AP.ChevronCoop.AppCore.Loans.LoanProducts;

public class LoanProductCommandHandler :
  IRequestHandler<QueryLoanProductCommand, CommandResult<IQueryable<LoanProduct>>>,
  IRequestHandler<GetLoanProductCommand, CommandResult<GetLoanProductViewModel>>,
  IRequestHandler<GetUserLoanProductsCommand, CommandResult<List<LoanProductViewModel>>>,
  IRequestHandler<GetCustomerPublicationByProductIdCommand, CommandResult<List<CustomerViewModel>>>,
  IRequestHandler<GetDepartmentPublicationByProductIdCommand, CommandResult<List<DepartmentViewModel>>>,
  IRequestHandler<CreateLoanProductCommand, CommandResult<LoanProductViewModel>>,
  IRequestHandler<UpdateLoanProductCommand, CommandResult<LoanProductViewModel>>,
  IRequestHandler<UpdateLoanProductStatusCommand, CommandResult<LoanProductViewModel>>,
  IRequestHandler<PublishLoanProductCommand, CommandResult<LoanProductViewModel>>,
  IRequestHandler<DeleteLoanProductCommand, CommandResult<string>>,
  IRequestHandler<CustomerLoanProductEligibilityCommand, CommandResult<bool>>
{
    private readonly IManageApprovalService _approval;
    private readonly IMediator _mediator;
    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger logger;
    private readonly IMapper mapper;

    public LoanProductCommandHandler(ChevronCoopDbContext appDbContext,
      ILogger<LoanProductCommandHandler> _logger, IMapper _mapper, IMediator mediator, IManageApprovalService approval)
    {
        dbContext = appDbContext;
        logger = _logger;
        mapper = _mapper;
        _mediator = mediator;
        _approval = approval;
    }

    public async Task<CommandResult<LoanProductViewModel>> Handle(CreateLoanProductCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<LoanProductViewModel>();
        var entity = mapper.Map<LoanProduct>(request);
        entity.Status = ProductStatus.PENDING_APPROVAL;
        entity.MemberTypeIdList = request.MemberTypes;
        entity.SavingsOffSetProductIdList = request.SavingsOffSets;


        //var key = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString();
        var DisplayTitle = $"{request.Code} - {request.Name}";

        var PrincipalAccount = new LedgerAccount();
        PrincipalAccount.Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString();
        //$"LPPRA-{request.Code}"; //$"LPPRA-{request.Code}";
        PrincipalAccount.Name = $"Loan Product - Principal Receivable Account ({request.Code})";
        PrincipalAccount.IsOfficeAccount = true;
        PrincipalAccount.AccountType = COAType.CONTROL;
        PrincipalAccount.AllowManualEntry = true;
        PrincipalAccount.CurrencyId = request.DefaultCurrencyId;
        dbContext.LedgerAccounts.Add(PrincipalAccount);
        entity.PrincipalAccount = PrincipalAccount;


        var PrincipalLossAccount = new LedgerAccount();
        PrincipalLossAccount.Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString();//$"LPPLA-{request.Code}";
        PrincipalLossAccount.Name = $"Loan Product - Principal Loss Account ({request.Code})";
        PrincipalLossAccount.IsOfficeAccount = true;
        PrincipalLossAccount.AccountType = COAType.CONTROL;
        PrincipalLossAccount.AllowManualEntry = true;
        PrincipalLossAccount.CurrencyId = request.DefaultCurrencyId;
        dbContext.LedgerAccounts.Add(PrincipalLossAccount);
        entity.PrincipalLossAccount = PrincipalLossAccount;


        var InterestIncomeAccount = new LedgerAccount();
        InterestIncomeAccount.Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString();// $"LPIRA-{request.Code}";
        InterestIncomeAccount.Name = $"Loan Product - Interest Receivable Account ({request.Code})";
        InterestIncomeAccount.IsOfficeAccount = true;
        InterestIncomeAccount.AccountType = COAType.CONTROL;
        InterestIncomeAccount.AllowManualEntry = true;
        InterestIncomeAccount.CurrencyId = request.DefaultCurrencyId;
        dbContext.LedgerAccounts.Add(InterestIncomeAccount);
        entity.InterestIncomeAccount = InterestIncomeAccount;


        var PenalInterestReceivableAccount = new LedgerAccount();
        PenalInterestReceivableAccount.Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString();
        PenalInterestReceivableAccount.Name =
          $"Loan Product - Penal Interest Receivable Account ({request.Code})";
        PenalInterestReceivableAccount.IsOfficeAccount = true;
        PenalInterestReceivableAccount.AccountType = COAType.CONTROL;
        PenalInterestReceivableAccount.AllowManualEntry = true;
        PenalInterestReceivableAccount.CurrencyId = request.DefaultCurrencyId;
        dbContext.LedgerAccounts.Add(PenalInterestReceivableAccount);
        entity.PenalInterestReceivableAccount = PenalInterestReceivableAccount;


        var InterestLossAccount = new LedgerAccount();
        InterestLossAccount.Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString();
        InterestLossAccount.Name = $"Loan Product - Interest Loss Account ({request.Code}) {DisplayTitle}";
        InterestLossAccount.IsOfficeAccount = true;
        InterestLossAccount.AccountType = COAType.CONTROL;
        InterestLossAccount.AllowManualEntry = true;
        InterestLossAccount.CurrencyId = request.DefaultCurrencyId;
        dbContext.LedgerAccounts.Add(InterestLossAccount);
        entity.InterestLossAccount = InterestLossAccount;


        var ChargesIncomeAccount = new LedgerAccount();
        ChargesIncomeAccount.Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString();
        ChargesIncomeAccount.Name = $"Loan Product - Charges Receivable Account ({request.Code})";
        ChargesIncomeAccount.IsOfficeAccount = true;
        ChargesIncomeAccount.AccountType = COAType.CONTROL;
        ChargesIncomeAccount.AllowManualEntry = true;
        ChargesIncomeAccount.CurrencyId = request.DefaultCurrencyId;
        dbContext.LedgerAccounts.Add(ChargesIncomeAccount);
        entity.ChargesIncomeAccount = ChargesIncomeAccount;


        var ChargesAccrualAccount = new LedgerAccount();
        ChargesAccrualAccount.Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString();
        ChargesAccrualAccount.Name = $"Loan Product - Charges Accrual Account ({request.Code})";
        ChargesAccrualAccount.IsOfficeAccount = true;
        ChargesAccrualAccount.AccountType = COAType.CONTROL;
        ChargesAccrualAccount.AllowManualEntry = true;
        ChargesAccrualAccount.CurrencyId = request.DefaultCurrencyId;
        dbContext.LedgerAccounts.Add(ChargesAccrualAccount);
        entity.ChargesAccrualAccount = ChargesAccrualAccount;


        var ChargesWaivedAccount = new LedgerAccount();
        ChargesWaivedAccount.Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString();
        ChargesWaivedAccount.Name = $"Loan Product - Charges Waived Account ({request.Code})";
        ChargesWaivedAccount.IsOfficeAccount = true;
        ChargesWaivedAccount.AccountType = COAType.CONTROL;
        ChargesWaivedAccount.AllowManualEntry = true;
        ChargesWaivedAccount.CurrencyId = request.DefaultCurrencyId;
        dbContext.LedgerAccounts.Add(ChargesWaivedAccount);
        entity.ChargesWaivedAccount = ChargesWaivedAccount;


        var UnearnedInterestAccount = new LedgerAccount();
        UnearnedInterestAccount.Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString();
        UnearnedInterestAccount.Name = $"Loan Product - Unearned Interest Account ({request.Code})";
        UnearnedInterestAccount.IsOfficeAccount = true;
        UnearnedInterestAccount.AccountType = COAType.CONTROL;
        UnearnedInterestAccount.AllowManualEntry = true;
        UnearnedInterestAccount.CurrencyId = request.DefaultCurrencyId;
        dbContext.LedgerAccounts.Add(UnearnedInterestAccount);
        entity.UnearnedInterestAccount = UnearnedInterestAccount;


        var InterestWaivedAccount = new LedgerAccount();
        InterestWaivedAccount.Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString();
        InterestWaivedAccount.Name = $"Loan Product - Interest Waived Account ({request.Code})";
        InterestWaivedAccount.IsOfficeAccount = true;
        InterestWaivedAccount.AccountType = COAType.CONTROL;
        InterestWaivedAccount.AllowManualEntry = true;
        InterestWaivedAccount.CurrencyId = request.DefaultCurrencyId;
        dbContext.LedgerAccounts.Add(InterestWaivedAccount);
        entity.InterestWaivedAccount = InterestWaivedAccount;

        dbContext.LoanProducts.Add(entity);

        // Manage Charges
        if (request.HasAdminCharges && request.AdminCharges.Any())
        {
            var adminCharges = new List<LoanProductCharge>();
            foreach (var adminCharge in request.AdminCharges)
                adminCharges.Add(new LoanProductCharge
                {
                    Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString(),
                    ChargeId = adminCharge,
                    ProductId = entity.Id,
                    ChargeType = ChargeType.LOAN_DISBURSEMENT // charge type to be reviewed 
                });

            await dbContext.LoanProductCharges.AddRangeAsync(adminCharges, cancellationToken);
        }

        if (request.EnableAdminOffsetCharge && request.AdminOffsetCharges is not null && request.AdminOffsetCharges.Any())
        {
            var adminOffsetCharges = new List<LoanProductCharge>();
            foreach (var adminOffsetCharge in request.AdminOffsetCharges)
                adminOffsetCharges.Add(new LoanProductCharge
                {
                    Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString(),
                    ChargeId = adminOffsetCharge,
                    ProductId = entity.Id,
                    ChargeType = ChargeType.LOAN_ADMIN_OFFSET
                });

            await dbContext.LoanProductCharges.AddRangeAsync(adminOffsetCharges, cancellationToken);
        }

        if (request.EnableChargeWaiver && request.WaiverCharges.Any())
        {
            var waivedCharges = new List<LoanProductCharge>();
            foreach (var waivedCharge in request.WaiverCharges)
                waivedCharges.Add(new LoanProductCharge
                {
                    Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString(),
                    ChargeId = waivedCharge,
                    ProductId = entity.Id,
                    ChargeType = ChargeType.LOAN_EARLY_OFFSET_WAIVER
                });

            await dbContext.LoanProductCharges.AddRangeAsync(waivedCharges, cancellationToken);
        }

        if (request.EnableTopUpCharges && request.TopUpCharges.Any())
        {
            var topUpCharges = new List<LoanProductCharge>();
            foreach (var topUpCharge in request.TopUpCharges)
                topUpCharges.Add(new LoanProductCharge
                {
                    Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString(),
                    ChargeId = topUpCharge,
                    ProductId = entity.Id,
                    ChargeType = ChargeType.LOAN_TOP_UP
                });

            await dbContext.LoanProductCharges.AddRangeAsync(topUpCharges, cancellationToken);
        }

        if (request.EnableWaitingPeriodCharge && request.WaitingPeriodCharges.Any())
        {
            var waitingPeriodCharges = new List<LoanProductCharge>();
            foreach (var waitingPeriodCharge in request.WaitingPeriodCharges)
                waitingPeriodCharges.Add(new LoanProductCharge
                {
                    Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString(),
                    ChargeId = waitingPeriodCharge,
                    ProductId = entity.Id,
                    ChargeType = ChargeType.LOAN_WAITING_PERIOD
                });
            await dbContext.LoanProductCharges.AddRangeAsync(waitingPeriodCharges, cancellationToken);
        }



        var approvalRequest = new CreateApprovalModel
        {
            Module = "LoanProduct",
            Payload = JsonConvert.SerializeObject(request),
            Comment = "Create loan product approval initiated",
            ApprovalType = ApprovalType.LOAN_PRODUCT,
            Description = $"Loan Product - {entity.Name} ({entity.Code})",
            CreatedBy = request.ApplicationUserId,
            EntityId = entity.Id,
            EntityType = typeof(CreateLoanProductCommand)
        };

        var approval = await _approval.CreateApproval(approvalRequest, true);

        if (approval.StatusCode == StatusCodes.Status201Created)
        {
            entity.ApprovalId = approval.Response.Id;
            dbContext.LoanProducts.Update(entity);
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = mapper.Map<LoanProductViewModel>(entity);
        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteLoanProductCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await dbContext.LoanProducts.FindAsync(request.Id);
        entity.IsDeleted = true;
        entity.Status = ProductStatus.DELETED;
        dbContext.LoanProducts.Update(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = "Data successfully deleted";

        return rsp;
    }

    public async Task<CommandResult<GetLoanProductViewModel>> Handle(GetLoanProductCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<GetLoanProductViewModel>();
        var loanProduct = await dbContext.LoanProductMasterView
          .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        var response = mapper.Map<GetLoanProductViewModel>(loanProduct!);
        response.CurrencyName = loanProduct.DefaultCurrencyId_Name;
        response.CurrencySymbol = loanProduct.DefaultCurrencyId_Symbol;
        response.ApprovalWorkFlowName = loanProduct.ApprovalWorkflowId_WorkflowName;
        response.SavingsOffSets = loanProduct.SavingsOffSetProductIdList is null ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(loanProduct.SavingsOffSetProductIdList);
        response.MemberTypes = loanProduct.MemberTypeIdList is null ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(loanProduct.MemberTypeIdList);

        var loanProductCharges = await dbContext.LoanProductCharges
          .Include(y => y.Charge)
          .Where(x => x.ProductId == request.Id)
          .ToListAsync(cancellationToken);

        response.DisbursementCharges = loanProductCharges.Where(x => x.ChargeType == ChargeType.LOAN_DISBURSEMENT)
          .Select(y => new ChargeLookup
          {
              Id = y.ChargeId,
              ProductId = y.ProductId,
              Name = y.Charge.Name,
              Code = y.Charge.Code,
              Amount = y.Charge.ChargeValue
          }).ToList() ?? new List<ChargeLookup>();

        response.AdminOffsetCharges = loanProductCharges.Where(x => x.ChargeType == ChargeType.LOAN_ADMIN_OFFSET)
          .Select(y => new ChargeLookup
          {
              Id = y.ChargeId,
              ProductId = y.ProductId,
              Name = y.Charge.Name,
              Code = y.Charge.Code,
              Amount = y.Charge.ChargeValue
          }).ToList() ?? new List<ChargeLookup>();

        response.WaivedCharges = loanProductCharges.Where(x => x.ChargeType == ChargeType.LOAN_EARLY_OFFSET_WAIVER)
          .Select(y => new ChargeLookup
          {
              Id = y.ChargeId,
              ProductId = y.ProductId,
              Name = y.Charge.Name,
              Code = y.Charge.Code,
              Amount = y.Charge.ChargeValue
          }).ToList() ?? new List<ChargeLookup>();

        response.TopUpCharges = loanProductCharges.Where(x => x.ChargeType == ChargeType.LOAN_TOP_UP)
          .Select(y => new ChargeLookup
          {
              Id = y.ChargeId,
              ProductId = y.ProductId,
              Name = y.Charge.Name,
              Code = y.Charge.Code,
              Amount = y.Charge.ChargeValue
          }).ToList() ?? new List<ChargeLookup>();

        response.WaitingPeriodCharges = loanProductCharges.Where(x => x.ChargeType == ChargeType.LOAN_WAITING_PERIOD)
          .Select(y => new ChargeLookup
          {
              Id = y.ChargeId,
              ProductId = y.ProductId,
              Name = y.Charge.Name,
              Code = y.Charge.Code,
              Amount = y.Charge.ChargeValue
          }).ToList() ?? new List<ChargeLookup>();

        // Get GL Accounts
        var glAccountIds = new HashSet<string>
        {
            response.DisbursementAccountId,
            response.BankDepositAccountId,
            response.ChargesAccrualAccountId,
            response.InterestLossAccountId,
            response.PrincipalLossAccountId,
            response.PenalInterestReceivableAccountId,
            response.PrincipalAccountId,
            response.InterestIncomeAccountId,
            response.UnearnedInterestAccountId,
            response.InterestWaivedAccountId,
            response.ChargesIncomeAccountId,
            response.ChargesWaivedAccountId
        };

        var ledgerAccounts = await dbContext.LedgerAccounts
          .Where(x => glAccountIds.Contains(x.Id))
          .ToListAsync(cancellationToken);

        var accounts = mapper.Map<List<LedgerAccountViewModel>>(ledgerAccounts);

        response.LedgerAccounts = accounts;
        rsp.Response = response;
        return rsp;
    }

    public Task<CommandResult<IQueryable<LoanProduct>>> Handle(QueryLoanProductCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<IQueryable<LoanProduct>>();
        rsp.Response = dbContext.LoanProducts;

        return Task.FromResult(rsp);
    }

    public async Task<CommandResult<LoanProductViewModel>> Handle(UpdateLoanProductCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<LoanProductViewModel>();
        var entity = await dbContext.LoanProducts.FindAsync(request.Id);

        mapper.Map(request, entity);
        entity.MemberTypeIdList = request.MemberTypes;
        entity.SavingsOffSetProductIdList = request.SavingsOffSets;
        entity.DateUpdated = DateTime.Now;
        entity.UpdatedByUserId = request.ApplicationUserId;

        // TODO: Optimize this
        var existingCharges = await dbContext.LoanProductCharges.Where(x => x.ProductId == request.Id)
          .ToListAsync(cancellationToken);
        dbContext.LoanProductCharges.RemoveRange(existingCharges);

        // Manage Charges
        if (request.HasAdminCharges && request.AdminCharges.Any())
        {
            var adminCharges = new List<LoanProductCharge>();
            foreach (var adminCharge in request.AdminCharges)
                adminCharges.Add(new LoanProductCharge
                {
                    Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString(),
                    ChargeId = adminCharge,
                    Product = entity,
                    ChargeType = ChargeType.LOAN_DISBURSEMENT
                });

            await dbContext.LoanProductCharges.AddRangeAsync(adminCharges, cancellationToken);
        }

        if (request.EnableAdminOffsetCharge && request.AdminOffsetCharges.Any())
        {
            var adminOffsetCharges = new List<LoanProductCharge>();
            foreach (var adminOffsetCharge in request.AdminOffsetCharges)
                adminOffsetCharges.Add(new LoanProductCharge
                {
                    Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString(),
                    ChargeId = adminOffsetCharge,
                    ProductId = entity.Id,
                    ChargeType = ChargeType.LOAN_ADMIN_OFFSET
                });

            await dbContext.LoanProductCharges.AddRangeAsync(adminOffsetCharges, cancellationToken);
        }


        if (request.EnableChargeWaiver && request.WaivedCharges.Any())
        {
            var waivedCharges = new List<LoanProductCharge>();
            foreach (var waivedCharge in request.WaivedCharges)
                waivedCharges.Add(new LoanProductCharge
                {
                    Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString(),
                    ChargeId = waivedCharge,
                    Product = entity,
                    ChargeType = ChargeType.LOAN_EARLY_OFFSET_WAIVER
                });

            await dbContext.LoanProductCharges.AddRangeAsync(waivedCharges, cancellationToken);
        }

        if (request.EnableTopUpCharges && request.TopUpCharges.Any())
        {
            var topUpCharges = new List<LoanProductCharge>();
            foreach (var topUpCharge in request.TopUpCharges)
                topUpCharges.Add(new LoanProductCharge
                {
                    Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString(),
                    ChargeId = topUpCharge,
                    Product = entity,
                    ChargeType = ChargeType.LOAN_TOP_UP
                });

            await dbContext.LoanProductCharges.AddRangeAsync(topUpCharges, cancellationToken);
        }

        if (request.EnableWaitingPeriodCharge && request.WaitingPeriodCharges.Any())
        {
            var waitingPeriodCharges = new List<LoanProductCharge>();
            foreach (var waitingPeriodCharge in request.WaitingPeriodCharges)
                waitingPeriodCharges.Add(new LoanProductCharge
                {
                    Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString(),
                    ChargeId = waitingPeriodCharge,
                    Product = entity,
                    ChargeType = ChargeType.LOAN_WAITING_PERIOD
                });

            await dbContext.LoanProductCharges.AddRangeAsync(waitingPeriodCharges, cancellationToken);
        }

        dbContext.LoanProducts.Update(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = mapper.Map<LoanProductViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<LoanProductViewModel>> Handle(UpdateLoanProductStatusCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<LoanProductViewModel>();
        var entity = await dbContext.LoanProducts.FindAsync(request.Id);
        entity.Status = request.Status;
        dbContext.LoanProducts.Update(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = mapper.Map<LoanProductViewModel>(entity);

        return rsp;
    }


    public async Task<CommandResult<List<LoanProductViewModel>>> Handle(GetUserLoanProductsCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<List<LoanProductViewModel>>();

        try
        {
            var customer =
                await dbContext.Customers.FirstOrDefaultAsync(x => x.ApplicationUserId == request.ApplicationUserId,
                    cancellationToken);

            var types = new List<string> { MemberType.REGULAR.ToString(), MemberType.RETIREE.ToString(), MemberType.EXPATRIATE.ToString() };

            var publishedLoanProducts = dbContext.LoanProducts
                .Where(x => x.LoanProductType == request.LoanType && x.Status == ProductStatus.PUBLISHED && x.PublicationType == PublicationType.ALL)
                .AsEnumerable().Where(y => y.MemberTypeIdList.All(c => types.Contains(c)))
                .ToHashSet();



            var departmentProducts = dbContext.DepartmentLoanProductPublications
                .Include(dp => dp.Product)
                .Where(x => x.DepartmentId == customer.DepartmentId && x.Product.LoanProductType == request.LoanType)
                .Select(p => p.Product).ToHashSet();

            publishedLoanProducts.UnionWith(departmentProducts);


            var customerProducts = dbContext.CustomerLoanProductPublications
                .Include(dp => dp.Product)
                .Where(x => x.CustomerId == customer.Id && x.Product.LoanProductType == request.LoanType)
                .Select(p => p.Product).ToHashSet();

            publishedLoanProducts.UnionWith(customerProducts);
            
            // Get all loan account products that are not yet closed
            var activeLoanAccountProducts = dbContext.LoanAccounts
                .Include(la => la.LoanApplication).ThenInclude(lp => lp.LoanProduct)
                .Where(x => x.CustomerId == customer.Id && x.IsClosed == false)
                .Select(y => y.LoanApplication.LoanProduct)
                .ToHashSet();
            
            publishedLoanProducts.ExceptWith(activeLoanAccountProducts);

            var response = mapper.Map<List<LoanProductViewModel>>(publishedLoanProducts);

            rsp.StatusCode = (int)HttpStatusCode.OK;
            rsp.Response = response;
        }
        catch (Exception e)
        {

        }
        return rsp;
    }

    public async Task<CommandResult<LoanProductViewModel>> Handle(PublishLoanProductCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<LoanProductViewModel>();

        var loanProduct =
          await dbContext.LoanProducts.FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

        loanProduct.PublicationType = request.PublicationType;
        loanProduct.PublishedByUserId = request.PublishedByUserId;
        loanProduct.Status = ProductStatus.PUBLISHED;


        if (request.PublicationType == PublicationType.CUSTOMER) // SPECIFIC_USER
        {
            var publications = new List<CustomerLoanProductPublication>();
            foreach (var target in request.Targets)
                publications.Add(new CustomerLoanProductPublication
                {
                    ProductId = loanProduct.Id,
                    CustomerId = target
                });

            await dbContext.CustomerLoanProductPublications.AddRangeAsync(publications, cancellationToken);
        }

        if (request.PublicationType == PublicationType.DEPARTMENT)
        {
            var publications = new List<DepartmentLoanProductPublication>();
            foreach (var target in request.Targets)
                publications.Add(new DepartmentLoanProductPublication
                {
                    ProductId = loanProduct.Id,
                    DepartmentId = target
                });

            await dbContext.DepartmentLoanProductPublications.AddRangeAsync(publications, cancellationToken);
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = mapper.Map<LoanProductViewModel>(loanProduct);
        return rsp;
    }


    public async Task<CommandResult<List<CustomerViewModel>>> Handle(GetCustomerPublicationByProductIdCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<List<CustomerViewModel>>();

        var customers = dbContext.CustomerLoanProductPublications
          .Where(x => x.ProductId == request.ProductId)
          .Select(z => z.Customer)
          .ToHashSet();

        var response = mapper.Map<List<CustomerViewModel>>(customers);

        rsp.StatusCode = (int)HttpStatusCode.OK;
        rsp.Response = response;
        return rsp;
    }

    public async Task<CommandResult<List<DepartmentViewModel>>> Handle(GetDepartmentPublicationByProductIdCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<List<DepartmentViewModel>>();

        var departments = dbContext.DepartmentLoanProductPublications
          .Where(x => x.ProductId == request.ProductId)
          .Select(z => z.Department)
          .ToHashSet();

        var response = mapper.Map<List<DepartmentViewModel>>(departments);

        rsp.StatusCode = (int)HttpStatusCode.OK;
        rsp.Response = response;
        return rsp;
    }

    public async Task<CommandResult<bool>> Handle(CustomerLoanProductEligibilityCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<bool> { Message = "You are eligible for this loan product", Response = true, StatusCode = (int)HttpStatusCode.OK };

        var loanAccount = await dbContext.LoanAccounts
            .Include(x => x.LoanApplication.LoanProduct)
            .FirstOrDefaultAsync(x => x.LoanApplication.LoanProductId == request.LoanProductId && x.CustomerId == request.CustomerId, cancellationToken);

        if (loanAccount is null)
            return rsp;

        if (!loanAccount!.LoanApplication.LoanProduct.EnableWaitingPeriod)
            return rsp;

        var repaymentDueDate = new LoanHelper().CalculateLoanRepaymentDueDate(
            loanAccount!.RepaymentCommencementDate, loanAccount!.TenureUnit, Convert.ToInt32(loanAccount!.TenureValue));

        var waitingPeriod = new LoanHelper().CalculateLoanRepaymentDueDate(
            repaymentDueDate, loanAccount.LoanApplication.LoanProduct.WaitingPeriodUnit,
            Convert.ToInt32(loanAccount.LoanApplication.LoanProduct.WaitingPeriodValue));

        if (DateTime.Now < waitingPeriod)
        {
            rsp.Response = false;
            rsp.Message = $"You have {(waitingPeriod - DateTime.Now).Days} day(s) waiting period for this loan product";
            return rsp;
        }

        return rsp;
    }
}