using DataLib.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLib.Data.EntityConfigurations
{
	public class UsersConfiguration : IEntityTypeConfiguration<Users>
	{
		public void Configure(EntityTypeBuilder<Users> builder)
		{
			builder.ToTable("USERS");

			builder.HasKey(t => t.Id);
			builder.Property(t=> t.Username).IsRequired();
			builder.Property(t => t.HashedPassword)
				   .HasColumnName("Password") 
				   .IsRequired();
			builder.Property(t=> t.UserType).IsRequired();
		}
	}
}
