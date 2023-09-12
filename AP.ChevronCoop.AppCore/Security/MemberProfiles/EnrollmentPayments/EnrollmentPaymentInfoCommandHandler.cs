using AP.ChevronCoop.AppDomain.Security.MemberProfiles.EnrollmentPayments;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.MemberProfiles.EnrollmentPayments;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.MemberProfiles.EnrollmentPayments
{
    public class EnrollmentPaymentInfoCommandHandler :
     IRequestHandler<QueryEnrollmentPaymentInfoCommand, CommandResult<IQueryable<EnrollmentPaymentInfo>>>,
    IRequestHandler<CreateEnrollmentPaymentInfoCommand, CommandResult<EnrollmentPaymentInfoViewModel>>,
    IRequestHandler<UpdateEnrollmentPaymentInfoCommand, CommandResult<EnrollmentPaymentInfoViewModel>>,
    IRequestHandler<DeleteEnrollmentPaymentInfoCommand, CommandResult<string>>,
    IRequestHandler<CheckEnrollmentPaymentInfoCommand, CommandResult<bool>>

    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public EnrollmentPaymentInfoCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<EnrollmentPaymentInfoCommandHandler> _logger, IMapper _mapper)
        {
            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;
        }


        public Task<CommandResult<IQueryable<EnrollmentPaymentInfo>>> Handle(QueryEnrollmentPaymentInfoCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<EnrollmentPaymentInfo>>();
            rsp.Response = dbContext.EnrollmentPaymentInfos;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<EnrollmentPaymentInfoViewModel>> Handle(CreateEnrollmentPaymentInfoCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<EnrollmentPaymentInfoViewModel>();
            var entity = mapper.Map<EnrollmentPaymentInfo>(request);

            entity.Evidence = request.Document;

            dbContext.EnrollmentPaymentInfos.Add(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            rsp.Response = mapper.Map<EnrollmentPaymentInfoViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<EnrollmentPaymentInfoViewModel>> Handle(UpdateEnrollmentPaymentInfoCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<EnrollmentPaymentInfoViewModel>();
            var entity = await dbContext.EnrollmentPaymentInfos.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.EnrollmentPaymentInfos.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<EnrollmentPaymentInfoViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteEnrollmentPaymentInfoCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.EnrollmentPaymentInfos.FindAsync(request.Id);

            dbContext.EnrollmentPaymentInfos.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }

        public async Task<CommandResult<bool>> Handle(CheckEnrollmentPaymentInfoCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<bool>();

            rsp.Response = true;
            rsp.Message = $"Check was successful. An Enrollment payment info could be associated with {request.Email}";

            return rsp;
        }
    }






}
