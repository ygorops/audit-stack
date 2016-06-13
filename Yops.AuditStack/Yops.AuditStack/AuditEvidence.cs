namespace Yops.AuditStack
{
	using System;
	using System.Collections.Generic;
	public class AuditEvidence
	{
		public readonly string Author;
		public readonly object Data;
		public readonly string Id;
		public readonly DateTime Date;
		public readonly string Operation;

		public AuditEvidence(string id, string author, object data, string operation, DateTime? date = null)
		{
			this.Author = author;
			this.Data = data;
			this.Operation = operation;
			this.Date = date.HasValue ? date.Value : DateTime.Now;
			this.Id = id;
		}
	}

	public class AuditEvidenceCollection : List<AuditEvidence>
	{

	}
}
