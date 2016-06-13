namespace Yops.AuditStack
{
	using System.Collections.Generic;

	public class Audit
    {
		private IAuditPersistense _persistence;
		private List<AuditEvidence> _evidences;

		public Audit(IAuditPersistense persistenceConfiguration)
		{
			this._persistence = persistenceConfiguration;
			this._evidences = new List<AuditEvidence>();
		}

		protected void AddAuditEvidence(AuditEvidence evidence)
		{
			this._evidences.Add(evidence);
		}

		protected void SaveAudit()
		{
			_persistence.SaveAudit(_evidences);
		}
    }
}