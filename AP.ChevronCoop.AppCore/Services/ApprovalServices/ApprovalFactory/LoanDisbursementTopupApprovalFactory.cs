using AP.ChevronCoop.AppDomain.Loans.LoanTransactions;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalNotifications;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Loans.LoanDisbursements;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AP.ChevronCoop.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AP.ChevronCoop.AppCore.Services.ApprovalServices.ApprovalFactory
{
    public class LoanDisbursementTopupApprovalFactory : IApprovalFactory
    {
        private readonly ChevronCoopDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public LoanDisbursementTopupApprovalFactory(ChevronCoopDbContext dbContext, IMapper mapper, IMediator mediator)
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

        public async Task<CommandResult<bool>> Process(Approval approval, string? approvedById, string? comment,
          ApprovalStatus status)
        {
            var response = new CommandResult<bool>();

            var disbursement = await _dbContext.LoanDisbursements.FirstOrDefaultAsync(x => x.Id == approval.EntityId);

            if (disbursement != null)
            {
                if (status != ApprovalStatus.APPROVED)
                {
                    disbursement.Status = TransactionStatus.REJECTED;
                    _dbContext.LoanDisbursements.Update(disbursement);
                    _dbContext.SaveChanges();

                    response.Response = false;
                    return response;
                }

                var transaction = new LoanTransactionCommand()
                {
                    EntityId = approval.EntityId,
                    EntityType = typeof(LoanDisbursement),
                    IsApproved = true,
                    ApprovedOn = DateTime.Now,
                    TransactionAction = TransactionAction.POST,
                    TransactionDate = DateTime.Now,
                    TransactionType = TransactionType.LOAN_DISBURSEMENT_TOPUP,
                    LoanAccountId = disbursement.LoanAccountId,
                    TransactionJournalId = null
                };

                var transactionResponse = await _mediator.Send(transaction);
                if (!transactionResponse.ErrorFlag && transactionResponse.Response.IsPosted)
                {
                    disbursement.DisbursementStatus = DisbursementStatusType.DISBURSED;
                    disbursement.DisbursementDate = DateTime.UtcNow.ToLocalTime();
                    _dbContext.LoanDisbursements.Update(disbursement);
                    await _dbContext.SaveChangesAsync();
                }
            }

            response.Response = true;

            return response;
        }
    }
}
