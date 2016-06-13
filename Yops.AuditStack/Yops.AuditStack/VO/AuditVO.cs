namespace Yops.AuditStack.VO
{
	using System;

	public class AuditVO
	{
		public AuditVO()
		{
			this.Events = new AuditEventVOCollection();
		}

		public string Id { get; set; }
		public string Author { get; set; }
		public string Operation { get; set; }
		public DateTime? Date { get; set; }
		public AuditEventVOCollection Events { get; set; }
	}
}
