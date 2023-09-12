using AP.ChevronCoop.AppCore.Services.AuditServices;
using AP.ChevronCoop.AppDomain.Security.AuditTrails;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.AuditTrails;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace AP.ChevronCoop.AppCore.Security.AuditTrails;

public class AuditTrailCommandHandler :
IRequestHandler<QueryAuditTrailCommand, CommandResult<IQueryable<AuditTrail>>>,
IRequestHandler<CreateAuditTrailCommand, CommandResult<AuditTrailViewModel>>
{
    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger logger;
    private readonly IMapper mapper;
    private readonly IAuditService _auditService;

    public AuditTrailCommandHandler(ChevronCoopDbContext appDbContext, ILogger<AuditTrailCommandHandler> _logger, IMapper _mapper, IAuditService auditService)
    {
        dbContext = appDbContext;
        logger = _logger;
        mapper = _mapper;
        _auditService = auditService;
    }

    public async Task<CommandResult<IQueryable<AuditTrail>>> Handle(QueryAuditTrailCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<IQueryable<AuditTrail>>();
        rsp.Response = dbContext.AuditTrails;

        return rsp;
    }

    public async Task<CommandResult<AuditTrailViewModel>> Handle(CreateAuditTrailCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<AuditTrailViewModel>();
        await _auditService.LogAudit(request.Action, request.Description, request.Module, request.CreatedByUserId, request.Payload, request.UserName);
        rsp.StatusCode = (int)HttpStatusCode.Created;
        rsp.Message = "Audit logged successfully!";
        return rsp;
    }
}