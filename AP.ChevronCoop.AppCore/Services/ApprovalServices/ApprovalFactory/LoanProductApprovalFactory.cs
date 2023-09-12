using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalNotifications;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Approvals;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AP.ChevronCoop.AppCore.Services.ApprovalServices.ApprovalFactory;

public class LoanProductApprovalFactory : IApprovalFactory
{
    private readonly ChevronCoopDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public LoanProductApprovalFactory(ChevronCoopDbContext dbContext, IMapper mapper, IMediator mediator)
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

    public async Task<CommandResult<bool>> Process(Approval request, string? approvedById, string? comment, ApprovalStatus status)
    {
        var response = new CommandResult<bool>();
        var entity = await _dbContext.LoanProducts.FirstOrDefaultAsync(x => x.Id == request.EntityId);

        if (status != ApprovalStatus.APPROVED)
        {
            entity.Status = ProductStatus.REJECTED;
            _dbContext.LoanProducts.Update(entity);
            _dbContext.SaveChanges();
            
            response.Response = false;
            return response;
        }
        
        try
        {
            if (entity == null)
            {
                response.Response = false;
                response.Message = "Product not found";
                return response;
            }

            entity.Status = ProductStatus.APPROVED;
            _dbContext.LoanProducts.Update(entity);

            response.Response = true;
        }
        catch (Exception e)
        {
            response.Response = false;
            response.Message = e.Message;
        }

        return response;
    }
}