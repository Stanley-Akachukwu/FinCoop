using AP.ChevronCoop.AppDomain.Loans.LoanAccounts;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalNotifications;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AP.ChevronCoop.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AP.ChevronCoop.AppDomain.Loans.LoanTopups;
using AP.ChevronCoop.AppCore.Loans.LoanTopups;
using Microsoft.Extensions.Logging;
using AP.ChevronCoop.AppCore.Services.LogServices;

namespace AP.ChevronCoop.AppCore.Services.ApprovalServices.ApprovalFactory
{
    public class LoanTopupApprovalFactory : IApprovalFactory
    {
        private readonly ChevronCoopDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        ILogger<LoanTopupApprovalFactory> logger;
        //private readonly ILoggerService loggerService;
        ILoggerFactory loggerFactory;
        public LoanTopupApprovalFactory(ChevronCoopDbContext dbContext,
            ILoggerService _loggerService, ILoggerFactory _logger,
        IMapper mapper, IMediator mediator)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _mediator = mediator;
            loggerFactory = _logger;
            logger = loggerFactory.CreateLogger<LoanTopupApprovalFactory>();
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
            var loanTopup = await _dbContext.LoanTopups.FirstOrDefaultAsync(x => x.Id == approval.EntityId);

            if (status != ApprovalStatus.APPROVED)
            {
                logger.LogInformation("loan topup approval NOT successful");
                loanTopup.Status = TransactionStatus.REJECTED;
                _dbContext.LoanTopups.Update(loanTopup);
                _dbContext.SaveChanges();

                _ = await _mediator.Send(new SendApprovalNotificationCommand
                {
                    ApprovalId = approval.Id,
                    IsApproved = false
                });

                response.Response = false;
                return response;
            }


            if (loanTopup != null)
            {
                if (status == ApprovalStatus.APPROVED)
                {
                    logger.LogInformation("loan topup approval successful");
                    logger.LogInformation("loan topup ->start trigger ProcessLoanTopupCommand ");


                    await _mediator.Send(new ProcessLoanTopupCommand
                    {
                        LoanTopupId = loanTopup.Id,
                        Status = status
                    });

                    logger.LogInformation("loan topup ->end trigger ProcessLoanTopupCommand ");

                    _ = await _mediator.Send(new SendApprovalNotificationCommand
                    {
                        ApprovalId = approval.Id,
                        IsApproved = true
                    });
                }
            }

            response.Response = true;

            return response;
        }
    }
}
