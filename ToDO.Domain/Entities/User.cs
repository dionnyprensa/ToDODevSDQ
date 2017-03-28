using System;

namespace ToDO.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool RememberMe { get; set; }
        public bool SoftDelete { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}