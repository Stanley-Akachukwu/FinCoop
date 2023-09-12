namespace AP.ChevronCoop.AppDomain.Customers.Customers;

public class CustomerViewModel : BaseViewModel
{
  public string CustomerNo { get; set; }

  public string LastName { get; set; }

  public string MiddleName { get; set; }
  public string MemberId { get; set; }

  public string FirstName { get; set; }

  public DateTimeOffset? Dob { get; set; }

  public string Gender { get; set; }

  public string ProfileImageUrl { get; set; }

  public DateTime? RegistrationDate { get; set; }
  public string DepartmentId { get; set; }
  public string ProfileId { get; set; }
  public int YearsOfService { get; set; }
  public DateTime? DateOfEmployment { get; set; }
  public string CreatedByUserId { get; set; }
  public string UpdatedByUserId { get; set; }
  public string DeletedByUserId { get; set; }

  public string FullName => $"{LastName} {MiddleName} {FirstName}";
}