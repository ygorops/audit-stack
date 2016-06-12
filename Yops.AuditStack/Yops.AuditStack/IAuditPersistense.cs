using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yops.AuditStack
{
	public interface IAuditPersistense<T>
	{
		void SaveAudit(T data);
	}
}
