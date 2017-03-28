using System.ComponentModel.DataAnnotations;

namespace ToDO.Models
{
    public class UserViewModel
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => string.Format($"{0} {1}", FirstName, LastName);
    }
}