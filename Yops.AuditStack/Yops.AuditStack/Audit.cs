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

		protected Audit AuditSetAuthor(string author)
		{
			this._auditVO.Author = author;
			return this;
		}

		protected Audit AuditSetOperation(string operation)
		{
			this._auditVO.Operation = operation;
			return this;
		}

		protected Audit AuditSetDate(DateTime date)
		{
			this._auditVO.Date = date;
			return this;
		}

		protected Audit AuditAddEvent(AuditEventVO @event)
		{
			this._auditVO.Events.Add(@event);
			return this;
		}

		protected Audit AuditAddEvent(string @event, object data, DateTime date)
		{
			return this.AuditAddEvent(new AuditEventVO() { Event = @event, Data = data, Date = date });
		}

		protected void AuditSave()
		{
			// Validate AuditVO
			AuditCore.AuditValidation.Validate(this._auditVO);


			// Save AuditVO
			AuditCore.AuditPersistence.SaveAudit(this._auditVO);
		}
    }
}