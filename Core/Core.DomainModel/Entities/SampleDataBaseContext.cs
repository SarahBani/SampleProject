using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Core.DomainModel.Entities
{
    public class SampleDataBaseContext : DbContext// IdentityDbContext<User, Role, int>
    {

        #region Properties

        public DbSet<Bank> Banks { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<WebServiceAssignment> WebServiceAssignments { get; set; }

        #endregion /Properties

        #region Constructors

        public SampleDataBaseContext(DbContextOptions<SampleDataBaseContext> options)
        : base(options)
        {

        }

        #endregion /Constructors

        #region Methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(Utility.GetConnectionString());
                optionsBuilder.UseSqlServer(@"Server=.;Database=SampleDataBase;Trusted_Connection=True;");
            }
        }

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
            modelBuilder.ApplyConfiguration(new WebServiceAssignmentEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguration());

            //modelBuilder.Ignore<IdentityUserLogin<int>>();
            //modelBuilder.Ignore<IdentityUserToken<int>>();
            //modelBuilder.Ignore<IdentityRoleClaim<int>>();

            //modelBuilder.Entity<IdentityUserRole<int>>(entity =>
            //{
            //    entity.ToTable("UserRole");
            //});
            //modelBuilder.Entity<IdentityUserLogin<int>>(entity =>
            //{
            //    entity.ToTable("UserLogin");
            //});
            //modelBuilder.Entity<IdentityUserToken<int>>(entity =>
            //{
            //    entity.ToTable("UserToken");
            //});
            //modelBuilder.Entity<IdentityUserClaim<int>>(entity =>
            //{
            //    entity.ToTable("UserClaim");
            //});
            //modelBuilder.Entity<IdentityRoleClaim<int>>(entity =>
            //{
            //    entity.ToTable("RoleClaim");
            //});
        }

        public virtual void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        #endregion /Methods

    }
}