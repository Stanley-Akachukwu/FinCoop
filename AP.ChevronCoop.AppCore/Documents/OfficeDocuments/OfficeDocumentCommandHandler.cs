using AP.ChevronCoop.AppDomain.Documents.OfficeDocuments;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Documents.OfficeDocuments;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Documents.OfficeDocuments
{
    public class OfficeDocumentCommandHandler :
     IRequestHandler<QueryOfficeDocumentCommand, CommandResult<IQueryable<OfficeDocument>>>,
    IRequestHandler<CreateOfficeDocumentCommand, CommandResult<OfficeDocumentViewModel>>,
    IRequestHandler<UpdateOfficeDocumentCommand, CommandResult<OfficeDocumentViewModel>>,
    IRequestHandler<DeleteOfficeDocumentCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public OfficeDocumentCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<OfficeDocumentCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<OfficeDocument>>> Handle(QueryOfficeDocumentCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<OfficeDocument>>();
            rsp.Response = dbContext.OfficeDocuments;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<OfficeDocumentViewModel>> Handle(CreateOfficeDocumentCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<OfficeDocumentViewModel>();
            var entity = mapper.Map<OfficeDocument>(request);

            dbContext.OfficeDocuments.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<OfficeDocumentViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<OfficeDocumentViewModel>> Handle(UpdateOfficeDocumentCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<OfficeDocumentViewModel>();
            var entity = await dbContext.OfficeDocuments.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.OfficeDocuments.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<OfficeDocumentViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteOfficeDocumentCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.OfficeDocuments.FindAsync(request.Id);

            dbContext.OfficeDocuments.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }








}
