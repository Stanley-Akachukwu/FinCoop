using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.CustomerDepositProductPublications;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.Products.CustomerDepositProductPublications;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.DepositProducts.CustomerDepositProductPublications
{
    public class CustomerDepositProductPublicationCommandHandler :
      IRequestHandler<QueryCustomerDepositProductPublicationCommand, CommandResult<IQueryable<CustomerDepositProductPublication>>>,
   IRequestHandler<CreateCustomerDepositProductPublicationCommand, CommandResult<CustomerDepositProductPublicationViewModel>>,
   IRequestHandler<UpdateCustomerDepositProductPublicationCommand, CommandResult<CustomerDepositProductPublicationViewModel>>,
   IRequestHandler<DeleteCustomerDepositProductPublicationCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public CustomerDepositProductPublicationCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<CustomerDepositProductPublicationCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<CustomerDepositProductPublication>>> Handle(QueryCustomerDepositProductPublicationCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<CustomerDepositProductPublication>>();
            rsp.Response = dbContext.CustomerDepositProductPublications;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<CustomerDepositProductPublicationViewModel>> Handle(CreateCustomerDepositProductPublicationCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<CustomerDepositProductPublicationViewModel>();
            var entity = mapper.Map<CustomerDepositProductPublication>(request);

            dbContext.CustomerDepositProductPublications.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<CustomerDepositProductPublicationViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<CustomerDepositProductPublicationViewModel>> Handle(UpdateCustomerDepositProductPublicationCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<CustomerDepositProductPublicationViewModel>();
            var entity = await dbContext.CustomerDepositProductPublications.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.CustomerDepositProductPublications.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<CustomerDepositProductPublicationViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteCustomerDepositProductPublicationCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.CustomerDepositProductPublications.FindAsync(request.Id);

            dbContext.CustomerDepositProductPublications.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }

}


