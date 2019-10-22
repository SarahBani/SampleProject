using Core.DomainModel.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Transactions;

namespace Core.DomainModel.Migrations
{
    public partial class adddataintoUsertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var dbContext = new SampleDataBaseContext(new DbContextOptions<SampleDataBaseContext>());
                var userStore = new CustomUserStore(dbContext);
                var userManager = new UserManager<User>(userStore, null, null, null, null, null, null, null, null);
                var user = new User
                {
                    UserName = "Sarah",
                    NormalizedUserName = "SARAH",
                    Email = "Sarah_Bani@yahoo.com",
                    NormalizedEmail = "SARAH_BANI@YAHOO.COM",
                    PhoneNumber = "+111111111111",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    LockoutEnabled = true,
                };
                string password = "Test@123";
                //var result = userManager.CreateAsync(user, password).Result;
                var hashed = new PasswordHasher<User>().HashPassword(user, password);
                user.PasswordHash = hashed;
                var result = userStore.CreateAsync(user).Result;
                 userStore.AddToRoleAsync(user, "Admin").Wait();

                dbContext.SaveChanges();
                scope.Complete();
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
