using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalDocuments;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalDocuments;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalDocuments
{
    public class ApprovalDocumentCommandHandler :
     IRequestHandler<QueryApprovalDocumentCommand, CommandResult<IQueryable<ApprovalDocument>>>,
    IRequestHandler<CreateApprovalDocumentCommand, CommandResult<ApprovalDocumentViewModel>>,
    IRequestHandler<UpdateApprovalDocumentCommand, CommandResult<ApprovalDocumentViewModel>>,
    IRequestHandler<DeleteApprovalDocumentCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public ApprovalDocumentCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<ApprovalDocumentCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<ApprovalDocument>>> Handle(QueryApprovalDocumentCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<ApprovalDocument>>();
            rsp.Response = dbContext.ApprovalDocuments;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<ApprovalDocumentViewModel>> Handle(CreateApprovalDocumentCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<ApprovalDocumentViewModel>();
            var entity = mapper.Map<ApprovalDocument>(request);

            dbContext.ApprovalDocuments.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<ApprovalDocumentViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<ApprovalDocumentViewModel>> Handle(UpdateApprovalDocumentCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<ApprovalDocumentViewModel>();
            var entity = await dbContext.ApprovalDocuments.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.ApprovalDocuments.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<ApprovalDocumentViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteApprovalDocumentCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.ApprovalDocuments.FindAsync(request.Id);

            dbContext.ApprovalDocuments.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }






}
