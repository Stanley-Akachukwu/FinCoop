using AP.ChevronCoop.AppDomain.MasterData.Departments;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.MasterData.Departments;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.MasterData.Departments
{
    public class DepartmentCommandHandler :
     IRequestHandler<QueryDepartmentCommand, CommandResult<IQueryable<Department>>>,
    IRequestHandler<CreateDepartmentCommand, CommandResult<DepartmentViewModel>>,
    IRequestHandler<UpdateDepartmentCommand, CommandResult<DepartmentViewModel>>,
    IRequestHandler<DeleteDepartmentCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public DepartmentCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<DepartmentCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<Department>>> Handle(QueryDepartmentCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<Department>>();
            rsp.Response = dbContext.Departments;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<DepartmentViewModel>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<DepartmentViewModel>();
            var entity = mapper.Map<Department>(request);

            dbContext.Departments.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<DepartmentViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<DepartmentViewModel>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<DepartmentViewModel>();
            var entity = await dbContext.Departments.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.Departments.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<DepartmentViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.Departments.FindAsync(request.Id);

            dbContext.Departments.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }








}
