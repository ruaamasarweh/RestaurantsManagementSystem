using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gp.Migrations
{
    /// <inheritdoc />
    public partial class Mg1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeType",
                columns: table => new
                {
                    EmployeeTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeType", x => x.EmployeeTypeID);
                });

            migrationBuilder.CreateTable(
                name: "ReservationStatus",
                columns: table => new
                {
                    ReservationStatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationnStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationStatus", x => x.ReservationStatusID);
                });

            migrationBuilder.CreateTable(
                name: "UserType",
                columns: table => new
                {
                    UserTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.UserTypeID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PhoneNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    UserTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_User_UserType_UserTypeID",
                        column: x => x.UserTypeID,
                        principalTable: "UserType",
                        principalColumn: "UserTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Restaurant",
                columns: table => new
                {
                    RestaurantID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ImageFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurant", x => x.RestaurantID);
                    table.ForeignKey(
                        name: "FK_Restaurant_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    BranchID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    ImageFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpenTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CloseTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    HasIndoorSeating = table.Column<bool>(type: "bit", nullable: false),
                    HasOutdoorSeating = table.Column<bool>(type: "bit", nullable: false),
                    NumOfTables = table.Column<int>(type: "int", nullable: false),
                    RestaurantID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.BranchID);
                    table.ForeignKey(
                        name: "FK_Branch_Restaurant_RestaurantID",
                        column: x => x.RestaurantID,
                        principalTable: "Restaurant",
                        principalColumn: "RestaurantID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DishCategory",
                columns: table => new
                {
                    DishCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RestaurantID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishCategory", x => x.DishCategoryID);
                    table.ForeignKey(
                        name: "FK_DishCategory_Restaurant_RestaurantID",
                        column: x => x.RestaurantID,
                        principalTable: "Restaurant",
                        principalColumn: "RestaurantID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PhoneNum = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    EmployeeTypeID = table.Column<int>(type: "int", nullable: false),
                    BranchID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_Employee_Branch_BranchID",
                        column: x => x.BranchID,
                        principalTable: "Branch",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employee_EmployeeType_EmployeeTypeID",
                        column: x => x.EmployeeTypeID,
                        principalTable: "EmployeeType",
                        principalColumn: "EmployeeTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    ReservationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumOfPeople = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ReservationStatusID = table.Column<int>(type: "int", nullable: false),
                    BranchID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.ReservationID);
                    table.ForeignKey(
                        name: "FK_Reservation_Branch_BranchID",
                        column: x => x.BranchID,
                        principalTable: "Branch",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Reservation_ReservationStatus_ReservationStatusID",
                        column: x => x.ReservationStatusID,
                        principalTable: "ReservationStatus",
                        principalColumn: "ReservationStatusID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Reservation_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Dish",
                columns: table => new
                {
                    DishID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DishName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ImageFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    DishCategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dish", x => x.DishID);
                    table.ForeignKey(
                        name: "FK_Dish_DishCategory_DishCategoryID",
                        column: x => x.DishCategoryID,
                        principalTable: "DishCategory",
                        principalColumn: "DishCategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Billing",
                columns: table => new
                {
                    BillingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillingType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ReservationID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billing", x => x.BillingID);
                    table.ForeignKey(
                        name: "FK_Billing_Reservation_ReservationID",
                        column: x => x.ReservationID,
                        principalTable: "Reservation",
                        principalColumn: "ReservationID");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationID = table.Column<int>(type: "int", nullable: false),
                    DishID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Order_Dish_DishID",
                        column: x => x.DishID,
                        principalTable: "Dish",
                        principalColumn: "DishID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Reservation_ReservationID",
                        column: x => x.ReservationID,
                        principalTable: "Reservation",
                        principalColumn: "ReservationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Billing_ReservationID",
                table: "Billing",
                column: "ReservationID");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_RestaurantID",
                table: "Branch",
                column: "RestaurantID");

            migrationBuilder.CreateIndex(
                name: "IX_Dish_DishCategoryID",
                table: "Dish",
                column: "DishCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_DishCategory_RestaurantID",
                table: "DishCategory",
                column: "RestaurantID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_BranchID",
                table: "Employee",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_EmployeeTypeID",
                table: "Employee",
                column: "EmployeeTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_PhoneNum",
                table: "Employee",
                column: "PhoneNum",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_DishID",
                table: "Order",
                column: "DishID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ReservationID",
                table: "Order",
                column: "ReservationID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_BranchID",
                table: "Reservation",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ReservationStatusID",
                table: "Reservation",
                column: "ReservationStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_UserID",
                table: "Reservation",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationStatus_ReservationnStatus",
                table: "ReservationStatus",
                column: "ReservationnStatus",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_RestaurantName",
                table: "Restaurant",
                column: "RestaurantName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_UserID",
                table: "Restaurant",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                table: "User",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserTypeID",
                table: "User",
                column: "UserTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Billing");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "EmployeeType");

            migrationBuilder.DropTable(
                name: "Dish");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "DishCategory");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "ReservationStatus");

            migrationBuilder.DropTable(
                name: "Restaurant");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserType");
        }
    }
}
