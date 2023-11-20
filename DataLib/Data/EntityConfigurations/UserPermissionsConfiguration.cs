using DataLib.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLib.Data.EntityConfigurations
{
	public class UserPermissionsConfiguration : IEntityTypeConfiguration<UserPermissions>
	{
		public void Configure(EntityTypeBuilder<UserPermissions> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x=> x.UserId);
			builder.Property(x=> x.PermissionId);

		}
	}
}
