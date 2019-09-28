using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Core.DomainModel.Entities
{
    public enum WebServiceType
    {
        Admin,
        ReadOnly
    }

    public class WebServiceAssignment : Entity<short>
    {

        #region Properties

        public string CompanyName { get; set; }

        public WebServiceType WebServiceType { get; set; }

        public Guid Token { get; set; }

        public DateTime ValidationDate { get; set; }

        #endregion /Properties

        #region Constructors

        public WebServiceAssignment()
        {
        }

        #endregion /Constructors

    }

    internal class WebServiceAssignmentEntityTypeConfiguration : IEntityTypeConfiguration<WebServiceAssignment>
    {
        public void Configure(EntityTypeBuilder<WebServiceAssignment> builder)
        {
            builder.Property(q => q.CompanyName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(q => q.WebServiceType)
                .HasColumnType("tinyint");

            builder.Property(q => q.Token)
                .HasDefaultValue(Guid.Empty);
        }
    }
}
