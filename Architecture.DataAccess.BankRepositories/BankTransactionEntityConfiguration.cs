using Architecture.DataAccess.BankEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Architecture.DataAccess.BankRepositories
{
    public class BankTransactionEntityConfiguration : IEntityTypeConfiguration<BankTransaction>
    {
        public void Configure(EntityTypeBuilder<BankTransaction> builder)
        {
            builder.ToTable("Transactions");


            builder.Property(t => t.Guid)
                            .HasColumnName("guid")
                            .ValueGeneratedOnAdd();
            builder.Property(t => t.IbanSource)
                            .HasColumnName("iban_source")
                            .HasMaxLength(32);
            builder.Property(t => t.IbanTarget)
                            .HasColumnName("iban_target")
                            .HasMaxLength(32);
            builder.Property(t => t.Value)
                            .HasColumnName("value");
            builder.Property(t => t.PointInTime)
                            .HasColumnName("pit")
                            .ValueGeneratedOnAdd();

            builder.HasIndex(t => t.Guid);
            builder.HasIndex(t => t.IbanSource);
            builder.HasIndex(t => t.IbanTarget);
            builder.HasIndex(t => t.PointInTime);
        }
    }
}
