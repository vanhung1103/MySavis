using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Assignment.Models;
namespace Assignment.Configurations
{
    public class CartDetailsConfiguration : IEntityTypeConfiguration<CartDetail>
    {
        public void Configure(EntityTypeBuilder<CartDetail> builder)
        {
            builder.ToTable("GioHangChiTiet"); // Đặt tên bảng
            builder.HasKey(p => p.ID); // Set khóa chính
            // Cấu hình cho thuộc tính
            builder.Property(p => p.Quantity).HasColumnType("int");
            builder.Property(p => p.Size).HasColumnType("int").IsRequired(); // int not null
            builder.Property(p => p.Color).HasColumnType("nvarchar(100)").IsRequired(); // int not null
            builder.Property(p => p.Image).HasColumnType("nvarchar(100)");
            builder.Property(p => p.Type).HasColumnType("nvarchar(100)");
            builder.HasOne(p => p.Cart).WithMany(p=>p.CartDetails).
                HasForeignKey(p => p.UserID);
            builder.HasOne(p => p.Product).WithMany(p=>p.CartDetails).
                HasForeignKey(p => p.IDSP);
        }
    }
}
