using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBeneficiaries;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBeneficiaries;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.MemberProfiles.MemberBeneficiaries;

public class MemberBeneficiaryCommandHandler :
IRequestHandler<QueryMemberBeneficiaryCommand, CommandResult<IQueryable<MemberBeneficiary>>>,
IRequestHandler<CreateMemberBeneficiaryCommand, CommandResult<MemberBeneficiaryViewModel>>,
IRequestHandler<UpdateMemberBeneficiaryCommand, CommandResult<MemberBeneficiaryViewModel>>,
IRequestHandler<DeleteMemberBeneficiaryCommand, CommandResult<string>>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger logger;
    private readonly IMapper mapper;

    public MemberBeneficiaryCommandHandler(ChevronCoopDbContext appDbContext,
    ILogger<MemberBeneficiaryCommandHandler> _logger, IMapper _mapper)
    {
        dbContext = appDbContext;
        logger = _logger;
        mapper = _mapper;
    }


    public Task<CommandResult<IQueryable<MemberBeneficiary>>> Handle(QueryMemberBeneficiaryCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<IQueryable<MemberBeneficiary>>();
        rsp.Response = dbContext.MemberBeneficiaries;

        return Task.FromResult(rsp);
    }


    public async Task<CommandResult<MemberBeneficiaryViewModel>> Handle(CreateMemberBeneficiaryCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<MemberBeneficiaryViewModel>();
        var entity = mapper.Map<MemberBeneficiary>(request);

        dbContext.MemberBeneficiaries.Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = mapper.Map<MemberBeneficiaryViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<MemberBeneficiaryViewModel>> Handle(UpdateMemberBeneficiaryCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<MemberBeneficiaryViewModel>();
        var entity = await dbContext.MemberBeneficiaries.FindAsync(request.Id);

        mapper.Map(request, entity);

        dbContext.MemberBeneficiaries.Update(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = mapper.Map<MemberBeneficiaryViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteMemberBeneficiaryCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await dbContext.MemberBeneficiaries.FindAsync(request.Id);

        dbContext.MemberBeneficiaries.Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = "Data successfully deleted";

        return rsp;
    }
}