using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.IO;

namespace Core.DomainModel.Migrations
{
    public partial class InsertdataintoBanktable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = @"
INSERT INTO dbo.Bank(Name, Grade)
SELECT 'Bank1' , 1
UNION
SELECT 'Bank2' , 3
UNION
SELECT 'Bank3' , 1
UNION
SELECT 'Bank4' , 2
";
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
