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

		protected void AuditSetId(string id)
		{
			this._auditVO.Id = id;
		}

		protected void AuditSetAuthor(string author)
		{
			this._auditVO.Author = author;
		}

		protected void AuditSetOperation(string operation)
		{
			this._auditVO.Operation = operation;
		}

		protected void AuditSetDate(DateTime date)
		{
			this._auditVO.Date = date;
		}

		protected void AuditAddEvent(AuditEventVO @event)
		{
			this._auditVO.Events.Add(@event);
		}

		protected void AuditAddEvent(string @event, object data, DateTime date)
		{
			this.AuditAddEvent(new AuditEventVO() { Event = @event, Data = data, Date = date });
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