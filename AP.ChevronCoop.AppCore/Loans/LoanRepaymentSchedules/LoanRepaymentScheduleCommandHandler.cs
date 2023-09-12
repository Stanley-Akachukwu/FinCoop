using AP.ChevronCoop.AppDomain.Loans.LoanRepaymentSchedules;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanRepaymentSchedules;

public class LoanRepaymentScheduleCommandHandler :
  // IRequestHandler<QueryLoanRepaymentScheduleCommand, CommandResult<IQueryable<LoanRepaymentScheduleItem>>>,
  IRequestHandler<CreateLoanRepaymentScheduleCommand, CommandResult<List<LoanRepaymentScheduleViewModel>>>,
  IRequestHandler<UpdateLoanRepaymentScheduleCommand, CommandResult<LoanRepaymentScheduleViewModel>>,
  IRequestHandler<DeleteLoanRepaymentScheduleCommand, CommandResult<string>>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger logger;
  private readonly IMapper mapper;

  public LoanRepaymentScheduleCommandHandler(ChevronCoopDbContext appDbContext,
    ILogger<LoanRepaymentScheduleCommandHandler> _logger, IMapper _mapper)
  {
    dbContext = appDbContext;
    logger = _logger;
    mapper = _mapper;
  }


  public async Task<CommandResult<List<LoanRepaymentScheduleViewModel>>> Handle(
    CreateLoanRepaymentScheduleCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<List<LoanRepaymentScheduleViewModel>>();
    // var entity = mapper.Map<LoanRepaymentSchedule>(request);

    // dbContext.LoanRepaymentSchedules.Add(entity);
    // await dbContext.SaveChangesAsync();

    // rsp.Response = mapper.Map<List<LoanRepaymentScheduleViewModel>>(entity);

    return rsp;
  }

  public async Task<CommandResult<string>> Handle(DeleteLoanRepaymentScheduleCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<string>();
    var entity = await dbContext.LoanRepaymentSchedules.FindAsync(request.Id);

    dbContext.LoanRepaymentSchedules.Remove(entity);
    await dbContext.SaveChangesAsync();

    rsp.Response = "Data successfully deleted";

    return rsp;
  }


  // public Task<CommandResult<IQueryable<LoanRepaymentScheduleItem>>> Handle(QueryLoanRepaymentScheduleCommand request,
  //   CancellationToken cancellationToken)
  // {
  //     var rsp = new CommandResult<IQueryable<LoanRepaymentScheduleItem>>();
  //     rsp.Response = dbContext.LoanRepaymentSchedules;
  //
  //     return Task.FromResult(rsp);
  // }

  public async Task<CommandResult<LoanRepaymentScheduleViewModel>> Handle(UpdateLoanRepaymentScheduleCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<LoanRepaymentScheduleViewModel>();
    var entity = await dbContext.LoanRepaymentSchedules.FindAsync(request.Id);

    mapper.Map(request, entity);

    dbContext.LoanRepaymentSchedules.Update(entity);
    await dbContext.SaveChangesAsync();

    rsp.Response = mapper.Map<LoanRepaymentScheduleViewModel>(entity);

    return rsp;
  }
}