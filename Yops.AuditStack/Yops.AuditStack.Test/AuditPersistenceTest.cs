namespace Yops.AuditStack.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using VO;

    internal class AuditPersistenceTest : IAuditStackPersistense
    {
        public AuditVO Get(string id)
        {
            return AuditStackStorage.GetAll().Find(a => a.Id.Equals(id));
        }

        public Task<AuditVO> GetAsync(string id, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew<AuditVO>(() =>
            {
                return AuditStackStorage.GetAll().Find(a => a.Id.Equals(id));
            });
        }

        public List<AuditVO> GetByAuthor(string author, int page, int size)
        {
            if (page < 1)
                throw new ArgumentOutOfRangeException("page", page, "Value cannot be less than one.");

            return new List<AuditVO>(AuditStackStorage.GetAll().Where(a => a.Author.Equals(author)).Skip((page - 1) * size).Take(size));
        }

        public Task<List<AuditVO>> GetByAuthorAsync(string author, int page, int size, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => GetByAuthor(author, page, size), cancellationToken);
        }

        public List<AuditVO> GetByOperation(string operation, int page, int size)
        {
            throw new NotImplementedException();
        }

        public Task<List<AuditVO>> GetByOperationAsync(string operation, int page, int size, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void SaveAudit(AuditVO auditVO)
        {
            AuditStackStorage.Add(auditVO);
        }

        public async Task SaveAuditAsync(AuditVO auditVO, CancellationToken cancellationToken)
        {
            await Task.Factory.StartNew(() => { AuditStackStorage.Add(auditVO); });
        }
    }
}
