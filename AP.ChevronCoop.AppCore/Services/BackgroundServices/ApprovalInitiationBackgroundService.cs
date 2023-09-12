using AP.ChevronCoop.AppCore.Services.ApprovalServices.ApprovalFactory;
using AP.ChevronCoop.AppCore.Services.BackgroundServices.Interfaces;
using AP.ChevronCoop.AppCore.Services.LogServices;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Approvals;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Services.BackgroundServices;
public class ApprovalInitiationBackgroundService : IApprovalInitiationBackgroundService
{
    private readonly ChevronCoopDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILoggerService _loggerService;
    private readonly ILoggerFactory _loggerFactory;
    private readonly IMediator _mediator;
    private CancellationTokenSource cts = new CancellationTokenSource();

    public ApprovalInitiationBackgroundService(ChevronCoopDbContext dbContext, IMapper mapper,
        ILoggerService loggerService, ILoggerFactory logger, IMediator mediator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _mediator = mediator;
        _loggerService = loggerService;
        _loggerFactory = logger;
    }

    public async Task InitiateApprovals()
    {
        var rsp = new CommandResult<Approval>();
        try
        {
            var createdApprovals = await _dbContext.Approvals.Where(x => x.Status == ApprovalStatus.CREATED && x.TriedCount < 10)
                .ToListAsync(cts.Token);

            if (createdApprovals.Any())
            {
                foreach (var approval in createdApprovals)
                {
                    var provider = new BaseApprovalFactory(_dbContext, _loggerService, _loggerFactory, _mapper, _mediator).GetProvider(approval.ApprovalType);
                    var approvalInitiation = await provider.Initiate(approval);

                    if (approvalInitiation.Response)
                    {
                        approval.Status = ApprovalStatus.INITIATED;
                    }
                    else
                    {
                        approval.Comment = approvalInitiation.Message;
                    }

                    approval.TriedCount = ++approval.TriedCount;

                    _dbContext.Approvals.Update(approval);
                    await _dbContext.SaveChangesAsync(cts.Token);


                }
            }
        }
        catch (Exception ex)
        {
            // await _logger.LogError(nameof(ApprovalInitiationBackgroundService), nameof(InitiateApprovals), ex);
            //_loggerFactory.CreateLogger< ApprovalInitiationBackgroundService>().LogError(nameof(ApprovalInitiationBackgroundService), nameof(InitiateApprovals), ex);
            _loggerFactory.CreateLogger<ApprovalInitiationBackgroundService>().LogError(nameof(ApprovalInitiationBackgroundService), nameof(InitiateApprovals), ex);
        }
    }
}