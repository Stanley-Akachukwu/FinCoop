using System.Net;
using AP.ChevronCoop.AppCore.Services.ApprovalServices;
using AP.ChevronCoop.AppDomain.Customers.Customers;
using AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;
using AP.ChevronCoop.AppDomain.Loans.LoanApplications;
using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using AP.ChevronCoop.AppDomain.Loans.LoanTopups;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Loans.LoanApplicationGuarantors;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AP.ChevronCoop.AppCore.Loans.LoanApplicationGuarantors;

public class LoanApplicationGuarantorCommandHandler :
  IRequestHandler<QueryLoanApplicationGuarantorCommand, CommandResult<IQueryable<LoanApplicationGuarantor>>>,
  IRequestHandler<GetLoanApplicationGuarantorsCommand, CommandResult<List<GetLoanApplicationGuarantorsViewModel>>>,
  IRequestHandler<GetLoanApplicationGuarantorCommand, CommandResult<GetLoanApplicationGuarantorViewModel>>,
  IRequestHandler<CreateLoanApplicationGuarantorCommand, CommandResult<LoanApplicationGuarantorViewModel>>,
  IRequestHandler<UpdateLoanApplicationGuarantorCommand, CommandResult<LoanApplicationGuarantorViewModel>>,
  IRequestHandler<VerifyLoanApplicationGuarantorCommand, CommandResult<VerifyLoanApplicationGuarantorViewModel>>,
  IRequestHandler<QueryLoanApplicationGuarantorApprovalCommand,
    CommandResult<List<LoanApplicationGuarantorApprovalViewModel>>>,
  IRequestHandler<LoanApplicationGuarantorApprovalCommand, CommandResult<LoanApplicationGuarantorApprovalViewModel>>,
  IRequestHandler<LoanTopupGuarantorApprovalCommand, CommandResult<LoanTopupGuarantorApprovalViewModel>>,
  IRequestHandler<DeleteLoanApplicationGuarantorCommand, CommandResult<string>>
{
    private readonly IManageApprovalService _approval;
    private readonly ChevronCoopDbContext _dbContext;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public LoanApplicationGuarantorCommandHandler(
      ChevronCoopDbContext appDbContext,
      ILogger<LoanApplicationGuarantorCommandHandler> logger,
      IMapper mapper,
      IManageApprovalService approval)
    {
        _dbContext = appDbContext;
        _logger = logger;
        _mapper = mapper;
        _approval = approval;
    }

    public async Task<CommandResult<LoanApplicationGuarantorViewModel>> Handle(
      CreateLoanApplicationGuarantorCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<LoanApplicationGuarantorViewModel>();
        var entity = _mapper.Map<LoanApplicationGuarantor>(request);

        _dbContext.LoanApplicationGuarantors.Add(entity);
        await _dbContext.SaveChangesAsync();

        rsp.Response = _mapper.Map<LoanApplicationGuarantorViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteLoanApplicationGuarantorCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await _dbContext.LoanApplicationGuarantors.FindAsync(request.Id);

        _dbContext.LoanApplicationGuarantors.Remove(entity!);
        await _dbContext.SaveChangesAsync();

        rsp.Response = "Data successfully deleted";

        return rsp;
    }

    public async Task<CommandResult<GetLoanApplicationGuarantorViewModel>> Handle(
      GetLoanApplicationGuarantorCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<GetLoanApplicationGuarantorViewModel>();

        var loanApplicationGuarantor = await _dbContext.LoanApplicationGuarantors
          .Include(i => i.Guarantor)
          .Include(c => c.LoanApplication)
          .Include(b => b.LoanApplication.Customer)
          .Include(b => b.LoanApplication.LoanProduct)
          .FirstOrDefaultAsync(x => x.Id == request.LoanApplicationGuarantorId, cancellationToken);

        if (loanApplicationGuarantor is null)
        {
            rsp.ErrorFlag = true;
            rsp.StatusCode = (int)HttpStatusCode.BadRequest;
            rsp.Message = "Guarantor with the provided Membership Id not found.";
            return rsp;
        }

        var response = new GetLoanApplicationGuarantorViewModel
        {
            Product = _mapper.Map<LoanProductViewModel>(loanApplicationGuarantor!.LoanApplication.LoanProduct),
            Status = loanApplicationGuarantor.Status,
            ApprovedOn = loanApplicationGuarantor.ApprovedOn,
            Comment = loanApplicationGuarantor.Comment,

            Guarantor = _mapper.Map<CustomerViewModel>(loanApplicationGuarantor.Guarantor),
            LoanApplication = _mapper.Map<LoanApplicationViewModel>(loanApplicationGuarantor.LoanApplication),
            Applicant = _mapper.Map<CustomerViewModel>(loanApplicationGuarantor.LoanApplication.Customer)
        };

        rsp.StatusCode = (int)HttpStatusCode.OK;
        rsp.Response = response;
        return rsp;
    }

    public async Task<CommandResult<List<GetLoanApplicationGuarantorsViewModel>>> Handle(
      GetLoanApplicationGuarantorsCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<List<GetLoanApplicationGuarantorsViewModel>>();

        var loanApplicationGuarantor = await _dbContext.LoanApplicationGuarantors
          .Include(i => i.Guarantor)
          .Where(x => x.LoanApplicationId == request.LoanApplicationId)
          .ToListAsync(cancellationToken);

        var response = _mapper.Map<List<GetLoanApplicationGuarantorsViewModel>>(loanApplicationGuarantor);

        rsp.StatusCode = (int)HttpStatusCode.OK;
        rsp.Response = response;
        return rsp;
    }

    public async Task<CommandResult<LoanApplicationGuarantorApprovalViewModel>> Handle(
      LoanApplicationGuarantorApprovalCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<LoanApplicationGuarantorApprovalViewModel>();
        var entity = await _dbContext.LoanApplicationGuarantors
          .FirstOrDefaultAsync(
            x => x.GuarantorId == request.GuarantorId
                 && x.LoanApplicationId == request.LoanApplicationId &&
                 x.GuarantorApprovalType == request.GuarantorApprovalType, cancellationToken);

        entity!.Status = request.IsApproved ? ApprovalStatus.APPROVED : ApprovalStatus.REJECTED;
        entity.Comment = request.Comment;

        _dbContext.LoanApplicationGuarantors.Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var guarantorApprovalStatus = _dbContext.LoanApplicationGuarantors.Any(x =>
          x.LoanApplicationId == entity.LoanApplicationId
          && x.GuarantorApprovalType == GuarantorApprovalType.LOAN_APPLICATION && x.Status != ApprovalStatus.APPROVED);

        if (!guarantorApprovalStatus)
        {
            var loanApplication =
              await _dbContext.LoanApplications
                .Include(lp => lp.LoanProduct)
                .FirstOrDefaultAsync(x => x.Id == entity.LoanApplicationId, cancellationToken);

            var customer =
              await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == loanApplication!.CustomerId, cancellationToken);

            if (request.GuarantorApprovalType == GuarantorApprovalType.LOAN_TOPUP)
            {
                var loanTopup = await _dbContext.LoanTopups
                  .Include(x => x.LoanAccount)
                  .FirstOrDefaultAsync(
                    x => x.LoanAccount.LoanApplicationId == request.LoanApplicationId && !x.LoanAccount.IsClosed &&
                         x.LoanAccount.IsActive, cancellationToken);

                var approvalRequest = new CreateApprovalModel
                {
                    Module = "LoanTopupApplication",
                    Payload = JsonConvert.SerializeObject(request),
                    Comment = "Create loan topup approval initiated",
                    ApprovalType = ApprovalType.LOAN_TOPUP_APPLICATION,
                    Description =
                    $"Loan Topup - {customer?.FirstName} {customer?.MiddleName} {customer?.LastName} ({loanApplication!.ApplicationNo})",
                    CreatedBy = loanApplication.CustomerId,
                    EntityId = loanTopup!.Id,
                    EntityType = typeof(CreateLoanTopupCommand)
                };

                var approval =
                  await _approval.CreateApproval(approvalRequest, false, loanApplication.LoanProduct?.ApprovalWorkflowId);

                if (approval.StatusCode == StatusCodes.Status201Created)
                {
                    loanTopup.ApprovalId = approval.Response.Id;
                    loanTopup.DateUpdated = DateTime.UtcNow.ToLocalTime();
                    _dbContext.LoanTopups.Update(loanTopup);
                }
            }
            else
            {
                var approvalRequest = new CreateApprovalModel
                {
                    Module = "LoanProductApplication",
                    Payload = JsonConvert.SerializeObject(request),
                    Comment = "Create loan product approval initiated",
                    ApprovalType = ApprovalType.LOAN_PRODUCT_APPLICATION,
                    Description =
                    $"Loan Application - {customer?.FirstName} {customer?.MiddleName} {customer?.LastName} ({loanApplication!.ApplicationNo})",
                    CreatedBy = loanApplication.CustomerId,
                    EntityId = loanApplication.Id,
                    EntityType = typeof(CreateLoanApplicationCommand)
                };

                var approval =
                  await _approval.CreateApproval(approvalRequest, false, loanApplication.LoanProduct?.ApprovalWorkflowId);

                if (approval.StatusCode == StatusCodes.Status201Created)
                {
                    loanApplication.ApprovalId = approval.Response.Id;
                    loanApplication.Status = LoanApplicationStatus.PENDING;
                    _dbContext.LoanApplications.Update(loanApplication);
                }
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = _mapper.Map<LoanApplicationGuarantorApprovalViewModel>(entity);
        rsp.StatusCode = (int)HttpStatusCode.OK;
        return rsp;
    }

    public async Task<CommandResult<LoanTopupGuarantorApprovalViewModel>> Handle(
      LoanTopupGuarantorApprovalCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<LoanTopupGuarantorApprovalViewModel>();

        var loanAccount = await _dbContext.LoanAccounts
          .Include(x => x.LoanApplication.Customer)
          .FirstOrDefaultAsync(x => x.Id == request.LoanAccountId, cancellationToken);

        var entity = await _dbContext.LoanApplicationGuarantors
          .Include(x => x.LoanApplication.LoanProduct)
          .FirstOrDefaultAsync(
            x => x.GuarantorId == request.GuarantorId
                 && x.LoanApplicationId == loanAccount!.LoanApplicationId,
            cancellationToken);

        entity!.Status = request.IsApproved ? ApprovalStatus.APPROVED : ApprovalStatus.REJECTED;
        entity.Comment = request.Comment;

        _dbContext.LoanApplicationGuarantors.Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var guarantorApprovalStatus = _dbContext.LoanApplicationGuarantors.Any(x =>
          x.LoanApplicationId == entity.LoanApplicationId &&
          x.GuarantorApprovalType == GuarantorApprovalType.LOAN_TOPUP && x.Status != ApprovalStatus.APPROVED);

        var topup = await _dbContext.LoanTopups.FirstOrDefaultAsync(x => x.LoanAccountId == request.LoanAccountId);

        if (!guarantorApprovalStatus)
        {
            var approvalRequest = new CreateApprovalModel
            {
                Module = "LoanTopupApplication",
                Payload = JsonConvert.SerializeObject(request),
                Comment = "Create loan topup approval initiated",
                ApprovalType = ApprovalType.LOAN_TOPUP_APPLICATION,
                Description =
                $"Loan Topup Application - {loanAccount?.LoanApplication?.Customer?.FirstName} {loanAccount?.LoanApplication?.Customer?.MiddleName} {loanAccount?.LoanApplication?.Customer?.LastName} ({entity!.LoanApplication?.ApplicationNo})",
                CreatedBy = loanAccount?.CreatedByUserId,
                EntityId = topup?.Id,
                EntityType = typeof(CreateLoanTopupCommand)
            };

            var approval = await _approval.CreateApproval(approvalRequest, false,
              entity!.LoanApplication!.LoanProduct!.ApprovalWorkflowId);

            if (approval.StatusCode == StatusCodes.Status201Created)
            {
                var loanTopup = await _dbContext.LoanTopups.FirstOrDefaultAsync(x => x.LoanAccountId == loanAccount!.Id);
                loanTopup!.ApprovalId = approval.Response.Id;
                loanTopup.DateUpdated = DateTime.UtcNow.ToLocalTime();
                _dbContext.LoanTopups.Update(loanTopup);
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = _mapper.Map<LoanTopupGuarantorApprovalViewModel>(entity);
        rsp.StatusCode = (int)HttpStatusCode.OK;
        return rsp;
    }

    public async Task<CommandResult<List<LoanApplicationGuarantorApprovalViewModel>>> Handle(
      QueryLoanApplicationGuarantorApprovalCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<List<LoanApplicationGuarantorApprovalViewModel>>();
        var response = await _dbContext.LoanApplicationGuarantors
          .Include(y => y.LoanApplication)
          .Where(x => x.GuarantorId == request.GuarantorId)
          .Select(x => new LoanApplicationGuarantorApprovalViewModel
          {
              Id = x.Id,
              LoanApplicationId = x.LoanApplicationId,
              GuarantorType = x.GuarantorType.ToString(),
              Amount = x.LoanApplication.Principal,
              Comments = x.Comment,
              GuarantorId = x.GuarantorId
          })
          .ToListAsync(cancellationToken);

        rsp.Response = response;

        return rsp;
    }

    public Task<CommandResult<IQueryable<LoanApplicationGuarantor>>> Handle(QueryLoanApplicationGuarantorCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<IQueryable<LoanApplicationGuarantor>>();
        rsp.Response = _dbContext.LoanApplicationGuarantors;

        return Task.FromResult(rsp);
    }

    public async Task<CommandResult<LoanApplicationGuarantorViewModel>> Handle(
      UpdateLoanApplicationGuarantorCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<LoanApplicationGuarantorViewModel>();
        var entity = await _dbContext.LoanApplicationGuarantors.FindAsync(request.Id);

        _mapper.Map(request, entity);

        _dbContext.LoanApplicationGuarantors.Update(entity!);
        await _dbContext.SaveChangesAsync();

        rsp.Response = _mapper.Map<LoanApplicationGuarantorViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<VerifyLoanApplicationGuarantorViewModel>> Handle(
      VerifyLoanApplicationGuarantorCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<VerifyLoanApplicationGuarantorViewModel>();
        try
        {
            var entity =
              await _dbContext.Customers.FirstOrDefaultAsync(x => x.MemberId == request.MembershipId,
                cancellationToken);

            var response = _mapper.Map<VerifyLoanApplicationGuarantorViewModel>(entity);

            // Get user role claims
            var corporateRoles = new HashSet<string>(new[] { "regular", "retiree" });
            var userRole = await _dbContext.ApplicationUserRoles
              .Include(i => i.Role)
              .FirstOrDefaultAsync(x => entity != null && x.UserId == entity.ApplicationUserId && x.Role != null &&
                                        corporateRoles.Contains(x.Role.Code.ToLower()), cancellationToken);

            if (userRole is null)
            {
                rsp.ErrorFlag = true;
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = "Provided user is neither a regular or retiree user";
                return rsp;
            }

            var guarantorApplications = await _dbContext.LoanApplicationGuarantors
              .Include(a => a.LoanApplication)
              .Where(z => z.GuarantorId == entity.Id && z.Status == ApprovalStatus.APPROVED)
              .SumAsync(x => x.LoanApplication.Principal, cancellationToken);

            response.GuarantorType = userRole.Role.Name;
            response.GuarantorCustomerId = entity.Id;
            response.GuarantorMembershipId = entity.MemberId;
            response.YearsOfService = DateTime.Now.Year - entity.DateOfEmployment?.Year ?? 0;
            response.TotalRunningLoan = guarantorApplications;
            response.IdentificationUrl = "";
            response.PassportUrl = "";
            response.ProfileImageUrl = "";

            rsp.Response = response;
            rsp.StatusCode = (int)HttpStatusCode.OK;
        }
        catch (Exception e)
        {
            rsp.ErrorFlag = true;
            rsp.Message = "Guarantor not found";
            rsp.StatusCode = (int)HttpStatusCode.BadRequest;
        }

        return rsp;
    }
}