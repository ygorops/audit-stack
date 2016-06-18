namespace Yops.AuditStack.Test
{
    using System.Collections.Generic;
    using VO;

    internal class AuditStackStorage
    {
        private static List<AuditVO> _audits = new List<AuditVO>();

        public static List<AuditVO> GetAll()
        {
            return _audits;
        }

        public static void Add(AuditVO audit)
        {
            _audits.Add(audit);
        }
    }
}
