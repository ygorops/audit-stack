namespace Yops.AuditStack
{
	using System;
	using VO;

	public class Audit
	{
		private AuditVO _auditVO;

		public Audit()
		{
			this._auditVO = new AuditVO();
		}

		protected Audit SetAuthor(string author)
		{
			this._auditVO.Author = author;
			return this;
		}

		protected Audit SetOperation(string operation)
		{
			this._auditVO.Operation = operation;
			return this;
		}

		protected Audit SetDate(DateTime date)
		{
			this._auditVO.Date = date;
			return this;
		}

		protected Audit AddEvent(AuditEventVO @event)
		{
			this._auditVO.Events.Add(@event);
			return this;
		}

		protected Audit AddEvent(string @event, object data, DateTime date)
		{
			return this.AddEvent(new AuditEventVO() { Event = @event, Data = data, Date = date });
		}

		protected void SaveAudit()
		{
			// Validate AuditVO
			AuditCore.AuditValidation.Validate(this._auditVO);


			// Save AuditVO
			AuditCore.AuditPersistence.SaveAudit(this._auditVO);
		}
    }
}