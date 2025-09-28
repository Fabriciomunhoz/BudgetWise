using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpFinance.Migrations
{
    /// <inheritdoc />
    public partial class DadosComplementares : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsuarioDadosComplementares",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_USUARIO = table.Column<int>(type: "int", nullable: false),
                    SALARIO = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioDadosComplementares", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UsuarioDadosComplementares_Usuario_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "Usuario",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioDadosComplementares_ID_USUARIO",
                table: "UsuarioDadosComplementares",
                column: "ID_USUARIO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioDadosComplementares");
        }
    }
}
