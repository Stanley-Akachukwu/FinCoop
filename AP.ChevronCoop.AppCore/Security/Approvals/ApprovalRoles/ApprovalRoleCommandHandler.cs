using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalRoles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Approvals;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalRoles
{
    public class ApprovalRoleCommandHandler :
     IRequestHandler<QueryApprovalRoleCommand, CommandResult<IQueryable<ApprovalRole>>>,
    IRequestHandler<CreateApprovalRoleCommand, CommandResult<ApprovalRoleViewModel>>,
    IRequestHandler<UpdateApprovalRoleCommand, CommandResult<ApprovalRoleViewModel>>,
    IRequestHandler<DeleteApprovalRoleCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public ApprovalRoleCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<ApprovalRoleCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<ApprovalRole>>> Handle(QueryApprovalRoleCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<ApprovalRole>>();
            rsp.Response = dbContext.ApprovalRoles;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<ApprovalRoleViewModel>> Handle(CreateApprovalRoleCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<ApprovalRoleViewModel>();
            var entity = mapper.Map<ApprovalRole>(request);

            dbContext.ApprovalRoles.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<ApprovalRoleViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<ApprovalRoleViewModel>> Handle(UpdateApprovalRoleCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<ApprovalRoleViewModel>();
            var entity = await dbContext.ApprovalRoles.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.ApprovalRoles.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<ApprovalRoleViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteApprovalRoleCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.ApprovalRoles.FindAsync(request.Id);

            dbContext.ApprovalRoles.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }






}
