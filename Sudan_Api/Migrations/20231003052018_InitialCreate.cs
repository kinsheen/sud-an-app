using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Sudan_Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SudanCategories",
                columns: table => new
                {
                    sudanCatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    sudanTitle = table.Column<string>(type: "longtext", nullable: false),
                    image = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SudanCategories", x => x.sudanCatId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Sudans",
                columns: table => new
                {
                    sudanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    sudanName = table.Column<string>(type: "longtext", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<double>(type: "double", nullable: false),
                    image = table.Column<string>(type: "longtext", nullable: false),
                    SudanCategorysudanCatId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sudans", x => x.sudanId);
                    table.ForeignKey(
                        name: "FK_Sudans_SudanCategories_SudanCategorysudanCatId",
                        column: x => x.SudanCategorysudanCatId,
                        principalTable: "SudanCategories",
                        principalColumn: "sudanCatId");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Sudans_SudanCategorysudanCatId",
                table: "Sudans",
                column: "SudanCategorysudanCatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sudans");

            migrationBuilder.DropTable(
                name: "SudanCategories");
        }
    }
}
