using AP.ChevronCoop.AppDomain.Loans.LoanApplicationSchedules;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Loans.LoanApplicationSchedules;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanApplicationSchedules;

public class LoanApplicationScheduleCommandHandler :
  IRequestHandler<QueryLoanApplicationScheduleCommand, CommandResult<IQueryable<LoanApplicationSchedule>>>,
  IRequestHandler<CreateLoanApplicationScheduleCommand, CommandResult<List<LoanApplicationScheduleViewModel>>>
{
    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger logger;
    private readonly IMapper mapper;

    public LoanApplicationScheduleCommandHandler(ChevronCoopDbContext appDbContext,
      ILogger<LoanApplicationScheduleCommandHandler> _logger, IMapper _mapper)
    {
        dbContext = appDbContext;
        logger = _logger;
        mapper = _mapper;
    }


    public async Task<CommandResult<List<LoanApplicationScheduleViewModel>>> Handle(
      CreateLoanApplicationScheduleCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<List<LoanApplicationScheduleViewModel>>();

        var monthlyInterest = request.Interest / 12;
        var monthlyPayment = request.Amount / request.TenureValue;

        var response = new List<LoanApplicationScheduleViewModel>();

        for (var month = 1; month <= request.TenureValue; month++)
        {
            var interestAmount = request.Amount * monthlyInterest;
            var totalPayment = monthlyPayment + interestAmount;
            request.Amount -= monthlyPayment;

            response.Add(new LoanApplicationScheduleViewModel
            {
                PeriodInterest = interestAmount,
                TotalBalance = request.Amount,
                PeriodPrincipal = monthlyPayment,
                DueDate = request.CommencementDate
            });

            request.CommencementDate = request.CommencementDate.AddMonths(1);
        }

        rsp.Response = response;
        return rsp;
    }

    public Task<CommandResult<IQueryable<LoanApplicationSchedule>>> Handle(QueryLoanApplicationScheduleCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<IQueryable<LoanApplicationSchedule>>();
        // rsp.Response = dbContext.LoanApplicationSchedules;

        return Task.FromResult(rsp);
    }
}