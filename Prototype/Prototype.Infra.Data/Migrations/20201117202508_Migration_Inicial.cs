using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prototype.Infra.Data.Migrations
{
    public partial class Migration_Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blokcs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegisterDate = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Street = table.Column<string>(maxLength: 50, nullable: true),
                    UnitQuantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blokcs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proprietary",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegisterDate = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Document = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proprietary", x => x.Id);
                });

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
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegisterDate = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    FloorLevel = table.Column<int>(nullable: false),
                    BlokcId = table.Column<Guid>(nullable: false),
                    PropietaryId = table.Column<Guid>(nullable: false),
                    BlokcId1 = table.Column<Guid>(nullable: true),
                    ProprietaryId = table.Column<Guid>(nullable: true)
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
                    Ultima_Modificacao = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 11, 17, 17, 25, 7, 688, DateTimeKind.Local).AddTicks(9793)),
                    Arquivo_Base64 = table.Column<string>(nullable: false),
                    Bytes = table.Column<byte[]>(nullable: false),
                    Categoria = table.Column<int>(nullable: false),
                    SetorTramitacao = table.Column<int>(nullable: false),
                    CategoriaDescicao = table.Column<string>(nullable: true),
                    SetorDescricao = table.Column<string>(nullable: true),
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
                    DocumentoId1 = table.Column<Guid>(nullable: true),
                    Data_Tramitacao = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 11, 17, 17, 25, 7, 695, DateTimeKind.Local).AddTicks(4941)),
                    Setor_Origem = table.Column<int>(nullable: false),
                    Setor_Destino = table.Column<int>(nullable: false),
                    Setor_Origem_Descricao = table.Column<string>(nullable: true),
                    Setor_Destino_Descricao = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(maxLength: 50, nullable: false),
                    DocumentoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tramitacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tramitacao_Documentos_DocumentoId",
                        column: x => x.DocumentoId,
                        principalTable: "Documentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tramitacao_Servidores_DocumentoId1",
                        column: x => x.DocumentoId1,
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
                name: "IX_Tramitacao_DocumentoId1",
                table: "Tramitacao",
                column: "DocumentoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Tramitacao_ServidorId",
                table: "Tramitacao",
                column: "ServidorId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tramitacao");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Documentos");

            migrationBuilder.DropTable(
                name: "Blokcs");

            migrationBuilder.DropTable(
                name: "Proprietary");

            migrationBuilder.DropTable(
                name: "Servidores");
        }
    }
}
