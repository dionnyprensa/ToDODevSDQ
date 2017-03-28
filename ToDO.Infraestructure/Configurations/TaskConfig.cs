using System.Data.Entity.ModelConfiguration;
using ToDO.Infraestructure.DataModel;

namespace ToDO.Infraestructure.Configurations
{
    public class TaskConfig : EntityTypeConfiguration<TaskDataModel>
    {
        public TaskConfig()
        {
            ToTable("tasks");

            HasKey(t => t.Id);

            Property(t => t.Title)
                .HasMaxLength(32)
                .IsRequired();

            Property(t => t.Description)
                .HasMaxLength(254)
                .IsRequired();

            Property(t => t.IsCompleted)
                .IsRequired();

            Property(t => t.CreatedAt)
                .HasColumnType("date")
                .IsRequired();

            Property(t => t.LastModified)
                .HasColumnType("date")
                .IsRequired();

            HasRequired(t => t.User)
                .WithMany(u => u.ToDoList);
        }
    }
}