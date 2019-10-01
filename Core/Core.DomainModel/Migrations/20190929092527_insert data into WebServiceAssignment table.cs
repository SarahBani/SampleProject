using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.DomainModel.Migrations
{
    public partial class insertdataintoWebServiceAssignmenttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = @"INSERT INTO dbo.WebServiceAssignment(CompanyName, WebServiceType, Token, ValidationDate)
            VALUES ('Company1', 1, '11223344-5566-7788-99AA-BBCCDDEEFF00', '12.29.2019')";
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
