using System.ComponentModel.DataAnnotations;

namespace Sips.SipsModels.ViewModels
{
    public class UserRoleVM
    {
        [Key]
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set;}

        [Required]
        public string Email { get; set; }
    }
}
