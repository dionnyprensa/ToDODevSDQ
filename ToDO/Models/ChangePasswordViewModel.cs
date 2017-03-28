using System.ComponentModel.DataAnnotations;

namespace ToDO.Models
{
    public class ChangePasswordViewModel
    {
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; }
    }
}