using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using ToDO.Domain.Entities;
using ToDO.Infraestructure.DataModel;

namespace ToDO.Infraestructure.Configurations
{
    public class UserConfig : EntityTypeConfiguration<UserDataModel>
    {
        public UserConfig()
        {
            ToTable("users");

            HasKey(u => u.Id);

            Property(u => u.UserName)
                .HasMaxLength(16)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(
                    new IndexAttribute("IX_UserName", 1) {IsUnique = true}))
                .IsRequired();

            Property(u => u.Password)
                .IsRequired();

            Property(u => u.FirstName)
                .HasMaxLength(32)
                .IsRequired();

            Property(u => u.LastName)
                .HasMaxLength(32)
                .IsRequired();

            Property(u => u.RememberMe)
                .IsOptional();

            Property(u => u.SoftDelete)
                .IsRequired();

            Property(t => t.CreatedAt)
                .HasColumnType("date");
        }
    }
}