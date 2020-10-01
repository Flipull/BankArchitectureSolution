using Architecture.DataAccess.BankEntities;
using Architecture.DataAccess.CustomerEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Architecture.DataAccess.BankRepositories
{
    public class BankAccountEntityConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.ToTable("Accounts");


            builder.HasKey(a => a.Id);
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

            builder.HasOne<Customer>()
                    .WithMany().HasForeignKey(a => a.OwnerId)
                   .IsRequired()//no navigational properties at all; only id
                   .OnDelete(DeleteBehavior.Restrict);

            /*
            builder.HasOne(a => a.Owner)
                   .WithMany()//no navigational property present
                                //in customer-class (ofcourse)
                   .HasForeignKey(c => c.Id)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);
            */
        }
    }
}
