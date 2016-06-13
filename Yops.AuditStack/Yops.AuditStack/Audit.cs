namespace Yops.AuditStack
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using VO;

	public class Audit
	{
		private AuditVO _auditVO;

		public Audit()
		{
			this._auditVO = new AuditVO();
		}

		#region Setting parameters
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
		#endregion
		public static AuditVO AuditGet(string id)
		{
			return AuditCore.AuditPersistence.Get(id);
		}

		public static Task<AuditVO> AuditGetAsync(string id, CancellationToken cancellationToken)
		{
			return AuditCore.AuditPersistence.GetAsync(id, cancellationToken);
		}

		#region save methods
		protected void AuditSave()
		{
			// Validate AuditVO
			AuditCore.AuditValidation.Validate(this._auditVO);

			// Save AuditVO
			AuditCore.AuditPersistence.SaveAudit(this._auditVO);
		}

		protected async Task AuditSaveAsync(CancellationToken cancellationToken)
		{
			// Validate AuditVO
			AuditCore.AuditValidation.Validate(this._auditVO);

			// Save AuditVO
			await AuditCore.AuditPersistence.SaveAuditAsync(this._auditVO, cancellationToken);
		}
		#endregion
	}
}