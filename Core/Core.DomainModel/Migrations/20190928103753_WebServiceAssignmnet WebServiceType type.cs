using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.DomainModel.Migrations
{
    public partial class WebServiceAssignmnetWebServiceTypetype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Token",
                table: "WebServiceAssignment",
                nullable: false,
                defaultValue: new Guid("3f99011e-dccf-40a5-8e64-cb88af933016"),
                oldClrType: typeof(Guid),
                oldDefaultValue: new Guid("81d7bb59-34ac-4454-836c-1f355d9f70e7"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Token",
                table: "WebServiceAssignment",
                nullable: false,
                defaultValue: new Guid("81d7bb59-34ac-4454-836c-1f355d9f70e7"),
                oldClrType: typeof(Guid),
                oldDefaultValue: new Guid("3f99011e-dccf-40a5-8e64-cb88af933016"));
        }
    }
}
