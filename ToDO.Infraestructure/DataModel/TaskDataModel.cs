using System;

namespace ToDO.Infraestructure.DataModel
{
    public class TaskDataModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
        public virtual UserDataModel User { get; set; }
    }
}