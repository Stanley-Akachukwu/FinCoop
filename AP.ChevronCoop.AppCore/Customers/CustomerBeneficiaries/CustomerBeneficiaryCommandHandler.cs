using AP.ChevronCoop.AppDomain.Customers.CustomerBeneficiaries;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBeneficiaries;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.CoopCustomers.CustomerBeneficiaries;

public class CustomerBeneficiaryCommandHandler :
IRequestHandler<QueryCustomerBeneficiaryCommand, CommandResult<IQueryable<CustomerBeneficiary>>>,
IRequestHandler<CreateCustomerBeneficiaryCommand, CommandResult<CustomerBeneficiaryViewModel>>,
IRequestHandler<UpdateCustomerBeneficiaryCommand, CommandResult<CustomerBeneficiaryViewModel>>,
IRequestHandler<DeleteCustomerBeneficiaryCommand, CommandResult<string>>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger logger;
    private readonly IMapper mapper;

    public CustomerBeneficiaryCommandHandler(ChevronCoopDbContext appDbContext,
    ILogger<CustomerBeneficiaryCommandHandler> _logger, IMapper _mapper)
    {
        dbContext = appDbContext;
        logger = _logger;
        mapper = _mapper;
    }


    public Task<CommandResult<IQueryable<CustomerBeneficiary>>> Handle(QueryCustomerBeneficiaryCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<IQueryable<CustomerBeneficiary>>();
        rsp.Response = dbContext.CustomerBeneficiaries;

        return Task.FromResult(rsp);
    }


    public async Task<CommandResult<CustomerBeneficiaryViewModel>> Handle(CreateCustomerBeneficiaryCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<CustomerBeneficiaryViewModel>();
        var entity = mapper.Map<CustomerBeneficiary>(request);

        dbContext.CustomerBeneficiaries.Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = mapper.Map<CustomerBeneficiaryViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<CustomerBeneficiaryViewModel>> Handle(UpdateCustomerBeneficiaryCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<CustomerBeneficiaryViewModel>();
        var entity = await dbContext.CustomerBeneficiaries.FindAsync(request.Id);

        mapper.Map(request, entity);

        dbContext.CustomerBeneficiaries.Update(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = mapper.Map<CustomerBeneficiaryViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteCustomerBeneficiaryCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await dbContext.CustomerBeneficiaries.FindAsync(request.Id);

        dbContext.CustomerBeneficiaries.Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = "Data successfully deleted";

        return rsp;
    }
}