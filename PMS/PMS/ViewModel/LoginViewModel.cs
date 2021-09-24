using System.ComponentModel.DataAnnotations;

namespace PMS.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remeber me")]
        public bool RememberMe { get; set; }
    }
}
