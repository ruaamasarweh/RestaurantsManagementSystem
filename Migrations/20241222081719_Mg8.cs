using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gp.Migrations
{
    /// <inheritdoc />
    public partial class Mg8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Dish_DishID",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Reservation_ReservationID",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_DishID",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_ReservationID",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "ReservationID",
                table: "Order",
                newName: "TableNumber");

            migrationBuilder.RenameColumn(
                name: "DishID",
                table: "Order",
                newName: "Quantity");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "User",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

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

            migrationBuilder.AddColumn<int>(
                name: "ID_Reservation_Dish",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsTaken",
                table: "Order",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Employee",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "DishName",
                table: "Dish",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.CreateTable(
                name: "ReservationDish",
                columns: table => new
                {
                    ID_Reservation_Dish = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Reservation = table.Column<int>(type: "int", nullable: false),
                    ID_Dish = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationDish", x => x.ID_Reservation_Dish);
                    table.ForeignKey(
                        name: "FK_ReservationDish_Dish_ID_Dish",
                        column: x => x.ID_Dish,
                        principalTable: "Dish",
                        principalColumn: "DishID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationDish_Reservation_ID_Reservation",
                        column: x => x.ID_Reservation,
                        principalTable: "Reservation",
                        principalColumn: "ReservationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Review_BranchID1",
                table: "Review",
                column: "BranchID1");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserID1",
                table: "Review",
                column: "UserID1");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ID_Reservation_Dish",
                table: "Order",
                column: "ID_Reservation_Dish");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationDish_ID_Dish",
                table: "ReservationDish",
                column: "ID_Dish");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationDish_ID_Reservation",
                table: "ReservationDish",
                column: "ID_Reservation");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_ReservationDish_ID_Reservation_Dish",
                table: "Order",
                column: "ID_Reservation_Dish",
                principalTable: "ReservationDish",
                principalColumn: "ID_Reservation_Dish",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_ReservationDish_ID_Reservation_Dish",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Branch_BranchID1",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_User_UserID1",
                table: "Review");

            migrationBuilder.DropTable(
                name: "ReservationDish");

            migrationBuilder.DropIndex(
                name: "IX_Review_BranchID1",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_UserID1",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Order_ID_Reservation_Dish",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "BranchID1",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "UserID1",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "ID_Reservation_Dish",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "IsTaken",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "TableNumber",
                table: "Order",
                newName: "ReservationID");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Order",
                newName: "DishID");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "User",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Employee",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employee",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Employee",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "DishName",
                table: "Dish",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.CreateIndex(
                name: "IX_Order_DishID",
                table: "Order",
                column: "DishID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ReservationID",
                table: "Order",
                column: "ReservationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Dish_DishID",
                table: "Order",
                column: "DishID",
                principalTable: "Dish",
                principalColumn: "DishID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Reservation_ReservationID",
                table: "Order",
                column: "ReservationID",
                principalTable: "Reservation",
                principalColumn: "ReservationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
