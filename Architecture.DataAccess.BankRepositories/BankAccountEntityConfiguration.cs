using Architecture.DataAccess.BankEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Architecture.DataAccess.BankRepositories
{
    public class BankAccountEntityConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.ToTable("Accounts");


            builder.HasKey(a => a.Id).HasName("id");
            builder.Property(a => a.Guid)
                            .HasColumnName("guid")
                            .ValueGeneratedOnAdd();
            builder.HasIndex(a => a.Guid).IsUnique();

            /*
            will be able to validate:

            - Country code
            - Number of characters in the IBAN correspond to the number specified for the country code
            - BBAN format specified for the country code
            -Account number, bank code and country code combination is compatible with the check digits
            */
            builder.Property(a => a.Iban)
                    .HasColumnName("iban")
                    .IsRequired()
                    .HasMaxLength(32);
            builder.Property(a => a.Worth)
                    .HasColumnName("worth")
                    .IsRequired();

            builder.HasOne(a => a.Owner).WithMany()
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
