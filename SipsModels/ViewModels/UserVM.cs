using System.ComponentModel.DataAnnotations;

namespace Sips.SipsModels.ViewModels
{
    public class UserVM
    {
        [Key]
        [Required]
        public string Email { get; set; }
    }
}
