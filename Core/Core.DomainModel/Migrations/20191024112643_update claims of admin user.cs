using Core.DomainModel.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;
using System.Security.Claims;
using System.Transactions;

namespace Core.DomainModel.Migrations
{
    public partial class updateclaimsofadminuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var dbContext = new SampleDataBaseContext(new DbContextOptions<SampleDataBaseContext>());
                var userStore = new CustomUserStore(dbContext);
                var userManager = new UserManager<User>(userStore, null, null, null, null, null, null, null, null);
                var user = userManager.FindByNameAsync("Sarah").Result;
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.System,"auth;crud;cqrs"),
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
