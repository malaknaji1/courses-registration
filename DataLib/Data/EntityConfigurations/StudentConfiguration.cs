using DataLib.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLib.Data.EntityConfigurations
{
	public class StudentConfiguration : IEntityTypeConfiguration<Student>
	{
		public void Configure(EntityTypeBuilder<Student> builder)
		{
			builder.ToTable("STUDENTS");
			builder.HasKey(t => t.Id);

			builder.Property(t => t.Id).HasMaxLength(9);
			builder.Property(t => t.FirstName).IsRequired();
			builder.Property(t => t.LastName).IsRequired();
			builder.Property(t => t.Email).IsRequired();
			builder.Property(t => t.Major).IsRequired();
		}
	}
}
