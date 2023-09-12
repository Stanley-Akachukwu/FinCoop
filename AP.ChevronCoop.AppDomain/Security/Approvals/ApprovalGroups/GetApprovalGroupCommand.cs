using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroups;

public record GetApprovalGroupCommand(string Id): IRequest<CommandResult<GetApprovalGroupViewModel>>;