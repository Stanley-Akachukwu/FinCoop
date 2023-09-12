using AP.ChevronCoop.AppDomain.Accounting.TransactionDocuments;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.TransactionDocuments;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.TransactionDocuments
{
    public class TransactionDocumentCommandHandler :
     IRequestHandler<QueryTransactionDocumentCommand, CommandResult<IQueryable<TransactionDocument>>>,
    IRequestHandler<CreateTransactionDocumentCommand, CommandResult<TransactionDocumentViewModel>>,
    IRequestHandler<UpdateTransactionDocumentCommand, CommandResult<TransactionDocumentViewModel>>,
    IRequestHandler<DeleteTransactionDocumentCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public TransactionDocumentCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<TransactionDocumentCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<TransactionDocument>>> Handle(QueryTransactionDocumentCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<TransactionDocument>>();
            rsp.Response = dbContext.TransactionDocuments;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<TransactionDocumentViewModel>> Handle(CreateTransactionDocumentCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<TransactionDocumentViewModel>();
            var entity = mapper.Map<TransactionDocument>(request);

            dbContext.TransactionDocuments.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<TransactionDocumentViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<TransactionDocumentViewModel>> Handle(UpdateTransactionDocumentCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<TransactionDocumentViewModel>();
            var entity = await dbContext.TransactionDocuments.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.TransactionDocuments.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<TransactionDocumentViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteTransactionDocumentCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.TransactionDocuments.FindAsync(request.Id);

            dbContext.TransactionDocuments.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }








}
