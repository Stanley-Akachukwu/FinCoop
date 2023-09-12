using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.MasterData.GlobalCodes;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationRoles;

namespace AP.ChevronCoop.Entities.Security.Approvals;
/*
 * ROLES
 * SuperAdmin,InternalControl,FinanceHead,AccountOfficer
 * 
 * 
 * Admin/Staff user creation
 * Member enrollment
 * 
 * 
 * 
 * Loan application
 * Loan disbursement
 * Loan account closure
 * Loan repayment posting ??
 * 
 * 
 * Savings deposits
 * Savings deposits
 * 
 * public enum ApprovalType(ADMIN_USER_CREATE->"Approve Create User Admin", 
 * MEMBER_ENROLLMENT->"Approve Member Enrollment",->"/member/enrollment/345"
 * LOAN_APPLICATION,->"/loan/application/167"
 * LOAN_DISBURSEMENT,LOAN_CLOSURE,
 * SAVINGS_APPLICATION, SAVINGS_DEPOSIT, SAVINGS_WITHDRAWAL
 * 
 * 
 * Global codes table
 * Id,Code,Name,Type
 * 1,LN1, Approve Loan Application, APPROVAL_TYPE ===>Admin,FinanceHead
 * 2,LN2, Approve Loan Disbursement ,APPROVAL_TYPE
 * 3,ER1, Approve user enrollment,APPROVAL_TYPE
 * 
 * Roles table
 * Id, Name
 * 1, Admin
 * 2, FinanceHead
 * 3,InternalControl
 * 
 * ApprovalRoles(Id,ApprovalTypeGlobalCodeId,RoleId,Order)
 * 1,1,1 ->Admin
 * 1,2,2 ->FinanceHead
 * 
 * 
 * PendingApprovals(Id,ApprovalTypeGlobalCodeId, TableName,EntityType->Loans EntityId, EntityPageUrl->"/loans/application/345"
 * RequestDate,RequestedByUserId,
 * IsApproved,ProcessedDate, ProcessedByUserId,
 * 
 * 
 * {Field1,Field2,Field3}
 * 
 */

public class ApprovalRole : BaseEntity<string>
{
  public ApprovalRole()
  {
    Id = NUlid.Ulid.NewUlid().ToString();
  }


  //filter by CodeType=APPROVAL_TYPE
  [Required] public string EventGlobalCodeId { get; set; }

  [ForeignKey(nameof(EventGlobalCodeId))]
  public virtual GlobalCode PermissionEvent { get; set; }

  [Required] public string RoleId { get; set; }

  [ForeignKey(nameof(RoleId))] public virtual ApplicationRole Role { get; set; }

  public int Order { get; set; }


  public override string DisplayCaption => "";

  public override string DropdownCaption => "";

  public override string ShortCaption => "";
}