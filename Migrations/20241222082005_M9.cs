using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gp.Migrations
{
    /// <inheritdoc />
    public partial class M9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Branch_BranchID1",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_User_UserID1",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_BranchID1",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_UserID1",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "BranchID1",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "UserID1",
                table: "Review");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchID1",
                table: "Review",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserID1",
                table: "Review",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Review_BranchID1",
                table: "Review",
                column: "BranchID1");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserID1",
                table: "Review",
                column: "UserID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Branch_BranchID1",
                table: "Review",
                column: "BranchID1",
                principalTable: "Branch",
                principalColumn: "BranchID");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_User_UserID1",
                table: "Review",
                column: "UserID1",
                principalTable: "User",
                principalColumn: "UserID");
        }
    }
}
