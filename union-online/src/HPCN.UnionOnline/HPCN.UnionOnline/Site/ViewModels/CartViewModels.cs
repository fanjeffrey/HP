using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Site.ViewModels
{
    public class CartAddViewModel
    {
        public Guid ActivityProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class CartUpdateViewModel
    {
        public Guid CartProductId { get; set; }
        public int Quantity { get; set; }
    }
}
