using AP.ChevronCoop.AppDomain.Accounting.FinancialCalendars;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.FinancialCalendars;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.FinancialCalendars
{
    public class FinancialCalendarCommandHandler :
     IRequestHandler<QueryFinancialCalendarCommand, CommandResult<IQueryable<FinancialCalendar>>>,
    IRequestHandler<CreateFinancialCalendarCommand, CommandResult<FinancialCalendarViewModel>>,
    IRequestHandler<UpdateFinancialCalendarCommand, CommandResult<FinancialCalendarViewModel>>,
    IRequestHandler<DeleteFinancialCalendarCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public FinancialCalendarCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<FinancialCalendarCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<FinancialCalendar>>> Handle(QueryFinancialCalendarCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<FinancialCalendar>>();
            rsp.Response = dbContext.FinancialCalendars;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<FinancialCalendarViewModel>> Handle(CreateFinancialCalendarCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<FinancialCalendarViewModel>();
            var entity = mapper.Map<FinancialCalendar>(request);

            dbContext.FinancialCalendars.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<FinancialCalendarViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<FinancialCalendarViewModel>> Handle(UpdateFinancialCalendarCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<FinancialCalendarViewModel>();
            var entity = await dbContext.FinancialCalendars.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.FinancialCalendars.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<FinancialCalendarViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteFinancialCalendarCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.FinancialCalendars.FindAsync(request.Id);

            dbContext.FinancialCalendars.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }








}
