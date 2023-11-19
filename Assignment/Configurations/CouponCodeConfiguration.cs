using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assignment.Configurations
{
    public class CouponCodeConfiguration : IEntityTypeConfiguration<CouponCode>
    {
        public void Configure(EntityTypeBuilder<CouponCode> builder)
        {
            builder.ToTable("MaGiamGia"); // Đặt tên bảng
            builder.HasKey(p => p.ID); // Set khóa chính
            // Cấu hình cho thuộc tính
            builder.Property(p => p.CouponCodes).HasColumnType("nvarchar(100)");
            builder.Property(p => p.Status).HasColumnType("int").
                IsRequired(); // int not null
            builder.Property(p => p.CouponValue).HasColumnType("int").IsRequired();
        }
    }
}
