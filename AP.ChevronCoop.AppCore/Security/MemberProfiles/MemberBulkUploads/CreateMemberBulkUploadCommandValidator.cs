using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads;
using FluentValidation;

namespace AP.ChevronCoop.AppCore.Security.MemberProfiles.MemberBulkUploads
{
    public partial class CreateMemberBulkUploadCommandValidator : AbstractValidator<CreateMemberBulkUploadCommand>
    {
        //public CreateMemberBulkUploadCommandValidator(RoleManager<ApplicationRole> roleManager)
        //{

        //    RuleFor(p => p.UploadedByUserId).NotNull().WithMessage("Required creator/initiator User ID.");
        //    RuleForEach(x => x.MemberDataUploads).SetValidator(new MemberDataUploadValidator(roleManager));
        //    RuleFor(p => p).Custom((data, context) =>
        //    {
        //    });
        //}

    }
}

