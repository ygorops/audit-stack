namespace Yops.AuditStack.Test
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class Dog : Audit
    {
        private const string AUTHOR_VALUE = "testAuthor";

        public string Name { get; set; }
        public string Owner { get; set; }

        public void Save()
        {
            // Validate Owner
            ValidateOwner();
            this.AuditAddEvent("checkOwner", this.Owner, DateTime.Now);

            // Save in database
            SaveSQL();
            this.AuditAddEvent("save", this, DateTime.Now);

            // Audit	
            SaveAudit("saveOperation");
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            // Validate Owner
            ValidateOwner();
            this.AuditAddEvent("checkOwner", this.Owner, DateTime.Now);

            // Save in database
            SaveSQL();
            this.AuditAddEvent("save", this, DateTime.Now);

            // Audit	
            await SaveAuditAsync("saveOperation", CancellationToken.None);
        }

        #region SaveAudit
        private void SaveAudit(string operation)
        {
            SetAuditParameters(operation);
            this.AuditSave();
        }

        private async Task SaveAuditAsync(string operation, CancellationToken cancellationToken)
        {
            SetAuditParameters(operation);
            await this.AuditSaveAsync(cancellationToken);
        }

        private void SetAuditParameters(string operation)
        {
            this.AuditSetId(Guid.NewGuid().ToString());
            this.AuditSetAuthor(AUTHOR_VALUE);
            this.AuditSetOperation(operation);
            this.AuditSetDate(DateTime.Now);
        }
        #endregion

        private void SaveSQL() { }
        private void ValidateOwner() { }
    }
}
