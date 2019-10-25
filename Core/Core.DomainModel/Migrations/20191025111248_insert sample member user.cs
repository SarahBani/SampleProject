using Core.DomainModel.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Transactions;

namespace Core.DomainModel.Migrations
{
    public partial class insertsamplememberuser : Migration
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
                    UserName = "User",
                    NormalizedUserName = "USER",
                    Email = "User@gmail.com",
                    NormalizedEmail = "USER@GAMIL.COM",
                    PhoneNumber = null,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = false,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    LockoutEnabled = true,
                };
                string password = "Test@123";
                var hashed = new PasswordHasher<User>().HashPassword(user, password);
                user.PasswordHash = hashed;
                var result = userStore.CreateAsync(user).Result;
                userStore.AddToRoleAsync(user, RoleEnum.Member.ToString()).Wait();
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.System,"crud"),
                };
                userManager.AddClaimsAsync(user, claims).Wait();

                dbContext.SaveChanges();
                scope.Complete();
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
