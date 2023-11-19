using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Assignment.Models;
namespace Assignment.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("GioHang"); // Đặt tên bảng
            builder.HasKey(p => p.UserID); // Set khóa chính
            // Cấu hình cho thuộc tính
            builder.Property(p => p.Description).HasColumnType("nvarchar(1000)");
            builder.HasOne(p => p.User).WithOne();

        }
    }
}
