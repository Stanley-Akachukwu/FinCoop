using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalLogs;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalLogs;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalLogs;

public class ApprovalLogCommandHandler :
  IRequestHandler<QueryApprovalLogCommand, CommandResult<IQueryable<ApprovalLog>>>,
  IRequestHandler<CreateApprovalLogCommand, CommandResult<ApprovalLogViewModel>>,
  IRequestHandler<UpdateApprovalLogCommand, CommandResult<ApprovalLogViewModel>>,
  IRequestHandler<DeleteApprovalLogCommand, CommandResult<string>>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger logger;
  private readonly IMapper mapper;

  public ApprovalLogCommandHandler(ChevronCoopDbContext appDbContext,
    ILogger<ApprovalLogCommandHandler> _logger, IMapper _mapper)
  {
    dbContext = appDbContext;
    logger = _logger;
    mapper = _mapper;
  }


  public Task<CommandResult<IQueryable<ApprovalLog>>> Handle(QueryApprovalLogCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<IQueryable<ApprovalLog>>();
    rsp.Response = dbContext.ApprovalLogs;

    return Task.FromResult(rsp);
  }


  public async Task<CommandResult<ApprovalLogViewModel>> Handle(CreateApprovalLogCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<ApprovalLogViewModel>();
    var entity = mapper.Map<ApprovalLog>(request);

    dbContext.ApprovalLogs.Add(entity);
    await dbContext.SaveChangesAsync();

    rsp.Response = mapper.Map<ApprovalLogViewModel>(entity);

    return rsp;
  }

  public async Task<CommandResult<ApprovalLogViewModel>> Handle(UpdateApprovalLogCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<ApprovalLogViewModel>();
    var entity = await dbContext.ApprovalLogs.FindAsync(request.Id);

    mapper.Map(request, entity);

    dbContext.ApprovalLogs.Update(entity);
    await dbContext.SaveChangesAsync();

    rsp.Response = mapper.Map<ApprovalLogViewModel>(entity);

    return rsp;
  }

  public async Task<CommandResult<string>> Handle(DeleteApprovalLogCommand request, CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<string>();
    var entity = await dbContext.ApprovalLogs.FindAsync(request.Id);

    dbContext.ApprovalLogs.Remove(entity);
    await dbContext.SaveChangesAsync();

    rsp.Response = "Data successfully deleted";

    return rsp;
  }
}