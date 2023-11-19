using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Assignment.Models;
namespace Assignment.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User"); // Đặt tên bảng
            builder.HasKey(p => p.UserID); // Set khóa chính
            // Cấu hình cho thuộc tính
            builder.Property(p => p.Username).HasColumnType("nvarchar(256)").IsRequired();
            builder.Property(p => p.Password).HasColumnType("nvarchar(256)");
            builder.Property(p => p.Name).HasColumnType("nvarchar(256)");
            builder.Property(p => p.Avata).HasColumnType("nvarchar(256)");
            builder.Property(p => p.PhoneNumber).HasColumnType("nvarchar(256)");
            builder.Property(p=>p.Status).HasColumnType("int");
            builder.HasOne(p => p.Role).WithMany(p => p.Users).
                HasForeignKey(p => p.RoleID);
        }
    }
}
