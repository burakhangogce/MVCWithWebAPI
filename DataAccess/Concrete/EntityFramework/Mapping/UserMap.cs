using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataAccess.Concrete.EntityFramework.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", @"dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserName).HasColumnName("UserName").HasMaxLength(50).IsRequired();
            builder.Property(x => x.FirstName).HasColumnName("FirstName").HasMaxLength(50).IsRequired();
            builder.Property(x => x.LastName).HasColumnName("LastName").HasMaxLength(50).IsRequired();
            builder.Property(x => x.Password).HasColumnName("Password").HasMaxLength(20).IsRequired();
            builder.Property(x => x.Gender).HasColumnName("Gender").IsRequired();
            builder.Property(x => x.DateOfBirth).HasColumnName("DateOfBirth").IsRequired();
            builder.Property(x => x.CreatedDate).HasDefaultValue(DateTime.Now);

            builder.HasData(new User
            {
                Id = 1,
                FirstName = "Burakhan",
                LastName = "Gögce",
                Password ="12345",
                Gender = true,
                DateOfBirth = Convert.ToDateTime("17-05-2000"),
                CreatedDate = DateTime.Now,
                Adress = "ISTANBUL",
                CreatedUserId = 1,
                Email = "burakhan@gmail.com",
                UserName = "burakhan"
            });
        }
    }
}
