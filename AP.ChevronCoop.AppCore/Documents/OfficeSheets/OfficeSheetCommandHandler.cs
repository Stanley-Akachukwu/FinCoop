using AP.ChevronCoop.AppDomain.Documents.OfficeSheets;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Documents.OfficeSheets;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Documents.OfficeSheets
{






    public class OfficeSheetCommandHandler :
     IRequestHandler<QueryOfficeSheetCommand, CommandResult<IQueryable<OfficeSheet>>>,
    IRequestHandler<CreateOfficeSheetCommand, CommandResult<OfficeSheetViewModel>>,
    IRequestHandler<UpdateOfficeSheetCommand, CommandResult<OfficeSheetViewModel>>,
    IRequestHandler<DeleteOfficeSheetCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public OfficeSheetCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<OfficeSheetCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<OfficeSheet>>> Handle(QueryOfficeSheetCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<OfficeSheet>>();
            rsp.Response = dbContext.OfficeSheets;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<OfficeSheetViewModel>> Handle(CreateOfficeSheetCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<OfficeSheetViewModel>();
            var entity = mapper.Map<OfficeSheet>(request);

            dbContext.OfficeSheets.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<OfficeSheetViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<OfficeSheetViewModel>> Handle(UpdateOfficeSheetCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<OfficeSheetViewModel>();
            var entity = await dbContext.OfficeSheets.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.OfficeSheets.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<OfficeSheetViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteOfficeSheetCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.OfficeSheets.FindAsync(request.Id);

            dbContext.OfficeSheets.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }








}
