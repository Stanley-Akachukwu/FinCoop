using System.ComponentModel.DataAnnotations.Schema;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.MasterData.Departments;
using AP.ChevronCoop.Entities.Security;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;


namespace AP.ChevronCoop.Entities.CoopCustomers.Customers;

public class Customer : BaseEntity<string>
{
    public Customer()
    {
        Id = NUlid.Ulid.NewUlid().ToString();
    }

    public string CustomerNo { get; set; }
    public string ApplicationUserId { get; set; }
    public virtual ApplicationUser ApplicationUser { get; set; }

    public string CashAccountId { get; set; }

    public virtual LedgerAccount CashAccount { get; set; }

    public int YearsOfService { get; set; }
    public DateTime? DateOfEmployment { get; set; }
    public bool IsKycStarted { get; set; }
    public bool IsKycCompleted { get; set; }
    public DateTime? KycStartDate { get; set; }
    public DateTime? KycCompletedDate { get; set; }
    public MemberProfileStatus Status { get; set; }
    public MemberType MemberType { get; set; }
    public Gender Gender { get; set; }
    public string ProfileImageUrl { get; set; }
    public string PassportUrl { get; set; }
    public string IdentificationType { get; set; }
    public string IdentificationNumber { get; set; }
    public string IdentificationUrl { get; set; }
    public bool KycSubmitted { get; set; }
    public DateTime? KycSubmittedOn { get; set; }
    public bool KycApproved { get; set; }
    public DateTime? KycApprovedOn { get; set; }
    public bool KycApprovedBy { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string? DepartmentId { get; set; }
    public virtual Department Department { get; set; }

    public string? MemberId { get; set; }
    public string? CAI { get; set; }
    public string? RetireeNumber { get; set; }
    public string? StateOfOrigin { get; set; }
    public string? PrimaryEmail { get; set; }
    public string? SecondaryEmail { get; set; }
    public string? PrimaryPhone { get; set; }
    public string? SecondaryPhone { get; set; }
    public string? ResidentialAddress { get; set; }
    public string? OfficeAddress { get; set; }
    public string? Rank { get; set; }
    public string? JobRole { get; set; }

    public DateTimeOffset? DOB { get; set; }
    // Remove
    public string? Address { get; set; }
    public string? Country { get; set; }

    public string? State { get; set; }

    // End Remove


    public override string DisplayCaption => "";

    public override string DropdownCaption => "";

    public override string ShortCaption => "";
}