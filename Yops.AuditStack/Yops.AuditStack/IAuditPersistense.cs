namespace Yops.AuditStack
{

	public interface IAuditPersistense
	{
		void SaveAudit(VO.AuditVO evidences);
	}
}
