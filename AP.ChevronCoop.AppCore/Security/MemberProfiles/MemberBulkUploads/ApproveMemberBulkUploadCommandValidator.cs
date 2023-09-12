using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads;
using FluentValidation;

namespace AP.ChevronCoop.AppCore.Security.MemberProfiles.MemberBulkUploads
{
    public partial class ApproveMemberBulkUploadCommandValidator : AbstractValidator<ApproveMemberBulkUploadCommand>
    {
        //private readonly UserManager<ApplicationRole> _userManager;

        public ApproveMemberBulkUploadCommandValidator()
        {

            //_userManager = userManager;

            RuleFor(p => p.ApprovedById).NotNull().WithMessage("Required approval personnel user ID.");
            RuleFor(p => p.MemberBulkUploadSessionId).NotNull().WithMessage("Required bulk upload reference ID.");
            RuleFor(p => p.ApprovalId).NotNull().WithMessage("Required approval setup ID.");


            //RuleFor(m => m).Custom(async (data, context) =>
            //{

            //    var user = await _userManager.FindByIdAsync(data.ApprovalId);
            //    if (user == null)
            //        context.AddFailure(new ValidationFailure(nameof(data.ApprovalId), "You are not in role to process this  approval request.", data.ApprovalId));

            //    if (!await _userManager.IsInRoleAsync(user, "Internal Control"))
            //           context.AddFailure(new ValidationFailure(nameof(data.ApprovalId), "You do not have the permission for this request.", data.ApprovalId));

            //});
        }



    }
}
