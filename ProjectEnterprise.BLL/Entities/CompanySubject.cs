using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEnterprise.BLL.Entities
{
    public class CompanySubject
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string LatinName { get; set; }
        public virtual string Address { get; set; }
    }
}
