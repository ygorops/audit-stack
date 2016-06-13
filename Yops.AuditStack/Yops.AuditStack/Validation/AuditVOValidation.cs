using System;
using System.Linq;
using System.Collections.Generic;
using Yops.AuditStack.VO;

namespace Yops.AuditStack.Validation
{
	internal class AuditVOValidation
	{
		internal AuditVOValidation() { }

		internal void Validate(AuditVO auditVO)
		{
			List<Exception> exceptions = new List<Exception>();

			if (string.IsNullOrWhiteSpace(auditVO.Author))
				exceptions.Add(new ArgumentNullException("AuditVO.Author"));
			if (!auditVO.Date.HasValue)
				exceptions.Add(new ArgumentNullException("AuditVO.Date"));
			if (string.IsNullOrWhiteSpace(auditVO.Operation))
				exceptions.Add(new ArgumentNullException("AuditVO.Operation"));
			if (string.IsNullOrWhiteSpace(auditVO.Id))
				exceptions.Add(new ArgumentNullException("AuditVO.Id"));

			if (exceptions.Any())
				throw new AggregateException(exceptions);
		}
	}
}
