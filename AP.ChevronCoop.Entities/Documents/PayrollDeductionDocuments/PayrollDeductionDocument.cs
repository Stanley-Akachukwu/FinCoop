using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Payroll;
using AP.ChevronCoop.Entities.Payroll.PayrollDeductionSchedules;

namespace AP.ChevronCoop.Entities.Documents.PayrollDeductionDocuments;

public class PayrollDeductionDocument : BaseEntity<string>
{
    public PayrollDeductionDocument()
    {
        Id = NUlid.Ulid.NewUlid().ToString();
    }

    public string PayrollDeductionScheduleId { get; set; }
    public PayrollDeductionSchedule PayrollDeductionSchedule { get; set; }

    //LOAN REPAYMENT deductions or savings account funding deductions
    //public MemberPaymentUploadType DocumentType { get; set; }

    public int ProcessSequence { get; set; } = 0;
    public bool IsProcessed { get; set; }
    public DateTime ProcessedDate { get; set; }

    public byte[] Document { get; set; }
    public string MimeType { get; set; }
    public string FileName { get; set; }
    public int FileSize { get; set; }
    public override string DisplayCaption { get; }
    public override string DropdownCaption { get; }
    public override string ShortCaption { get; }
}