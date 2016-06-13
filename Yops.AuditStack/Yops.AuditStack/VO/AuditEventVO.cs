namespace Yops.AuditStack.VO
{
	using System;
	using System.Collections.Generic;

	public class AuditEventVO
	{
		public string Event { get; set; }
		public object Data { get; set; }
		public DateTime Date { get; set; }
	}

	public class AuditEventVOCollection : List<AuditEventVO>
	{

	}
}
