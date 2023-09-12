using AP.ChevronCoop.AppDomain.Loans.LoanApplicationApprovals;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Loans.LoanApplicationApprovals;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanApplicationApprovals;

public class LoanApplicationApprovalCommandHandler :
  IRequestHandler<QueryLoanApplicationApprovalCommand, CommandResult<IQueryable<LoanApplicationApproval>>>,
  IRequestHandler<CreateLoanApplicationApprovalCommand, CommandResult<LoanApplicationApprovalViewModel>>,
  IRequestHandler<UpdateLoanApplicationApprovalCommand, CommandResult<LoanApplicationApprovalViewModel>>,
  IRequestHandler<DeleteLoanApplicationApprovalCommand, CommandResult<string>>
{
    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger logger;
    private readonly IMapper mapper;

    public LoanApplicationApprovalCommandHandler(ChevronCoopDbContext appDbContext,
      ILogger<LoanApplicationApprovalCommandHandler> _logger, IMapper _mapper)
    {
        dbContext = appDbContext;
        logger = _logger;
        mapper = _mapper;
    }

    public async Task<CommandResult<LoanApplicationApprovalViewModel>> Handle(
      CreateLoanApplicationApprovalCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<LoanApplicationApprovalViewModel>();
        var entity = mapper.Map<LoanApplicationApproval>(request);

        dbContext.LoanApplicationApprovals.Add(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = mapper.Map<LoanApplicationApprovalViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteLoanApplicationApprovalCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await dbContext.LoanApplicationApprovals.FindAsync(request.Id);

        dbContext.LoanApplicationApprovals.Remove(entity!);
        await dbContext.SaveChangesAsync();

        rsp.Response = "Data successfully deleted";

        return rsp;
    }

    public Task<CommandResult<IQueryable<LoanApplicationApproval>>> Handle(QueryLoanApplicationApprovalCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<IQueryable<LoanApplicationApproval>>();
        rsp.Response = dbContext.LoanApplicationApprovals;

        return Task.FromResult(rsp);
    }

    public async Task<CommandResult<LoanApplicationApprovalViewModel>> Handle(
      UpdateLoanApplicationApprovalCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<LoanApplicationApprovalViewModel>();
        var entity = await dbContext.LoanApplicationApprovals.FindAsync(request.Id);

        mapper.Map(request, entity);

        dbContext.LoanApplicationApprovals.Update(entity!);
        await dbContext.SaveChangesAsync();

        rsp.Response = mapper.Map<LoanApplicationApprovalViewModel>(entity);

        return rsp;
    }
}