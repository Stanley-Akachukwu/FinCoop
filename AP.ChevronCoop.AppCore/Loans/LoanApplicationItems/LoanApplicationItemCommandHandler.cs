using AP.ChevronCoop.AppDomain.Loans.LoanApplicationItems;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Loans.LoanApplicationItems;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanApplicationItems;

public class LoanApplicationItemCommandHandler :
  IRequestHandler<QueryLoanApplicationItemCommand, CommandResult<IQueryable<LoanApplicationItem>>>,
  IRequestHandler<CreateLoanApplicationItemCommand, CommandResult<LoanApplicationItemViewModel>>,
  IRequestHandler<UpdateLoanApplicationItemCommand, CommandResult<LoanApplicationItemViewModel>>,
  IRequestHandler<DeleteLoanApplicationItemCommand, CommandResult<string>>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger logger;
  private readonly IMapper mapper;

  public LoanApplicationItemCommandHandler(ChevronCoopDbContext appDbContext,
    ILogger<LoanApplicationItemCommandHandler> _logger, IMapper _mapper)
  {
    dbContext = appDbContext;
    logger = _logger;
    mapper = _mapper;
  }


  public async Task<CommandResult<LoanApplicationItemViewModel>> Handle(CreateLoanApplicationItemCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<LoanApplicationItemViewModel>();
    var entity = mapper.Map<LoanApplicationItem>(request);

    dbContext.LoanApplicationItems.Add(entity);
    await dbContext.SaveChangesAsync();

    rsp.Response = mapper.Map<LoanApplicationItemViewModel>(entity);

    return rsp;
  }

  public async Task<CommandResult<string>> Handle(DeleteLoanApplicationItemCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<string>();
    var entity = await dbContext.LoanApplicationItems.FindAsync(request.Id);

    dbContext.LoanApplicationItems.Remove(entity);
    await dbContext.SaveChangesAsync();

    rsp.Response = "Data successfully deleted";

    return rsp;
  }


  public Task<CommandResult<IQueryable<LoanApplicationItem>>> Handle(QueryLoanApplicationItemCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<IQueryable<LoanApplicationItem>>();
    rsp.Response = dbContext.LoanApplicationItems;

    return Task.FromResult(rsp);
  }

  public async Task<CommandResult<LoanApplicationItemViewModel>> Handle(UpdateLoanApplicationItemCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<LoanApplicationItemViewModel>();
    var entity = await dbContext.LoanApplicationItems.FindAsync(request.Id);

    mapper.Map(request, entity);

    dbContext.LoanApplicationItems.Update(entity);
    await dbContext.SaveChangesAsync();

    rsp.Response = mapper.Map<LoanApplicationItemViewModel>(entity);

    return rsp;
  }
}