using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prototype.Infra.Data.Migrations
{
    public partial class AjusteentidadeServidor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documentos_Servidores_BeneficioServidorId",
                table: "Documentos");

            migrationBuilder.DropIndex(
                name: "IX_Documentos_BeneficioServidorId",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "BeneficioServidorId",
                table: "Documentos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data_Tramitacao",
                table: "Tramitacao",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 1, 16, 33, 33, 530, DateTimeKind.Local).AddTicks(2501),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 30, 9, 42, 25, 501, DateTimeKind.Local).AddTicks(4188));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Ultima_Modificacao",
                table: "Documentos",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 1, 16, 33, 33, 516, DateTimeKind.Local).AddTicks(4910),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 30, 9, 42, 25, 492, DateTimeKind.Local).AddTicks(6106));

            migrationBuilder.AddColumn<Guid>(
                name: "ServidorId1",
                table: "Documentos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_ServidorId1",
                table: "Documentos",
                column: "ServidorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Documentos_Servidores_ServidorId1",
                table: "Documentos",
                column: "ServidorId1",
                principalTable: "Servidores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documentos_Servidores_ServidorId1",
                table: "Documentos");

            migrationBuilder.DropIndex(
                name: "IX_Documentos_ServidorId1",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "ServidorId1",
                table: "Documentos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data_Tramitacao",
                table: "Tramitacao",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 30, 9, 42, 25, 501, DateTimeKind.Local).AddTicks(4188),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 10, 1, 16, 33, 33, 530, DateTimeKind.Local).AddTicks(2501));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Ultima_Modificacao",
                table: "Documentos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 30, 9, 42, 25, 492, DateTimeKind.Local).AddTicks(6106),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 10, 1, 16, 33, 33, 516, DateTimeKind.Local).AddTicks(4910));

            migrationBuilder.AddColumn<Guid>(
                name: "BeneficioServidorId",
                table: "Documentos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_BeneficioServidorId",
                table: "Documentos",
                column: "BeneficioServidorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documentos_Servidores_BeneficioServidorId",
                table: "Documentos",
                column: "BeneficioServidorId",
                principalTable: "Servidores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
