namespace ChevronCoop.Web.AppUI.BlazorServer.Data
{
	public class AuditTrailMasterViewResult
	{
		public int rowNumber { get; set; }
		public string id { get; set; }
		public string applicationUserId { get; set; }
		public DateTime timestamp { get; set; }
		public string eventType { get; set; }
		public string tableName { get; set; }
		public object primaryKey { get; set; }
		public string oldValues { get; set; }
		public string newValues { get; set; }
		public string auditJson { get; set; }
		public string module { get; set; }
		public object ipAddress { get; set; }
		public string action { get; set; }
		public string description { get; set; }
		public bool isActive { get; set; }
		public string createdByUserId { get; set; }
		public DateTime dateCreated { get; set; }
		public string updatedByUserId { get; set; }
		public DateTime dateUpdated { get; set; }
		public string deletedByUserId { get; set; }
		public bool isDeleted { get; set; }
		public DateTime? dateDeleted { get; set; }
		public string rowVersion { get; set; }
		public string fullText { get; set; }
		public string tags { get; set; }
		public string caption { get; set; }
		public string applicationUserId_UserName { get; set; }
		public string applicationUserId_Email { get; set; }
		public bool? applicationUserId_EmailConfirmed { get; set; }
		public string applicationUserId_PhoneNumber { get; set; }
		public bool? applicationUserId_PhoneNumberConfirmed { get; set; }
		public bool? applicationUserId_TwoFactorEnabled { get; set; }
		public object applicationUserId_LockoutEnd { get; set; }
		public bool? applicationUserId_LockoutEnabled { get; set; }
		public int? applicationUserId_AccessFailedCount { get; set; }
	}
}
