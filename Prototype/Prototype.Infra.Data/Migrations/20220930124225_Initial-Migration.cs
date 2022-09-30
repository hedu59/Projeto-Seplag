using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prototype.Infra.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Servidores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegisterDate = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(maxLength: 80, nullable: false),
                    CPF = table.Column<string>(maxLength: 11, nullable: false),
                    Orgao = table.Column<string>(maxLength: 80, nullable: false),
                    Matricula = table.Column<string>(maxLength: 40, nullable: false),
                    Setor_Atual = table.Column<int>(nullable: false),
                    SetorDescricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servidores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegisterDate = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Login = table.Column<string>(maxLength: 50, nullable: false, defaultValue: "Admin"),
                    Email = table.Column<string>(maxLength: 100, nullable: false, defaultValue: "admin@prototype.com"),
                    Password = table.Column<string>(maxLength: 15, nullable: false, defaultValue: "123456")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegisterDate = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ServidorId = table.Column<Guid>(nullable: false),
                    Nome_Arquivo = table.Column<string>(maxLength: 200, nullable: false),
                    Tamanho_Arquivo = table.Column<string>(nullable: false),
                    Tipo = table.Column<string>(nullable: false, defaultValue: "application/pdf"),
                    Ultima_Modificacao = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2022, 9, 30, 9, 42, 25, 492, DateTimeKind.Local).AddTicks(6106)),
                    Arquivo_Base64 = table.Column<string>(nullable: false),
                    Bytes = table.Column<byte[]>(nullable: false),
                    Categoria = table.Column<int>(nullable: false),
                    CategoriaDescicao = table.Column<string>(nullable: true),
                    BeneficioServidorId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documentos_Servidores_BeneficioServidorId",
                        column: x => x.BeneficioServidorId,
                        principalTable: "Servidores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documentos_Servidores_ServidorId",
                        column: x => x.ServidorId,
                        principalTable: "Servidores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tramitacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegisterDate = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ServidorId = table.Column<Guid>(nullable: false),
                    DocumentoId = table.Column<Guid>(nullable: true),
                    Data_Tramitacao = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2022, 9, 30, 9, 42, 25, 501, DateTimeKind.Local).AddTicks(4188)),
                    Setor_Origem = table.Column<int>(nullable: false),
                    Setor_Destino = table.Column<int>(nullable: false),
                    Setor_Origem_Descricao = table.Column<string>(nullable: true),
                    Setor_Destino_Descricao = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tramitacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tramitacao_Servidores_DocumentoId",
                        column: x => x.DocumentoId,
                        principalTable: "Servidores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tramitacao_Servidores_ServidorId",
                        column: x => x.ServidorId,
                        principalTable: "Servidores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_BeneficioServidorId",
                table: "Documentos",
                column: "BeneficioServidorId");

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_ServidorId",
                table: "Documentos",
                column: "ServidorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tramitacao_DocumentoId",
                table: "Tramitacao",
                column: "DocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tramitacao_ServidorId",
                table: "Tramitacao",
                column: "ServidorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documentos");

            migrationBuilder.DropTable(
                name: "Tramitacao");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Servidores");
        }
    }
}
