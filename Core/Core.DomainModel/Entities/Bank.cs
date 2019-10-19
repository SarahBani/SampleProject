using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DomainModel.Entities
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class Bank : Entity<int>
    {

        #region Properties

        [Required]
        public string Name { get; set; }

        public Grade? Grade { get; set; }

        public ICollection<Branch> Branches { get; set; }

        #endregion /Properties

        #region Constructors

        public Bank()
        {

        }

        #endregion /Constructors

    }

    internal class BankEntityTypeConfiguration : IEntityTypeConfiguration<Bank>
    {
        public void Configure(EntityTypeBuilder<Bank> builder)
        {
            //builder.HasKey(q => q.Id);

            builder.Property(q => q.Id)
                .UseSqlServerIdentityColumn()
                .Metadata.BeforeSaveBehavior = PropertySaveBehavior.Ignore; // make the column Identity        
            //    .ValueGeneratedOnAdd(); // make the key identity            
            //    .ValueGeneratedNever(); // don't make the key identity    

            builder.HasIndex(q => q.Name)
                .IsUnique();

            builder.Property(q => q.Name)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(q => q.Grade)
                .HasColumnType("tinyint");

            //.HasDefaultValueSql("getdate()");
        }
    }
}
