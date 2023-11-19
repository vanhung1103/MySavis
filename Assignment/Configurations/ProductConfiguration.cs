using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Assignment.Models;
namespace Assignment.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("SanPham"); // Đặt tên bảng
            builder.HasKey(p => p.ID); // Set khóa chính
            // Cấu hình cho thuộc tính
            builder.Property(p => p.Name).HasColumnType("nvarchar(100)");
            builder.Property(p => p.Price).HasColumnType("int");
            builder.Property(p => p.AvailableQuantity).HasColumnType("int");
            builder.Property(p => p.Status).HasColumnType("int");
            builder.Property(p => p.Size).HasColumnType("int");
            builder.Property(p => p.Color).HasColumnType("nvarchar(100)");
            builder.Property(p => p.Image).HasColumnType("nvarchar(100)");
            builder.Property(p => p.Image1).HasColumnType("nvarchar(100)");
            builder.Property(p => p.Image2).HasColumnType("nvarchar(100)");
            builder.Property(p => p.Image3).HasColumnType("nvarchar(100)");
            builder.Property(p => p.Image4).HasColumnType("nvarchar(100)");
            builder.Property(p => p.Image5).HasColumnType("nvarchar(100)");
            builder.Property(p => p.Type).HasColumnType("nvarchar(100)");
            builder.Property(p => p.Supplier).HasColumnType("nvarchar(100)");
            builder.Property(p => p.Description).HasColumnType("nvarchar(1000)");
        }
    }
}
