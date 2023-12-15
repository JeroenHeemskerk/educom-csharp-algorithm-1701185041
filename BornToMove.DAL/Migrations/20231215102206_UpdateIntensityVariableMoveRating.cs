using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BornToMove.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIntensityVariableMoveRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Vote",
                table: "MoveRating",
                newName: "Intensity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Intensity",
                table: "MoveRating",
                newName: "Vote");
        }
    }
}
