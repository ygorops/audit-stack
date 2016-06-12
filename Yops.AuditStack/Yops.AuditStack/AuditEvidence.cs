namespace Yops.AuditStack
{
	using System;

	public class AuditEvidence<T>
	{
		public readonly string Author;
		public readonly T Data;
		public readonly string Id;
		public readonly DateTime Date;
		public readonly string Operation;

		public AuditEvidence(string id, string author, T data, string operation, DateTime? date = null)
		{
			this.Author = author;
			this.Data = data;
			this.Operation = operation;
			this.Date = date.HasValue ? date.Value : DateTime.Now;
			this.Id = id;
		}
	}
}
