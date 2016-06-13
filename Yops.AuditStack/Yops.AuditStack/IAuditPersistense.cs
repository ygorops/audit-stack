namespace Yops.AuditStack
{
	using System.Threading;
	using System.Threading.Tasks;

	public interface IAuditPersistense
	{
		void SaveAudit(VO.AuditVO auditVO);
		Task SaveAuditAsync(VO.AuditVO auditVO, CancellationToken cancellationToken);
	}
}
