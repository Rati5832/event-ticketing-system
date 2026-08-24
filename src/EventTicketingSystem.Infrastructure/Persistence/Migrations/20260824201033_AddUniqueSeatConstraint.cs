using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventTicketingSystem.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueSeatConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Seats_VenueId",
                table: "Seats");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_VenueId_Section_Row_Number",
                table: "Seats",
                columns: new[] { "VenueId", "Section", "Row", "Number" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Seats_VenueId_Section_Row_Number",
                table: "Seats");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_VenueId",
                table: "Seats",
                column: "VenueId");
        }
    }
}
