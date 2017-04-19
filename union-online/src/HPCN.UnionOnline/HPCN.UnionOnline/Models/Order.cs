using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Models
{
    public class Order : Entity
    {
        public Employee Employee { get; set; }
        public Activity Activity { get; set; }
        public ICollection<OrderDetail> Details { get; set; }
    }
}
