namespace Yops.AuditStack
{
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;
	using VO;

	public interface IAuditPersistense
	{
		void SaveAudit(AuditVO auditVO);
		Task SaveAuditAsync(AuditVO auditVO, CancellationToken cancellationToken);

		AuditVO Get(string id);
		Task<AuditVO> GetAsync(string id, CancellationToken cancellationToken);

		List<AuditVO> GetByAuthor(string author, int page, int size);
		Task<List<AuditVO>> GetByAuthorAsync(string author, int page, int size, CancellationToken cancellationToken);

		List<AuditVO> GetByOperation(string operation, int page, int size);
		Task<List<AuditVO>> GetByOperationAsync(string operation, int page, int size, CancellationToken cancellationToken);
	}
}
