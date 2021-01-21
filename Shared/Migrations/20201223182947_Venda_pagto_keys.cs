using Microsoft.EntityFrameworkCore.Migrations;

namespace AmbiStore.Shared.Migrations
{
    public partial class Venda_pagto_keys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<decimal>(
                name: "VLR_DESCONTO",
                table: "VENDA_ITEMs",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "INTERVALO",
                table: "PARCELAMENTOs",
                nullable: true);

            migrationBuilder.InsertData(
                table: "PARCELAMENTOs",
                columns: new[] { "ID", "DESCRICAO", "ENTRADA", "INTERVALO", "NUMERO_PARCELA", "STATUS" },
                values: new object[] { 1, "À Vista", true, "0", 1, 1 });

            migrationBuilder.InsertData(
                table: "PARCELAMENTOs",
                columns: new[] { "ID", "DESCRICAO", "ENTRADA", "INTERVALO", "NUMERO_PARCELA", "STATUS" },
                values: new object[] { 2, "Cartão À Vista", true, "0", 1, 1 });

            migrationBuilder.InsertData(
                table: "PARCELAMENTOs",
                columns: new[] { "ID", "DESCRICAO", "ENTRADA", "INTERVALO", "NUMERO_PARCELA", "STATUS" },
                values: new object[] { 3, "30 dias", true, "30", 1, 1 });

            migrationBuilder.InsertData(
                table: "FORMAPAGAMENTOs",
                columns: new[] { "ID", "DESCRICAO", "PARCELAMENTO_ID", "STATUS", "UTILIZACAO" },
                values: new object[,]
                {
                    { 1, "Dinheiro", 1, 1, 2 },
                    { 10, "Outros", 1, 1, 2 },
                    { 3, "Cartão de Crédito", 2, 1, 2 },
                    { 4, "Cartão de Débito", 2, 1, 2 },
                    { 2, "A Prazo", 3, 1, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_VENDA_PAGAMENTOs_FORMAPAGAMENTO_ID",
                table: "VENDA_PAGAMENTOs",
                column: "FORMAPAGAMENTO_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_VENDA_PAGAMENTOs_FORMAPAGAMENTOs_FORMAPAGAMENTO_ID",
                table: "VENDA_PAGAMENTOs",
                column: "FORMAPAGAMENTO_ID",
                principalTable: "FORMAPAGAMENTOs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VENDA_PAGAMENTOs_FORMAPAGAMENTOs_FORMAPAGAMENTO_ID",
                table: "VENDA_PAGAMENTOs");

            migrationBuilder.DropIndex(
                name: "IX_VENDA_PAGAMENTOs_FORMAPAGAMENTO_ID",
                table: "VENDA_PAGAMENTOs");

            migrationBuilder.DeleteData(
                table: "FORMAPAGAMENTOs",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FORMAPAGAMENTOs",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FORMAPAGAMENTOs",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FORMAPAGAMENTOs",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FORMAPAGAMENTOs",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "PARCELAMENTOs",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PARCELAMENTOs",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PARCELAMENTOs",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "VLR_DESCONTO",
                table: "VENDA_ITEMs");

            migrationBuilder.DropColumn(
                name: "INTERVALO",
                table: "PARCELAMENTOs");

            migrationBuilder.AddColumn<decimal>(
                name: "VALORVENDA",
                table: "VENDAs",
                type: "NUMERIC(18,4)",
                nullable: false,
                defaultValue: 0m);

        }
    }
}
