using AP.ChevronCoop.AppDomain.Loans.LoanAccounts;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalNotifications;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AP.ChevronCoop.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AP.ChevronCoop.AppDomain.Loans.LoanTopups;
using AP.ChevronCoop.Entities.LoanTopupTransactions;
using AP.ChevronCoop.AppDomain.Loans.LoanOffsets;

namespace AP.ChevronCoop.AppCore.Services.ApprovalServices.ApprovalFactory
{
    public class LoanOffsetApprovalFactory : IApprovalFactory
    {
        private readonly ChevronCoopDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public LoanOffsetApprovalFactory(ChevronCoopDbContext dbContext, IMapper mapper, IMediator mediator)
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
            var loanOffset = await _dbContext.LoanOffsets.FirstOrDefaultAsync(x => x.Id == approval.EntityId);

            if (status != ApprovalStatus.APPROVED)
            {
                loanOffset.Status = TransactionStatus.REJECTED;
                _dbContext.LoanOffsets.Update(loanOffset);
                _dbContext.SaveChanges();
                
                _ = await _mediator.Send(new SendApprovalNotificationCommand
                {
                    ApprovalId = approval.Id,
                    IsApproved = false
                });

                response.Response = false;
                return response;
            }
            
            if (loanOffset != null)
            {
                await _mediator.Send(new ProcessLoanOffsetCommand
                {
                    LoanOffsetId = loanOffset.Id,
                    Status = status
                });
                
                _ = await _mediator.Send(new SendApprovalNotificationCommand
                {
                    ApprovalId = approval.Id,
                    IsApproved = true
                });
            }

            response.Response = true;

            return response;
        }
    }
}
