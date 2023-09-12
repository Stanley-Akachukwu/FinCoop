using AP.ChevronCoop.AppDomain.Customers.CustomerNextOfKins;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerNextOfKins;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Customers.CustomerNextOfKins;

public class CustomerNextOfKinCommandHandler :
IRequestHandler<QueryCustomerNextOfKinCommand, CommandResult<IQueryable<CustomerNextOfKin>>>,
IRequestHandler<CreateCustomerNextOfKinCommand, CommandResult<CustomerNextOfKinViewModel>>,
IRequestHandler<UpdateCustomerNextOfKinCommand, CommandResult<CustomerNextOfKinViewModel>>,
IRequestHandler<DeleteCustomerNextOfKinCommand, CommandResult<string>>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger logger;
    private readonly IMapper mapper;

    public CustomerNextOfKinCommandHandler(ChevronCoopDbContext appDbContext,
    ILogger<CustomerNextOfKinCommandHandler> _logger, IMapper _mapper)
    {
        dbContext = appDbContext;
        logger = _logger;
        mapper = _mapper;
    }


    public Task<CommandResult<IQueryable<CustomerNextOfKin>>> Handle(QueryCustomerNextOfKinCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<IQueryable<CustomerNextOfKin>>();
        rsp.Response = dbContext.CustomerNextOfKins;

        return Task.FromResult(rsp);
    }


    public async Task<CommandResult<CustomerNextOfKinViewModel>> Handle(CreateCustomerNextOfKinCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<CustomerNextOfKinViewModel>();
        var entity = mapper.Map<CustomerNextOfKin>(request);

        dbContext.CustomerNextOfKins.Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = mapper.Map<CustomerNextOfKinViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<CustomerNextOfKinViewModel>> Handle(UpdateCustomerNextOfKinCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<CustomerNextOfKinViewModel>();
        var entity = await dbContext.CustomerNextOfKins.FindAsync(request.Id);

        mapper.Map(request, entity);

        dbContext.CustomerNextOfKins.Update(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = mapper.Map<CustomerNextOfKinViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteCustomerNextOfKinCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await dbContext.CustomerNextOfKins.FindAsync(request.Id);

        dbContext.CustomerNextOfKins.Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = "Data successfully deleted";

        return rsp;
    }
}