using AP.ChevronCoop.AppCore.Services.LogServices;
using AP.ChevronCoop.AppDomain.Documents.CustomerDocuments;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Documents.CustomerDocuments;
using AutoMapper;
using MediatR;


namespace AP.ChevronCoop.AppCore.Documents.CustomerDocuments
{
    public class CustomerDocumentCommandHandler :
         IRequestHandler<CreateCustomerPaymentDocumentCommand, CommandResult<CustomerPaymentDocumentViewModel>>,
         IRequestHandler<QueryCustomerPaymentDocumentCommand, CommandResult<IQueryable<CustomerPaymentDocument>>>

    {
        private readonly ChevronCoopDbContext dbContext;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public CustomerDocumentCommandHandler(ChevronCoopDbContext appDbContext, ILoggerService logger, IMapper mapper)
        {
            dbContext = appDbContext;

            _logger = logger;
            _mapper = mapper;

        }

        public async Task<CommandResult<CustomerPaymentDocumentViewModel>> Handle(CreateCustomerPaymentDocumentCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<CustomerPaymentDocumentViewModel>();
            var entity = _mapper.Map<CustomerPaymentDocument>(request);

            dbContext.CustomerPaymentDocuments.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = _mapper.Map<CustomerPaymentDocumentViewModel>(entity);

            return rsp;

        }

        public Task<CommandResult<IQueryable<CustomerPaymentDocument>>> Handle(QueryCustomerPaymentDocumentCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<CustomerPaymentDocument>>();
            rsp.Response = dbContext.CustomerPaymentDocuments;

            return Task.FromResult(rsp);

        }
    }
}
