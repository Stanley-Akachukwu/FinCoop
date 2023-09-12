//using AP.ChevronCoop.AppDomain.Accounting.LienTypes;
//using AP.ChevronCoop.Commons;
//using AP.ChevronCoop.Entities;
//using AP.ChevronCoop.Entities.Accounting.LienTypes;
//using AutoMapper;
//using MediatR;
//using Microsoft.Extensions.Logging;

//namespace AP.ChevronCoop.AppCore.Accounting.LienTypes
//{
//    public class LienTypeCommandHandler :
//     IRequestHandler<QueryLienTypeCommand, CommandResult<IQueryable<LienType>>>,
//    IRequestHandler<CreateLienTypeCommand, CommandResult<LienTypeViewModel>>,
//    IRequestHandler<UpdateLienTypeCommand, CommandResult<LienTypeViewModel>>,
//    IRequestHandler<DeleteLienTypeCommand, CommandResult<string>>
//    {

//        private readonly ChevronCoopDbContext dbContext;
//        private readonly ILogger logger;
//        private readonly IMapper mapper;

//        public LienTypeCommandHandler(ChevronCoopDbContext appDbContext,
//        ILogger<LienTypeCommandHandler> _logger, IMapper _mapper)
//        {

//            dbContext = appDbContext;
//            logger = _logger;
//            mapper = _mapper;

//        }


//        public Task<CommandResult<IQueryable<LienType>>> Handle(QueryLienTypeCommand request, CancellationToken cancellationToken)
//        {

//            var rsp = new CommandResult<IQueryable<LienType>>();
//            rsp.Response = dbContext.LienTypes;

//            return Task.FromResult(rsp);
//        }




//        public async Task<CommandResult<LienTypeViewModel>> Handle(CreateLienTypeCommand request, CancellationToken cancellationToken)
//        {

//            var rsp = new CommandResult<LienTypeViewModel>();
//            var entity = mapper.Map<LienType>(request);

//            dbContext.LienTypes.Add(entity);
//            await dbContext.SaveChangesAsync();

//            rsp.Response = mapper.Map<LienTypeViewModel>(entity);

//            return rsp;
//        }

//        public async Task<CommandResult<LienTypeViewModel>> Handle(UpdateLienTypeCommand request, CancellationToken cancellationToken)
//        {

//            var rsp = new CommandResult<LienTypeViewModel>();
//            var entity = await dbContext.LienTypes.FindAsync(request.Id);

//            mapper.Map(request, entity);

//            dbContext.LienTypes.Update(entity);
//            await dbContext.SaveChangesAsync();

//            rsp.Response = mapper.Map<LienTypeViewModel>(entity);

//            return rsp;
//        }

//        public async Task<CommandResult<string>> Handle(DeleteLienTypeCommand request, CancellationToken cancellationToken)
//        {
//            var rsp = new CommandResult<string>();
//            var entity = await dbContext.LienTypes.FindAsync(request.Id);

//            dbContext.LienTypes.Remove(entity);
//            await dbContext.SaveChangesAsync();

//            rsp.Response = "Data successfully deleted";

//            return rsp;
//        }
//    }








//}
