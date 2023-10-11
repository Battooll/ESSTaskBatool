using ESSTaskBatool.Data;
using System.ComponentModel.DataAnnotations;

namespace ESSTaskBatool.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public Tanent1user user { get; set; }
    }
}
