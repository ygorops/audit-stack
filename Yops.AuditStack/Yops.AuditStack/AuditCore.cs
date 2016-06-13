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

		private static Validation.AuditVOValidation _auditValidation;
		internal static Validation.AuditVOValidation AuditValidation
		{
			get
			{
				if (_auditValidation == null)
					_auditValidation = new Validation.AuditVOValidation();

				return _auditValidation;
			}
		}
	}
}
