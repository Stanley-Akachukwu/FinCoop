using ChevronCoop.Web.AppUI.BlazorServer.Data;
using FluentValidation;

namespace ChevronCoop.Web.AppUI.BlazorServer.Validation
{
	public class MemberAccountValidator : AbstractValidator<CustomerAccountUpdateModel>
	{
		public MemberAccountValidator()
		{
			RuleFor(p => p.bankId).NotEmpty().WithMessage("Bank Name Is Required!");
			RuleFor(p => p.accountNumber).NotEmpty().WithMessage("Account Number Is Required!");
			RuleFor(p => p.branch).NotEmpty().WithMessage("Branch Is Required!");

		}
		
	}
}
