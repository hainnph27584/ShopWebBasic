using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SellerProduct.Models;
namespace SellerProduct.Configurations
{
    public class BilDetailsConfiguration : IEntityTypeConfiguration<BillDetails>
    {
        public void Configure(EntityTypeBuilder<BillDetails> builder)
        {
            builder.ToTable("BillDetails"); // Đặt tên bảng
            //builder.HasKey(p=>p.Id);// Set khóa chính
            builder.HasKey(p => new { p.ID }); // thiết lập khóa chính
            // Set Khóa Ngoại
            builder.HasOne(p => p.Bill).WithMany(p=>p.BillDetails).HasForeignKey(p=>p.IdHD).HasConstraintName("FK_HD");
            builder.HasOne(p => p.Product).WithMany(p=>p.BillDetails).HasForeignKey(p=>p.IdSP).HasConstraintName("FK_SP");
           
        }
    }
}
