using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.DomainModel.Migrations
{
    public partial class IndexandUniquekey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Branch_BankId",
                table: "Branch");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Branch_BankId_Name",
                table: "Branch",
                columns: new[] { "BankId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_Bank_Name",
                table: "Bank",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Branch_BankId_Name",
                table: "Branch");

            migrationBuilder.DropIndex(
                name: "IX_Bank_Name",
                table: "Bank");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_BankId",
                table: "Branch",
                column: "BankId");
        }
    }
}
