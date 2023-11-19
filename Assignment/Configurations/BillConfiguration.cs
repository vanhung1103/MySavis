using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Assignment.Models;
namespace Assignment.Configurations
{
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("HoaDon"); // Đặt tên bảng
            builder.HasKey(p => p.ID); // Set khóa chính
            // Cấu hình cho thuộc tính
            builder.Property(p => p.CreatDate).HasColumnType("Datetime");
            builder.Property(p => p.Status).HasColumnType("int").
                IsRequired(); // int not null
            builder.Property(p => p.TotalAmout).HasColumnType("int").IsRequired();
            builder.Property(p => p.ShippingFee).HasColumnType("int").IsRequired();
            builder.Property(p => p.RecipientName).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(p => p.RecipientAddress).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(p => p.RecipientPhoneNumber).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(p => p.Discount).HasColumnType("float").IsRequired();
            builder.HasOne(p => p.User).WithMany(p => p.Bills).
                HasForeignKey(p => p.UserID);
        }
    }
}
