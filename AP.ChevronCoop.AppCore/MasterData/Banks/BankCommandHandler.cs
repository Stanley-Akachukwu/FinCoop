using AP.ChevronCoop.AppDomain.MasterData.Banks;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.MasterData.Banks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.MasterData.Banks
{
    public class BankCommandHandler :
     IRequestHandler<QueryBankCommand, CommandResult<IQueryable<Bank>>>,
    IRequestHandler<CreateBankCommand, CommandResult<BankViewModel>>,
    IRequestHandler<UpdateBankCommand, CommandResult<BankViewModel>>,
    IRequestHandler<DeleteBankCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public BankCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<BankCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<Bank>>> Handle(QueryBankCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<Bank>>();
            rsp.Response = dbContext.Banks;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<BankViewModel>> Handle(CreateBankCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<BankViewModel>();
            var entity = mapper.Map<Bank>(request);

            dbContext.Banks.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<BankViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<BankViewModel>> Handle(UpdateBankCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<BankViewModel>();
            var entity = await dbContext.Banks.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.Banks.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<BankViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteBankCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.Banks.FindAsync(request.Id);

            dbContext.Banks.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }








}
