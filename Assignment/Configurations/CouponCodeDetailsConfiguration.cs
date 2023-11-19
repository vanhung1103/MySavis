using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assignment.Configurations
{
    public class CouponCodeDetailsConfiguration: IEntityTypeConfiguration<CouponCodeDetails>
    {
        public void Configure(EntityTypeBuilder<CouponCodeDetails> builder)
        {
            builder.ToTable("MaGiamGiaChiTiet"); // Đặt tên bảng
            builder.HasKey(p => p.ID); // Set khóa chính
            // Cấu hình cho thuộc tính
            builder.Property(p => p.CouponValue).HasColumnType("int");
            builder.HasOne(p => p.CouponCode).WithMany(p => p.CouponCodeDetails).HasForeignKey(p => p.CouponCodeID);
            builder.HasOne(p => p.Product).WithMany(p => p.CouponCodeDetails).HasForeignKey(p => p.ProductID);
        }
    }
}
