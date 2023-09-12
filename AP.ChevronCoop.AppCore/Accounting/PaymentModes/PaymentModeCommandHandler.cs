using AP.ChevronCoop.AppDomain.Accounting.PaymentModes;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.PaymentModes;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.PaymentModes
{
    public class PaymentModeCommandHandler :
     IRequestHandler<QueryPaymentModeCommand, CommandResult<IQueryable<PaymentMode>>>,
    IRequestHandler<CreatePaymentModeCommand, CommandResult<PaymentModeViewModel>>,
    IRequestHandler<UpdatePaymentModeCommand, CommandResult<PaymentModeViewModel>>,
    IRequestHandler<DeletePaymentModeCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public PaymentModeCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<PaymentModeCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<PaymentMode>>> Handle(QueryPaymentModeCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<PaymentMode>>();
            rsp.Response = dbContext.PaymentModes;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<PaymentModeViewModel>> Handle(CreatePaymentModeCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<PaymentModeViewModel>();
            var entity = mapper.Map<PaymentMode>(request);

            dbContext.PaymentModes.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<PaymentModeViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<PaymentModeViewModel>> Handle(UpdatePaymentModeCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<PaymentModeViewModel>();
            var entity = await dbContext.PaymentModes.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.PaymentModes.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<PaymentModeViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeletePaymentModeCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.PaymentModes.FindAsync(request.Id);

            dbContext.PaymentModes.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }








}
