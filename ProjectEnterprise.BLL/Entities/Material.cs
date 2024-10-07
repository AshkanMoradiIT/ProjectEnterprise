using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEnterprise.BLL.Entities
{
    public class Material
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual double Weight { get; set; }
        public virtual string Color { get; set; }
        public virtual string UnitOfMeasureName { get; set; }
    }
}
