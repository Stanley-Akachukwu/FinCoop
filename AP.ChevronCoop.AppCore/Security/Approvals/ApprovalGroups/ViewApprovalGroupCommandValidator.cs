using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroups;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalGroups
{
    public partial class ViewApprovalGroupCommandValidator : AbstractValidator<ViewApprovalGroupCommand>
    {
        private readonly ChevronCoopDbContext _dbContext;
        private readonly ILogger<ViewApprovalGroupCommandValidator> _logger;
        public ViewApprovalGroupCommandValidator(ChevronCoopDbContext appDbContext, ILogger<ViewApprovalGroupCommandValidator> logger)
        {
            _dbContext = appDbContext;
            _logger = logger;
            RuleFor(p => p.CreatedByUserId).NotNull().WithMessage("CreatedByUserId address required.");
            RuleFor(p => p.Id).NotNull().WithMessage("Required GroupId.");
        }
    }
}


