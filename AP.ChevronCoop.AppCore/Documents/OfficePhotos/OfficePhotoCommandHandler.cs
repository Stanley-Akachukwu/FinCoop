using AP.ChevronCoop.AppDomain.Documents.OfficePhotos;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Documents.OfficePhotos;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Documents.OfficePhotos
{
    public class OfficePhotoCommandHandler :
     IRequestHandler<QueryOfficePhotoCommand, CommandResult<IQueryable<OfficePhoto>>>,
    IRequestHandler<CreateOfficePhotoCommand, CommandResult<OfficePhotoViewModel>>,
    IRequestHandler<UpdateOfficePhotoCommand, CommandResult<OfficePhotoViewModel>>,
    IRequestHandler<DeleteOfficePhotoCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public OfficePhotoCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<OfficePhotoCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<OfficePhoto>>> Handle(QueryOfficePhotoCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<OfficePhoto>>();
            rsp.Response = dbContext.OfficePhotos;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<OfficePhotoViewModel>> Handle(CreateOfficePhotoCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<OfficePhotoViewModel>();
            var entity = mapper.Map<OfficePhoto>(request);

            dbContext.OfficePhotos.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<OfficePhotoViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<OfficePhotoViewModel>> Handle(UpdateOfficePhotoCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<OfficePhotoViewModel>();
            var entity = await dbContext.OfficePhotos.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.OfficePhotos.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<OfficePhotoViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteOfficePhotoCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.OfficePhotos.FindAsync(request.Id);

            dbContext.OfficePhotos.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }








}
