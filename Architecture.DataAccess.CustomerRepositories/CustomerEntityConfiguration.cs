using Architecture.DataAccess.CustomerEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Architecture.DataAccess.CustomerRepositories
{
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");


            builder.HasKey(c => c.Id);
            builder.Property(c => c.Guid)
                        .HasColumnName("guid")
                        .ValueGeneratedOnAdd();
            builder.HasIndex(c => c.Guid).IsUnique();

            builder.Property(c => c.FirstName)
                    .HasColumnName("first_name")
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(64);
            builder.Property(c => c.Initials)
                    .HasColumnName("initials")
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(8);
            builder.Property(c => c.LastName)
                    .HasColumnName("last_name")
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(64);
        }
    }
}
