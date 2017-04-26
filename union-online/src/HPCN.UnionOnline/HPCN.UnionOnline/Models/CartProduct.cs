using System.ComponentModel.DataAnnotations;

namespace HPCN.UnionOnline.Models
{
    public class CartProduct : Entity
    {
        [Required]
        public User User { get; set; }

        [Required]
        public Product Product { get; set; }

        [Required]
        public int Quantity { get; set; } = 1;
    }
}
