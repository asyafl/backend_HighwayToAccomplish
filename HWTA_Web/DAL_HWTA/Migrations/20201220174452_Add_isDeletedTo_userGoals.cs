using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL_HWTA.Migrations
{
    public partial class Add_isDeletedTo_userGoals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "UserGoals",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserGoals",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "UserGoals");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserGoals");
        }
    }
}
