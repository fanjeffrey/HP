using System.ComponentModel.DataAnnotations;

namespace HPCN.UnionOnline.Models
{
    public class User : Entity
    {
        [Required]
        [StringLength(200)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        public bool IsAdmin { get; set; }

        [Required]
        public bool Disabled { get; set; }

        public Employee Employee { get; set; }
    }
}
