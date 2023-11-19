using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Assignment.Models;
namespace Assignment.Configurations
{
    public class BillDetailsConfiguration : IEntityTypeConfiguration<BillDetail>
    {
        public void Configure(EntityTypeBuilder<BillDetail> builder)
        {
            builder.ToTable("HoaDonChiTiet"); // Đặt tên bảng
            builder.HasKey(p => p.ID); // Set khóa chính
            // Cấu hình cho thuộc tính
            builder.Property(p => p.Quantity).HasColumnType("int");
            builder.Property(p => p.Price).HasColumnType("int").
                IsRequired(); // int not null
            builder.Property(p => p.Image).HasColumnType("nvarchar(100)");
            builder.Property(p => p.Type).HasColumnType("nvarchar(100)");
            builder.Property(p => p.Size).HasColumnType("int").
    IsRequired(); // int not null
            builder.Property(p => p.Color).HasColumnType("nvarchar(100)").
    IsRequired(); // int not null
            builder.HasOne(p => p.Bill).WithMany(p=>p.BillDetails).
                HasForeignKey(p => p.IDHD);
            builder.HasOne(p => p.Product).WithMany(p => p.BillDetails).
                HasForeignKey(p => p.IDSP);
        }
    }
}
