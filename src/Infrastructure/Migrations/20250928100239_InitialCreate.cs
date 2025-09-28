using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _132ndWebsite.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Squadrons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Callsign = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Squadrons", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Squadrons",
                columns: new[] { "Id", "Callsign", "Name" },
                values: new object[,]
                {
                    { 1, "The Panthers", "494th vFighter Squadron" },
                    { 2, "The Peregrines", "388th Fighter Squadron" },
                    { 3, "Ravens", "335th Special Operations Squadron" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Squadrons");
        }
    }
}
