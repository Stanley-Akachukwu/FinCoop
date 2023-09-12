using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepartmentDepositProductPublications;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.Products.DepartmentDepositProductPublications;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.DepositProducts.DepartmentDepositProductPublications
{
    public class DepartmentDepositProductPublicationCommandHandler :
      IRequestHandler<QueryDepartmentDepositProductPublicationCommand, CommandResult<IQueryable<DepartmentDepositProductPublication>>>,
   IRequestHandler<CreateDepartmentDepositProductPublicationCommand, CommandResult<DepartmentDepositProductPublicationViewModel>>,
   IRequestHandler<UpdateDepartmentDepositProductPublicationCommand, CommandResult<DepartmentDepositProductPublicationViewModel>>,
   IRequestHandler<DeleteDepartmentDepositProductPublicationCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public DepartmentDepositProductPublicationCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<DepartmentDepositProductPublicationCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<DepartmentDepositProductPublication>>> Handle(QueryDepartmentDepositProductPublicationCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<DepartmentDepositProductPublication>>();
            rsp.Response = dbContext.DepartmentDepositProductPublications;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<DepartmentDepositProductPublicationViewModel>> Handle(CreateDepartmentDepositProductPublicationCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<DepartmentDepositProductPublicationViewModel>();
            var entity = mapper.Map<DepartmentDepositProductPublication>(request);

            dbContext.DepartmentDepositProductPublications.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<DepartmentDepositProductPublicationViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<DepartmentDepositProductPublicationViewModel>> Handle(UpdateDepartmentDepositProductPublicationCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<DepartmentDepositProductPublicationViewModel>();
            var entity = await dbContext.DepartmentDepositProductPublications.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.DepartmentDepositProductPublications.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<DepartmentDepositProductPublicationViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteDepartmentDepositProductPublicationCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.DepartmentDepositProductPublications.FindAsync(request.Id);

            dbContext.DepartmentDepositProductPublications.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }

}


