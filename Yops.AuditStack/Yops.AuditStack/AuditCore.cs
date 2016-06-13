using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yops.AuditStack
{
	public class AuditCore
	{
		internal static IAuditPersistense AuditPersistence;
		public static void ConfigureAuditPersistence(IAuditPersistense auditPersistence)
		{
			AuditPersistence = auditPersistence;
		}
	}
}
