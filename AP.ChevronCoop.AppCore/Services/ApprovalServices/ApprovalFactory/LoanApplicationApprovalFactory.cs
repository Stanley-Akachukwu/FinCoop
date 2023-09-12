using AP.ChevronCoop.AppDomain.Loans.LoanAccounts;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalNotifications;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AP.ChevronCoop.AppCore.Services.ApprovalServices.ApprovalFactory;

public class LoanApplicationApprovalFactory : IApprovalFactory
{
    private readonly ChevronCoopDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public LoanApplicationApprovalFactory(ChevronCoopDbContext dbContext, IMapper mapper, IMediator mediator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<CommandResult<bool>> Initiate(Approval request)
    {
        var response = new CommandResult<bool>();

        response.Response = await _mediator.Send(new SendApprovalRequestNotificationCommand()
        {
            ApprovalId = request.Id
        });

        return response;
    }

    public async Task<CommandResult<bool>> Process(Approval approval, string? approvedById, string? comment,
      ApprovalStatus status)
    {
        var response = new CommandResult<bool>();
        var application = await _dbContext.LoanApplications.FirstOrDefaultAsync(x => x.Id == approval.EntityId);
        
        if (status != ApprovalStatus.APPROVED)
        {
            application.Status = LoanApplicationStatus.REJECTED;
            _dbContext.LoanApplications.Update(application);
            _dbContext.SaveChanges();
            
            _ = await _mediator.Send(new SendApprovalNotificationCommand
            {
                ApprovalId = approval.Id,
                IsApproved = false
            });

            response.Response = false;
            return response;
        }


        if (application != null && application.Status != LoanApplicationStatus.APPROVED)
        {
            await _mediator.Send(new CreateLoanAccountCommand
            {
                LoanApplicationId = application.Id
            });
            
            _ = await _mediator.Send(new SendApprovalNotificationCommand
            {
                ApprovalId = approval.Id,
                IsApproved = true
            });
        }

        response.Response = true;

        return response;
    }
}