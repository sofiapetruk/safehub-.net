using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gs.Migrations
{
    /// <inheritdoc />
    public partial class PrimeiraMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_SAFEHUB_CADASTRO_ABRIGO",
                columns: table => new
                {
                    IdCadastroAbrigo = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nome_abrigo = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    capacidade_pessoa = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    nm_responsavel = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    longitude = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    latitude = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SAFEHUB_CADASTRO_ABRIGO", x => x.IdCadastroAbrigo);
                });

            migrationBuilder.CreateTable(
                name: "T_SAFEHUB_ABRIGO_OCUPACAO",
                columns: table => new
                {
                    IdOcupacao = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nr_pessoa = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    dt_registro = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    fk_id_abrigo = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SAFEHUB_ABRIGO_OCUPACAO", x => x.IdOcupacao);
                    table.ForeignKey(
                        name: "FK_Ocupacao_Abrigo",
                        column: x => x.fk_id_abrigo,
                        principalTable: "T_SAFEHUB_CADASTRO_ABRIGO",
                        principalColumn: "IdCadastroAbrigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_SAFEHUB_CADASTRO_USUARIO",
                columns: table => new
                {
                    IdUsuario = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    senha = table.Column<string>(type: "NVARCHAR2(15)", maxLength: 15, nullable: false),
                    fk_id_abrigo = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SAFEHUB_CADASTRO_USUARIO", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuario_Abrigo",
                        column: x => x.fk_id_abrigo,
                        principalTable: "T_SAFEHUB_CADASTRO_ABRIGO",
                        principalColumn: "IdCadastroAbrigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_SAFEHUB_ESTOQUE_ABRIGO",
                columns: table => new
                {
                    IdEstoque = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_item = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    tp_item = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    quantidade = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    fk_id_abrigo = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SAFEHUB_ESTOQUE_ABRIGO", x => x.IdEstoque);
                    table.ForeignKey(
                        name: "FK_Estoque_Abrigo",
                        column: x => x.fk_id_abrigo,
                        principalTable: "T_SAFEHUB_CADASTRO_ABRIGO",
                        principalColumn: "IdCadastroAbrigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_SAFEHUB_ABRIGO_OCUPACAO_fk_id_abrigo",
                table: "T_SAFEHUB_ABRIGO_OCUPACAO",
                column: "fk_id_abrigo");

            migrationBuilder.CreateIndex(
                name: "IX_T_SAFEHUB_CADASTRO_USUARIO_fk_id_abrigo",
                table: "T_SAFEHUB_CADASTRO_USUARIO",
                column: "fk_id_abrigo");

            migrationBuilder.CreateIndex(
                name: "IX_T_SAFEHUB_ESTOQUE_ABRIGO_fk_id_abrigo",
                table: "T_SAFEHUB_ESTOQUE_ABRIGO",
                column: "fk_id_abrigo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_SAFEHUB_ABRIGO_OCUPACAO");

            migrationBuilder.DropTable(
                name: "T_SAFEHUB_CADASTRO_USUARIO");

            migrationBuilder.DropTable(
                name: "T_SAFEHUB_ESTOQUE_ABRIGO");

            migrationBuilder.DropTable(
                name: "T_SAFEHUB_CADASTRO_ABRIGO");
        }
    }
}
