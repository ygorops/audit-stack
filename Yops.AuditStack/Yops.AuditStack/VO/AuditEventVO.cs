using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yops.AuditStack.VO
{
	public class AuditEventVO
	{
		public string Event { get; set; }
		public object Data { get; set; }
		public DateTime Date { get; set; }
	}
}
