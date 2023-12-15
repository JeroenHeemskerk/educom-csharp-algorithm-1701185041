using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BornToMove.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoveRatings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MoveRating",
                columns: new[] { "Id", "Intensity", "MoveId", "Rating" },
                values: new object[,]
                {
                    { 1, 5.0, 1, 5.0 },
                    { 2, 4.0, 2, 4.0 },
                    { 3, 3.0, 3, 5.0 },
                    { 4, 5.0, 1, 1.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MoveRating",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MoveRating",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MoveRating",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MoveRating",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
