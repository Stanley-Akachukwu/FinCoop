using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberNextOfKins;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberNextOfKins;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.MemberProfiles.MemberNextOfKins;

public class MemberNextOfKinCommandHandler :
IRequestHandler<QueryMemberNextOfKinCommand, CommandResult<IQueryable<MemberNextOfKin>>>,
IRequestHandler<CreateMemberNextOfKinCommand, CommandResult<MemberNextOfKinViewModel>>,
IRequestHandler<UpdateMemberNextOfKinCommand, CommandResult<MemberNextOfKinViewModel>>,
IRequestHandler<DeleteMemberNextOfKinCommand, CommandResult<string>>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger logger;
    private readonly IMapper mapper;

    public MemberNextOfKinCommandHandler(ChevronCoopDbContext appDbContext,
    ILogger<MemberNextOfKinCommandHandler> _logger, IMapper _mapper)
    {
        dbContext = appDbContext;
        logger = _logger;
        mapper = _mapper;
    }


    public Task<CommandResult<IQueryable<MemberNextOfKin>>> Handle(QueryMemberNextOfKinCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<IQueryable<MemberNextOfKin>>();
        rsp.Response = dbContext.MemberNextOfKins;

        return Task.FromResult(rsp);
    }


    public async Task<CommandResult<MemberNextOfKinViewModel>> Handle(CreateMemberNextOfKinCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<MemberNextOfKinViewModel>();
        var entity = mapper.Map<MemberNextOfKin>(request);

        dbContext.MemberNextOfKins.Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = mapper.Map<MemberNextOfKinViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<MemberNextOfKinViewModel>> Handle(UpdateMemberNextOfKinCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<MemberNextOfKinViewModel>();
        var entity = await dbContext.MemberNextOfKins.FindAsync(request.Id);

        mapper.Map(request, entity);

        dbContext.MemberNextOfKins.Update(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = mapper.Map<MemberNextOfKinViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteMemberNextOfKinCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await dbContext.MemberNextOfKins.FindAsync(request.Id);

        dbContext.MemberNextOfKins.Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = "Data successfully deleted";

        return rsp;
    }
}