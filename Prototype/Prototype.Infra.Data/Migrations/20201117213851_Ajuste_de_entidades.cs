using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prototype.Infra.Data.Migrations
{
    public partial class Ajuste_de_entidades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tramitacao_Documentos_DocumentoId",
                table: "Tramitacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Tramitacao_Servidores_DocumentoId1",
                table: "Tramitacao");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Blokcs");

            migrationBuilder.DropTable(
                name: "Proprietary");

            migrationBuilder.DropIndex(
                name: "IX_Tramitacao_DocumentoId1",
                table: "Tramitacao");

            migrationBuilder.DropColumn(
                name: "DocumentoId1",
                table: "Tramitacao");

            migrationBuilder.DropColumn(
                name: "SetorDescricao",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "SetorTramitacao",
                table: "Documentos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data_Tramitacao",
                table: "Tramitacao",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 17, 18, 38, 51, 447, DateTimeKind.Local).AddTicks(1757),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 11, 17, 17, 25, 7, 695, DateTimeKind.Local).AddTicks(4941));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Ultima_Modificacao",
                table: "Documentos",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 17, 18, 38, 51, 438, DateTimeKind.Local).AddTicks(2590),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 11, 17, 17, 25, 7, 688, DateTimeKind.Local).AddTicks(9793));

            migrationBuilder.AddForeignKey(
                name: "FK_Tramitacao_Servidores_DocumentoId",
                table: "Tramitacao",
                column: "DocumentoId",
                principalTable: "Servidores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tramitacao_Servidores_DocumentoId",
                table: "Tramitacao");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data_Tramitacao",
                table: "Tramitacao",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 17, 17, 25, 7, 695, DateTimeKind.Local).AddTicks(4941),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 11, 17, 18, 38, 51, 447, DateTimeKind.Local).AddTicks(1757));

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentoId1",
                table: "Tramitacao",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Ultima_Modificacao",
                table: "Documentos",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 17, 17, 25, 7, 688, DateTimeKind.Local).AddTicks(9793),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 11, 17, 18, 38, 51, 438, DateTimeKind.Local).AddTicks(2590));

            migrationBuilder.AddColumn<string>(
                name: "SetorDescricao",
                table: "Documentos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SetorTramitacao",
                table: "Documentos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Blokcs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Street = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UnitQuantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blokcs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proprietary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Document = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proprietary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    BlokcId = table.Column<Guid>(type: "uuid", nullable: false),
                    BlokcId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    FloorLevel = table.Column<int>(type: "integer", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    PropietaryId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProprietaryId = table.Column<Guid>(type: "uuid", nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Units_Blokcs_BlokcId",
                        column: x => x.BlokcId,
                        principalTable: "Blokcs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Units_Blokcs_BlokcId1",
                        column: x => x.BlokcId1,
                        principalTable: "Blokcs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Units_Proprietary_PropietaryId",
                        column: x => x.PropietaryId,
                        principalTable: "Proprietary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Units_Proprietary_ProprietaryId",
                        column: x => x.ProprietaryId,
                        principalTable: "Proprietary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tramitacao_DocumentoId1",
                table: "Tramitacao",
                column: "DocumentoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Units_BlokcId",
                table: "Units",
                column: "BlokcId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_BlokcId1",
                table: "Units",
                column: "BlokcId1");

            migrationBuilder.CreateIndex(
                name: "IX_Units_PropietaryId",
                table: "Units",
                column: "PropietaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_ProprietaryId",
                table: "Units",
                column: "ProprietaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tramitacao_Documentos_DocumentoId",
                table: "Tramitacao",
                column: "DocumentoId",
                principalTable: "Documentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tramitacao_Servidores_DocumentoId1",
                table: "Tramitacao",
                column: "DocumentoId1",
                principalTable: "Servidores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
