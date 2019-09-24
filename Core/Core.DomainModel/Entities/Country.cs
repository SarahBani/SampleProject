using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.DomainModel.Entities
{
    public class Country : Entity<short>
    {

        #region Properties

        public string Name { get; set; }

        #endregion /Properties

    }

    internal class CountryEntityTypeConfiguration : IEntityTypeConfiguration<Country>
    {

        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.Property(q => q.Name)
                .IsRequired()
                .HasMaxLength(30);
        }

    }
}
