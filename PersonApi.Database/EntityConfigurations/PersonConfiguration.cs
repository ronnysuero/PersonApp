using PersonApi.Domain.Entities;

namespace PersonApi.Database.EntityConfigurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable(nameof(Person).ToLower());
        builder.HasKey(k => k.Id);

        builder.Property(p => p.FirstName).HasMaxLength(45).HasColumnType("varchar(45)");
        builder.Property(p => p.LastName).HasMaxLength(45).HasColumnType("varchar(45)");
        builder.Property(p => p.Email).HasMaxLength(45).HasColumnType("varchar(45)");
        builder.Property(p => p.DateOfBirth).IsRequired();
    }
}
