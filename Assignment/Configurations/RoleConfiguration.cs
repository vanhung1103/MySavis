using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Assignment.Models;
namespace Assignment.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role"); // Đặt tên bảng
            builder.HasKey(p => p.RoleID); // Set khóa chính
            // Cấu hình cho thuộc tính
            builder.Property(p => p.RoleName).HasColumnType("nvarchar(100)");
            builder.Property(p => p.Status).HasColumnType("int");
        }
    }
}
