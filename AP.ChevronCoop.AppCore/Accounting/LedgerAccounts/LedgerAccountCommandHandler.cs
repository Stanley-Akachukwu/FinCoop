using AP.ChevronCoop.AppDomain.Accounting.LedgerAccounts;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.LedgerAccounts
{
    public class LedgerAccountCommandHandler :
     IRequestHandler<QueryLedgerAccountCommand, CommandResult<IQueryable<LedgerAccount>>>,
    IRequestHandler<CreateLedgerAccountCommand, CommandResult<LedgerAccountViewModel>>,
    IRequestHandler<UpdateLedgerAccountCommand, CommandResult<LedgerAccountViewModel>>,
    IRequestHandler<DeleteLedgerAccountCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public LedgerAccountCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<LedgerAccountCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<LedgerAccount>>> Handle(QueryLedgerAccountCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<LedgerAccount>>();
            rsp.Response = dbContext.LedgerAccounts;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<LedgerAccountViewModel>> Handle(CreateLedgerAccountCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<LedgerAccountViewModel>();
            var entity = mapper.Map<LedgerAccount>(request);

            dbContext.LedgerAccounts.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<LedgerAccountViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<LedgerAccountViewModel>> Handle(UpdateLedgerAccountCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<LedgerAccountViewModel>();
            var entity = await dbContext.LedgerAccounts.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.LedgerAccounts.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<LedgerAccountViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteLedgerAccountCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.LedgerAccounts.FindAsync(request.Id);

            dbContext.LedgerAccounts.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }








}
