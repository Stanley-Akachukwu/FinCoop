using AP.ChevronCoop.AppDomain.Loans.LoanRepayments;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Loans.LoanRepayment;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanRepayments;

public class LoanRepaymentCommandHandler :
  IRequestHandler<QueryLoanRepaymentCommand, CommandResult<IQueryable<LoanRepayment>>>,
  IRequestHandler<CreateLoanRepaymentCommand, CommandResult<LoanRepaymentViewModel>>,
  IRequestHandler<UpdateLoanRepaymentCommand, CommandResult<LoanRepaymentViewModel>>,
  IRequestHandler<DeleteLoanRepaymentCommand, CommandResult<string>>
{
    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger logger;
    private readonly IMapper mapper;

    public LoanRepaymentCommandHandler(ChevronCoopDbContext appDbContext,
      ILogger<LoanRepaymentCommandHandler> _logger, IMapper _mapper)
    {
        dbContext = appDbContext;
        logger = _logger;
        mapper = _mapper;
    }

    public async Task<CommandResult<LoanRepaymentViewModel>> Handle(CreateLoanRepaymentCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<LoanRepaymentViewModel>();
        var entity = mapper.Map<LoanRepayment>(request);

        dbContext.LoanRepayments.Add(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = mapper.Map<LoanRepaymentViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteLoanRepaymentCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await dbContext.LoanRepayments.FindAsync(request.Id);

        dbContext.LoanRepayments.Remove(entity!);
        await dbContext.SaveChangesAsync();

        rsp.Response = "Data successfully deleted";

        return rsp;
    }

    public Task<CommandResult<IQueryable<LoanRepayment>>> Handle(QueryLoanRepaymentCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<IQueryable<LoanRepayment>>();
        rsp.Response = dbContext.LoanRepayments;

        return Task.FromResult(rsp);
    }

    public async Task<CommandResult<LoanRepaymentViewModel>> Handle(UpdateLoanRepaymentCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<LoanRepaymentViewModel>();
        var entity = await dbContext.LoanRepayments.FindAsync(request.Id);

        mapper.Map(request, entity);

        dbContext.LoanRepayments.Update(entity!);
        await dbContext.SaveChangesAsync();

        rsp.Response = mapper.Map<LoanRepaymentViewModel>(entity);

        return rsp;
    }
}