namespace Yops.AuditStack
{
	using System.Collections.Generic;

	public interface IAuditPersistense
	{
		void SaveAudit(AuditEvidenceCollection evidences);
	}
}
