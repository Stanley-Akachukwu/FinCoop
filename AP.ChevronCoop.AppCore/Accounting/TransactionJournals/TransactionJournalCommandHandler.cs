using AP.ChevronCoop.AppCore.Services.Helpers;
using AP.ChevronCoop.AppDomain.Accounting.TransactionJournals;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.JournalEntries;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.TransactionJournals
{
    public class TransactionJournalCommandHandler :
     IRequestHandler<QueryTransactionJournalCommand, CommandResult<IQueryable<TransactionJournal>>>,
    IRequestHandler<CreateTransactionJournalCommand, CommandResult<TransactionJournalViewModel>>,
    IRequestHandler<UpdateTransactionJournalCommand, CommandResult<TransactionJournalViewModel>>,
    IRequestHandler<PostTransactionJournalCommand, CommandResult<TransactionJournalViewModel>>,
    IRequestHandler<ReverseTransactionJournalCommand, CommandResult<TransactionJournalViewModel>>,
    IRequestHandler<DeleteTransactionJournalCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public TransactionJournalCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<TransactionJournalCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<TransactionJournal>>> Handle(QueryTransactionJournalCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<TransactionJournal>>();
            rsp.Response = dbContext.TransactionJournals;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<TransactionJournalViewModel>> Handle(CreateTransactionJournalCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<TransactionJournalViewModel>();
            var entity = mapper.Map<TransactionJournal>(request);

            entity.TransactionNo = NHiloHelper.GetNextKey(nameof(TransactionJournal)).ToString();
            entity.IsPosted = false;
            entity.IsReversed = false;
            entity.TransactionType = Enum.Parse<TransactionType>(request.TransactionType);
            entity.JournalEntries = new List<JournalEntry>();

            foreach (var entry in request.JournalEntries)
            {

                var account = dbContext.LedgerAccounts.FirstOrDefault(p => p.Code == entry.AccountCode);

                entity.JournalEntries.Add(new JournalEntry()
                {
                    TransactionEntryNo = NHiloHelper.GetNextKey(nameof(JournalEntry)).ToString(),
                    EntryType = Enum.Parse<TransactionEntryType>(entry.EntryType),
                    AccountId = account.Id,
                    Debit = entry.Debit,
                    Credit = entry.Credit,
                    TransactionDate = entry.TransactionDate,

                });

            }

            dbContext.TransactionJournals.Add(entity);
            await dbContext.SaveChangesAsync();


            if (request.PostEntries)
            {
                entity = await dbContext.TransactionJournals.Where(p => p.TransactionNo == entity.TransactionNo)
               .Include(p => p.JournalEntries).ThenInclude(p => p.Account).FirstOrDefaultAsync();

                entity.Post();
                dbContext.TransactionJournals.Update(entity);
                await dbContext.SaveChangesAsync();

            }

            rsp.Response = mapper.Map<TransactionJournalViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<TransactionJournalViewModel>> Handle(UpdateTransactionJournalCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<TransactionJournalViewModel>();
            var entity = await dbContext.TransactionJournals.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.TransactionJournals.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<TransactionJournalViewModel>(entity);

            return rsp;
        }


        public async Task<CommandResult<TransactionJournalViewModel>> Handle(PostTransactionJournalCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<TransactionJournalViewModel>();
            var entity = await dbContext.TransactionJournals.Where(p => p.TransactionNo == request.TransactionNo)
                .Include(p => p.JournalEntries).ThenInclude(p => p.Account).FirstOrDefaultAsync();

            entity.Post();

            dbContext.TransactionJournals.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<TransactionJournalViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<TransactionJournalViewModel>> Handle(ReverseTransactionJournalCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<TransactionJournalViewModel>();
            var entity = await dbContext.TransactionJournals.Where(p => p.TransactionNo == request.TransactionNo)
                 .Include(p => p.JournalEntries).ThenInclude(p => p.Account).FirstOrDefaultAsync();

            entity.Reverse();

            dbContext.TransactionJournals.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<TransactionJournalViewModel>(entity);

            return rsp;
        }



        public async Task<CommandResult<string>> Handle(DeleteTransactionJournalCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.TransactionJournals.FindAsync(request.Id);

            dbContext.TransactionJournals.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }








}
