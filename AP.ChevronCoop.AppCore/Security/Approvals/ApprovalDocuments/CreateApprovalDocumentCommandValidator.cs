using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalDocuments;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalDocuments
{
    public partial class CreateApprovalDocumentCommandValidator : AbstractValidator<CreateApprovalDocumentCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<CreateApprovalDocumentCommandValidator> logger;
        public CreateApprovalDocumentCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateApprovalDocumentCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;

            RuleFor(p => p.ApprovalId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.Evidence).NotNull();
            RuleFor(p => p.MimeType).NotEmpty().MaximumLength(128);
            RuleFor(p => p.FileName).NotEmpty().MaximumLength(512);
            RuleFor(p => p.FileSize).NotNull();






            RuleFor(p => p).Custom((data, context) =>
            {


                /*
                        var parentExists = dbContext.Parent.Where(r => r.Id == data.ParentId).Any();
                        if (!parentExists)
                        {
                            context.AddFailure(
                            new ValidationFailure(nameof(data.ParentId),
                            "Invalid key.", data.ParentId));

                        }

                        var checkName = dbContext.ApprovalDocuments.Where(r => r.Name.ToLower() == data.Name.ToLower()
                && r.CodeTypeId == data.CodeTypeId).Any();
                        if (checkName)
                        {
                            context.AddFailure(
                            new ValidationFailure(nameof(data.Name),
                            "Duplicate names are not allowed.", data.Name));
                        }

                        var checkCode = dbContext.ApprovalDocuments.Where(r => r.Code.ToLower() == data.Code.ToLower()
                && r.CodeTypeId != data.CodeTypeId).Any();
                        if (checkCode)
                        {
                            context.AddFailure(
                            new ValidationFailure(nameof(data.Code),
                            "Duplicate codes are not allowed.", data.Code));
                        }
                */

                var fileNameExist = dbContext.ApprovalDocuments.Where(r => r.FileName.ToLower() == data.FileName.ToLower()).Any();
                if (fileNameExist)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.FileName),
                    "Duplicate names are not allowed.", data.FileName));
                }

            });

        }


    }




}
