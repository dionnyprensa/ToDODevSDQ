using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ToDO.Infraestructure.Configurations;
using ToDO.Infraestructure.DataModel;

namespace ToDO.Infraestructure.Context
{
    public class ToDoContext : DbContext
    {
        public ToDoContext() : base("ToDOMSSQL")
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ToDoContext>());
            Database.SetInitializer(new ToDoInitializer());
        }

        public DbSet<UserDataModel> Users { get; set; }
        public DbSet<TaskDataModel> Tasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new UserConfig());
            modelBuilder.Configurations.Add(new TaskConfig());
        }
    }
}