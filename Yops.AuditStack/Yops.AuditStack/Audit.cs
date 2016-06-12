namespace Yops.AuditStack
{
    public class Audit<T>
    {
		private IAuditPersistense<T> _persistence;

		public Audit(IAuditPersistense<T> persistenceConfiguration)
		{
			this._persistence = persistenceConfiguration;
		}

		protected void SaveAudit(T data)
		{
			_persistence.SaveAudit(data);
		}
    }
}