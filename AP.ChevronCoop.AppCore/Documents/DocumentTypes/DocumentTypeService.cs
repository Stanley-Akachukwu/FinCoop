//using AP.ChevronCoop.AppDomain.Documents.DocumentTypes;
//using AP.ChevronCoop.Commons;
//using AP.ChevronCoop.Entities;
//using AP.ChevronCoop.Entities.Documents;
//using AutoMapper;
//using Microsoft.Extensions.Logging;
//using System;

//namespace AP.ChevronCoop.AppCore.Documents.DocumentTypes
//{
//    public class DocumentTypeService : IDocumentTypeService
//    {
//        private readonly ChevronCoopDbContext dbContext;
//        private readonly ILogger<DocumentTypeService> logger;
//        private readonly IMapper mapper;

//        public DocumentTypeService(ChevronCoopDbContext appDbContext,
//        ILogger<DocumentTypeService> _logger, IMapper _mapper)
//        {

//            dbContext = appDbContext;
//            logger = _logger;
//            mapper = _mapper;

//        }


//        public Task<CommandResult<IQueryable<DocumentType>>> Handle(DocumentTypeQueryCommand request, CancellationToken cancellationToken)
//        {
//            var rsp = new CommandResult<IQueryable<DocumentType>>();
//            rsp.Response = dbContext.DocumentTypes;

//            return Task.FromResult(rsp);
//        }



//        public async Task<CommandResult<DocumentTypeViewModel>> Handle(DocumentTypeCreateCommand request, CancellationToken cancellationToken)
//        {

//            //throw new InvalidOperationException("test exception!");

//            var rsp = new CommandResult<DocumentTypeViewModel>();
//            var entity = mapper.Map<DocumentType>(request);

//            dbContext.DocumentTypes.Add(entity);
//            await dbContext.SaveChangesAsync();

//            rsp.Response = mapper.Map<DocumentTypeViewModel>(entity);

//            return rsp;
//        }

//        public async Task<CommandResult<DocumentTypeViewModel>> Handle(DocumentTypeUpdateCommand request, CancellationToken cancellationToken)
//        {
//            var rsp = new CommandResult<DocumentTypeViewModel>();
//            var entity = await dbContext.DocumentTypes.FindAsync(request.Id);

//            mapper.Map(request, entity);

//            dbContext.DocumentTypes.Update(entity);
//            await dbContext.SaveChangesAsync();

//            rsp.Response = mapper.Map<DocumentTypeViewModel>(entity);

//            return rsp;
//        }

//        public async Task<CommandResult<string>> Handle(DocumentTypeDeleteCommand request, CancellationToken cancellationToken)
//        {
//            var rsp = new CommandResult<string>();
//            var entity = await dbContext.DocumentTypes.FindAsync(request.Id);

//            dbContext.DocumentTypes.Remove(entity);
//            await dbContext.SaveChangesAsync();

//            rsp.Response = "Data successfully deleted";

//            return rsp;
//        }

//    }

//}