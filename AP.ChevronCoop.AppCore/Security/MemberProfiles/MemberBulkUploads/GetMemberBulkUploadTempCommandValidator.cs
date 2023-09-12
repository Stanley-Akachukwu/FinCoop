using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace AP.ChevronCoop.AppCore.Security.MemberProfiles.MemberBulkUploads
{

    public class GetMemberBulkUploadTempCommandValidator : AbstractValidator<GetMemberBulkUploadTempCommand>
    {
        private readonly ChevronCoopDbContext _dbContext;

        public GetMemberBulkUploadTempCommandValidator(ChevronCoopDbContext dbContext)
        {
            _dbContext = dbContext;
            RuleFor(m => m).Custom(async (data, context) =>
            {

                var memberBulkUploads = await _dbContext.MemberBulkUploadTemp.Where(x => x.SessionId == data.SessionId).ToListAsync();

                if (memberBulkUploads == null)
                {
                    context.AddFailure(new ValidationFailure(nameof(data.SessionId), "does not exist", data.SessionId));
                }

            });


        }
    }
}
