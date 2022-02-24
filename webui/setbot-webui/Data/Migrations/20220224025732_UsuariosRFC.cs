using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace setbot_webui.Data.Migrations
{
    public partial class UsuariosRFC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaRegistro",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UltimoAcceso",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ConfiguracionExtraccionRFC",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    RFC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaInicial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExtraerMetadatosEmisor = table.Column<bool>(type: "bit", nullable: false),
                    ExtraerMetadatosReceptor = table.Column<bool>(type: "bit", nullable: false),
                    ExtraerXMLEmisor = table.Column<bool>(type: "bit", nullable: false),
                    ExtraerXMLReceptor = table.Column<bool>(type: "bit", nullable: false),
                    ExtraerPDFReceptor = table.Column<bool>(type: "bit", nullable: false),
                    ExtraerPDFEmisor = table.Column<bool>(type: "bit", nullable: false),
                    ExtraerCanceladosReceptor = table.Column<bool>(type: "bit", nullable: false),
                    ExtraerCanceladosEmisor = table.Column<bool>(type: "bit", nullable: false),
                    Habilitada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracionExtraccionRFC", x => new { x.UsuarioId, x.RFC });
                    table.ForeignKey(
                        name: "FK_ConfiguracionExtraccionRFC_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PluginRFC",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    RFC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PluginId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PluginRFC", x => new { x.UsuarioId, x.RFC, x.PluginId });
                    table.ForeignKey(
                        name: "FK_PluginRFC_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioRFC",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    RFC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Denominacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Validado = table.Column<bool>(type: "bit", nullable: false),
                    EnEvaluacion = table.Column<bool>(type: "bit", nullable: false),
                    InicioEvaluacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinEvaluacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CertificadoValidoHasta = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaximoCFDIS = table.Column<int>(type: "int", nullable: false),
                    SecretoId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRFC", x => new { x.UsuarioId, x.RFC });
                    table.ForeignKey(
                        name: "FK_UsuarioRFC_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfiguracionExtraccionRFC");

            migrationBuilder.DropTable(
                name: "PluginRFC");

            migrationBuilder.DropTable(
                name: "UsuarioRFC");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FechaRegistro",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UltimoAcceso",
                table: "AspNetUsers");
        }
    }
}
