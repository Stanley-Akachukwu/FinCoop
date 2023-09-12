using AP.ChevronCoop.Commons;

namespace AP.ChevronCoop.Entities.Security.Alerts.EmailAlerts
{
	public class ApprovalEmailAlert : BaseEntity<string>
	{

		public ApprovalEmailAlert()
		{
			Id = NUlid.Ulid.NewUlid().ToString();
		}
		public string ApprovalId { get; set; }
		public string ApprovalWorkflowId { get; set; }
		public string EmailTitle { get; set; }
		public string EmailBody { get; set; }
		public bool TaskCompleted { get; set; }

		public override string DisplayCaption
		{
			get
			{
				return "";
			}
		}

		public override string DropdownCaption
		{
			get
			{
				return "";
			}
		}

		public override string ShortCaption
		{
			get
			{
				return "";
			}
		}
	}
}
