using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyecto_TBD.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.CreateTable(
                name: "instituciones",
                columns: table => new
                {
                    id_instituto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    telefono = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    direccion = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    descripcion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    ID_usuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_instituto);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "productos",
                columns: table => new
                {
                    id_producto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    descripción = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    peso_Aprox = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    ID_usuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_producto);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    telefono = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    nombre_empresa = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    correo = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_usuario);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "donativos",
                columns: table => new
                {
                    iddonativos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_usuario = table.Column<int>(type: "int", nullable: true),
                    id_producto = table.Column<int>(type: "int", nullable: true),
                    id_instituto = table.Column<int>(type: "int", nullable: true),
                    fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: true),
                    descripcion = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.iddonativos);
                    table.ForeignKey(
                        name: "idinstituciones",
                        column: x => x.id_instituto,
                        principalTable: "instituciones",
                        principalColumn: "id_instituto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "idproductos",
                        column: x => x.id_producto,
                        principalTable: "productos",
                        principalColumn: "id_producto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "idusuario",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateIndex(
                name: "idinstituciones_idx",
                table: "donativos",
                column: "id_instituto");

            migrationBuilder.CreateIndex(
                name: "idproductos_idx",
                table: "donativos",
                column: "id_producto");

            migrationBuilder.CreateIndex(
                name: "idusuario_idx",
                table: "donativos",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "correo_UNIQUE",
                table: "usuario",
                column: "correo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "donativos");

            migrationBuilder.DropTable(
                name: "instituciones");

            migrationBuilder.DropTable(
                name: "productos");

            migrationBuilder.DropTable(
                name: "usuario");
        }
    }
}
