using AP.ChevronCoop.AppDomain.Documents.DocumentTypes;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Documents.DocumentTypes;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Documents.DocumentTypes
{
    public class DocumentTypeCommandHandler :
     IRequestHandler<QueryDocumentTypeCommand, CommandResult<IQueryable<DocumentType>>>,
    IRequestHandler<CreateDocumentTypeCommand, CommandResult<DocumentTypeViewModel>>,
    IRequestHandler<UpdateDocumentTypeCommand, CommandResult<DocumentTypeViewModel>>,
    IRequestHandler<DeleteDocumentTypeCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public DocumentTypeCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<DocumentTypeCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<DocumentType>>> Handle(QueryDocumentTypeCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<DocumentType>>();
            rsp.Response = dbContext.DocumentTypes;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<DocumentTypeViewModel>> Handle(CreateDocumentTypeCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<DocumentTypeViewModel>();
            var entity = mapper.Map<DocumentType>(request);

            dbContext.DocumentTypes.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<DocumentTypeViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<DocumentTypeViewModel>> Handle(UpdateDocumentTypeCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<DocumentTypeViewModel>();
            var entity = await dbContext.DocumentTypes.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.DocumentTypes.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<DocumentTypeViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteDocumentTypeCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.DocumentTypes.FindAsync(request.Id);

            dbContext.DocumentTypes.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }








}
