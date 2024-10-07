using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEnterprise.BLL.Entities
{
    public class EnterpriseSubject
    {
        public virtual int Id { get; set; }
        public virtual int Code { get; set; }
        public virtual string Name { get; set; }
        public virtual int SubjectType { get; set; }
    }
}
