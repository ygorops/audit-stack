using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yops.AuditStack.VO
{
	public class AuditVO
	{
		public string Id { get; set; }
		public string Author { get; set; }
		public string Operation { get; set; }
		public DateTime Date { get; set; }
	}
}
