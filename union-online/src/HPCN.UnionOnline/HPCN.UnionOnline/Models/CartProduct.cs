using System.ComponentModel.DataAnnotations;

namespace HPCN.UnionOnline.Models
{
    public class CartProduct : AbstractEntity
    {
        [Required]
        public int Quantity { get; set; } = 1;

        [Required]
        public User User { get; set; }

        [Required]
        public ActivityProduct ActivityProduct { get; set; }
    }
}
