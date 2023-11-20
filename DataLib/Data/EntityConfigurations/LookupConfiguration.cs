using DataLib.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLib.Data.EntityConfigurations
{
	public class LookupsConfiguration : IEntityTypeConfiguration<Lookups>
	{
		public void Configure(EntityTypeBuilder<Lookups> builder)
		{
			builder.ToTable("LOOKUPS");

			builder.HasKey(x => x.Id);
			builder.Property(x=> x.LookupId).IsRequired();
			builder.Property(x=> x.LookupName).IsRequired();
			builder.Property(x=> x.EnglishValue).IsRequired();
			builder.Property(x=> x.ArabicValue).IsRequired();
		}
	}
}
