using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BornToMove.DAL.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Move",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(400)", nullable: false),
                    SweatRate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Move", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoveRating",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MoveId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Vote = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveRating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoveRating_Move_MoveId",
                        column: x => x.MoveId,
                        principalTable: "Move",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoveRating_MoveId",
                table: "MoveRating",
                column: "MoveId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoveRating");

            migrationBuilder.DropTable(
                name: "Move");
        }
    }
}
