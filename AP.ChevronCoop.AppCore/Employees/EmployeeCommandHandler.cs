using AP.ChevronCoop.AppDomain.Employees;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Employees;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Employees
{
    public class EmployeeCommandHandler :
     IRequestHandler<QueryEmployeeCommand, CommandResult<IQueryable<Employee>>>,
    IRequestHandler<CreateEmployeeCommand, CommandResult<EmployeeViewModel>>,
    IRequestHandler<UpdateEmployeeCommand, CommandResult<EmployeeViewModel>>,
    IRequestHandler<DeleteEmployeeCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public EmployeeCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<EmployeeCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<Employee>>> Handle(QueryEmployeeCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<Employee>>();
            rsp.Response = dbContext.Employees;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<EmployeeViewModel>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<EmployeeViewModel>();
            var entity = mapper.Map<Employee>(request);

            dbContext.Employees.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<EmployeeViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<EmployeeViewModel>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<EmployeeViewModel>();
            var entity = await dbContext.Employees.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.Employees.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<EmployeeViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.Employees.FindAsync(request.Id);

            dbContext.Employees.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }

}
