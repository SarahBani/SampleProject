using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Core.DomainModel.Entities
{
    public class SampleDataBaseContext : DbContext
    //IdentityDbContext<User, Role, int>
    {

        #region Properties

        public DbSet<Bank> Banks { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Country> Countries { get; set; }

        #endregion /Properties

        #region Constructors

        public SampleDataBaseContext(DbContextOptions<SampleDataBaseContext> options)
        : base(options)
        {

        }

        #endregion /Constructors

        #region Methods

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(Utility.GetConnectionString());
        //        //"Provider=SQLOLEDB.1;Password=sa123;Persist Security Info=True;User ID=sa;Initial Catalog=MyDataBase;Data Source=."
        //        //"Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;User ID=sa;Initial Catalog=MyDataBase;Data Source=."
        //        //optionsBuilder.UseSqlServer(@"Server=.;Database=MyDataBase;Trusted_Connection=True;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // after adding Identity this line is mandatory

            //This will singularize all table names
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.Relational().TableName = entityType.DisplayName();
            }

            modelBuilder.ApplyConfiguration(new CountryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BankEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BranchEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguration());

            //modelBuilder.Ignore<IdentityUserLogin<int>>();
            //modelBuilder.Ignore<IdentityUserToken<int>>();
            //modelBuilder.Ignore<IdentityRoleClaim<int>>();

            modelBuilder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.ToTable("UserRole");
            });
            modelBuilder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.ToTable("UserLogin");
            });
            modelBuilder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.ToTable("UserToken");
            });
            modelBuilder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("UserClaim");
            });
            modelBuilder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("RoleClaim");
            });

            //modelBuilder.Entity<Bank>(entity =>
            //{
            //    entity.Property(q => q.Name)
            //        .IsRequired()
            //        .HasMaxLength(40);

            //    entity.Property(q => q.Grade)
            //        .HasColumnType("tinyint");

            //    //.HasDefaultValueSql("getdate()");
            //});

            //modelBuilder.Entity<Branch>(entity =>
            //{
            //    entity.Property(q => q.Code)
            //        .IsRequired();

            //    entity.Property(q => q.Name)
            //        .IsRequired()
            //        .HasMaxLength(60);

            //    entity.Property(q => q.Address)
            //        .HasMaxLength(200);

            //    entity.HasOne(q => q.Bank)
            //        .WithMany(p => p.Branches)
            //        .HasForeignKey(q => q.BankId)
            //        .HasConstraintName("FK_Branch_Bank");
            //});

            //modelBuilder.Entity<Country>(entity =>
            //{
            //    entity.Property(q => q.Name)
            //        .IsRequired()
            //        .HasMaxLength(30);
            //});
        }

        #endregion /Methods

    }

    internal class BankEntityTypeConfiguration : IEntityTypeConfiguration<Bank>
    {
        public void Configure(EntityTypeBuilder<Bank> builder)
        {
            builder.Property(q => q.Name)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(q => q.Grade)
                .HasColumnType("tinyint");

            //.HasDefaultValueSql("getdate()");
        }
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

    internal class CountryEntityTypeConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.Property(q => q.Name)
                .IsRequired()
                .HasMaxLength(30);
        }
    }

    internal class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
        }
    }

    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
        }
    }

}