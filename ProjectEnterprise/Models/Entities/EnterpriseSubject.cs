using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEnterprise.Models.Entities
{
    public class EnterpriseSubject
    {
        public virtual string Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual int SubjectType { get; set; }
    }
}
