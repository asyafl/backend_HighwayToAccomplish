using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL_HWTA.Migrations
{
    public partial class ChangeUserGoals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyUserGoal");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "UserGoals",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDate",
                table: "UserGoals",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Progress",
                table: "UserGoals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Regularity",
                table: "UserGoals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "UserGoals",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "UserGoals");

            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                table: "UserGoals");

            migrationBuilder.DropColumn(
                name: "Progress",
                table: "UserGoals");

            migrationBuilder.DropColumn(
                name: "Regularity",
                table: "UserGoals");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "UserGoals");

            migrationBuilder.CreateTable(
                name: "DailyUserGoal",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CheckActivity = table.Column<bool>(type: "INTEGER", nullable: false),
                    CheckDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserGoalId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyUserGoal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyUserGoal_UserGoals_UserGoalId",
                        column: x => x.UserGoalId,
                        principalTable: "UserGoals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyUserGoal_UserGoalId",
                table: "DailyUserGoal",
                column: "UserGoalId");
        }
    }
}
