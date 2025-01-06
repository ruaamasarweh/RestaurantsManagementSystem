using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gp.Migrations
{
    /// <inheritdoc />
    public partial class Mg10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Billing");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Billing",
                columns: table => new
                {
                    BillingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationID = table.Column<int>(type: "int", nullable: true),
                    BillingType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Billing_ReservationID",
                table: "Billing",
                column: "ReservationID");
        }
    }
}
