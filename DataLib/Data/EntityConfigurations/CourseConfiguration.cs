using DataLib.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLib.Data.EntityConfigurations
{
	public class CourseConfiguration : IEntityTypeConfiguration<Course>
	{
		public void Configure(EntityTypeBuilder<Course> builder)
		{
			builder.ToTable("COURSES");

			builder.HasKey(t => t.Id);
			builder.Property(t => t.Id).HasMaxLength(9);
			builder.Property(t => t.CourseName).IsRequired();
			builder.Property(t => t.CourseSection).IsRequired();
			builder.Property(t => t.IsAvailable).IsRequired();
			builder.Property(t => t.PreRequisiteId);
		}
	}
}
