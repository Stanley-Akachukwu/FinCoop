using AP.ChevronCoop.AppDomain.Accounting.JournalEntries;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.JournalEntries;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.JournalEntries
{
    public class JournalEntryCommandHandler :
     IRequestHandler<QueryJournalEntryCommand, CommandResult<IQueryable<JournalEntry>>>,
    IRequestHandler<CreateJournalEntryCommand, CommandResult<JournalEntryViewModel>>,
    IRequestHandler<UpdateJournalEntryCommand, CommandResult<JournalEntryViewModel>>,
    IRequestHandler<DeleteJournalEntryCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public JournalEntryCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<JournalEntryCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<JournalEntry>>> Handle(QueryJournalEntryCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<JournalEntry>>();
            rsp.Response = dbContext.JournalEntries;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<JournalEntryViewModel>> Handle(CreateJournalEntryCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<JournalEntryViewModel>();
            var entity = mapper.Map<JournalEntry>(request);

            dbContext.JournalEntries.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<JournalEntryViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<JournalEntryViewModel>> Handle(UpdateJournalEntryCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<JournalEntryViewModel>();
            var entity = await dbContext.JournalEntries.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.JournalEntries.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<JournalEntryViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteJournalEntryCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.JournalEntries.FindAsync(request.Id);

            dbContext.JournalEntries.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }








}
