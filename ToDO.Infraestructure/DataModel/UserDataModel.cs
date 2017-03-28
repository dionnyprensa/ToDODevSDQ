using System;
using System.Collections.Generic;

namespace ToDO.Infraestructure.DataModel
{
    public class UserDataModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => string.Format($"{0} {1}", FirstName, LastName);
        public bool RememberMe { get; set; }
        public bool SoftDelete { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<TaskDataModel> ToDoList { get; set; } = new HashSet<TaskDataModel>();
    }
}