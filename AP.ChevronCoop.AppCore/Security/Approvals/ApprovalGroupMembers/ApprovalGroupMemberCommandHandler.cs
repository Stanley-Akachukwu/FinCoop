using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroupMembers;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroupMembers;
using AutoMapper;
using MediatR;
using System.Net;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalGroupMembers
{
    public class ApprovalGroupMemberCommandHandler :
    IRequestHandler<CreateApprovalGroupMemberCommand, CommandResult<ApprovalGroupMemberViewModel>>
    {

        private readonly ChevronCoopDbContext _dbContext;
        private readonly IMapper _mapper;
        public ApprovalGroupMemberCommandHandler(ChevronCoopDbContext appDbContext, IMapper mapper)
        {
            _dbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<CommandResult<ApprovalGroupMemberViewModel>> Handle(CreateApprovalGroupMemberCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<ApprovalGroupMemberViewModel>();
            var entity = _mapper.Map<ApprovalGroupMember>(request);
            _dbContext.ApprovalGroupMembers.Add(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            rsp.Response = new ApprovalGroupMemberViewModel
            {
                Id = entity.Id,
            };

            rsp.StatusCode = (int)HttpStatusCode.Created;
            rsp.Message = "Approval group created and members added.";
            return rsp;
        }
    }
}
