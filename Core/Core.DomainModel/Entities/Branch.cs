using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.DomainModel.Entities
{
    public class Branch : Entity<int>, IAggregateRoot
    {

        #region Properties

        public int BankId { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }

        public virtual Bank Bank { get; set; }

        #endregion /Properties

        #region Constructors

        private Branch()
        {

        }

        public Branch(int id, int bankId, int code, string name, Address address)
        {
            this.Id = id;
            this.BankId = bankId;
            this.Code = code;
            this.Name = name;
            this.Address = address;
        }

        #endregion /Constructors

        #region Methods

        public void SetAddress(Address address)
        {
            if (string.IsNullOrEmpty(address.CityName))
            {
                throw new CustomException(ExceptionKey.Invalid_Address_City_Required);
            }
            /// & so on 
            this.Address = address;
        }

        #endregion /Methods

    }

    internal class BranchEntityTypeConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.Property(q => q.Code)
                      .IsRequired();

            builder.Property(q => q.Name)
                .IsRequired()
                .HasMaxLength(60);

            //Address value object persisted as owned entity in EF Core 2.0
            /// By default, EF Core conventions name the database columns for the properties of the
            /// owned entity type as EntityProperty_OwnedEntityProperty -- it didn't!!!!, maybe in the past
            builder.OwnsOne(o => o.Address, q =>
            {
                q.Property(p => p.CityName)
                    .HasColumnName("ShippingCity")
                    .IsRequired()
                    .HasMaxLength(60);
                q.Property(p => p.Street)
                    .HasColumnName("ShippingStreet")
                    .IsRequired()
                    .HasMaxLength(200);
                q.Property(p => p.BlockNo)
                    .HasColumnName("ShippingBlockNo")
                    .IsRequired()
                    .HasMaxLength(20);
                q.Property(p => p.PostalCode)
                    .HasColumnName("PostalCode")
                    .HasMaxLength(10);

                //q.ToTable("OrderAddress"); // Storing owned type in a specific table
            });

            builder.HasOne(q => q.Bank)
                .WithMany(q => q.Branches)
                .HasForeignKey(q => q.BankId)
                .HasConstraintName("FK_Branch_Bank");
        }
    }
}