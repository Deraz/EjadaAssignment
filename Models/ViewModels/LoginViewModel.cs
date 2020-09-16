using System.ComponentModel.DataAnnotations;

namespace EjadaAssignment.Models.ViewModels
{
    public class LoginViewModel
    {
        //Login View Data
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}