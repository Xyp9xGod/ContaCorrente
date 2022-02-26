using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContaCorrente.Infra.Data.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    BankCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Balance = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    BankCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Value = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "Id", "AccountNumber", "Balance", "BankCode" },
                values: new object[,]
                {
                    { 1, "123456-0", 37.0, "371" },
                    { 2, "678910-2", 79.0, "371" },
                    { 3, "345678-9", 135.0, "371" }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "AccountNumber", "BankCode", "Date", "Type", "Value" },
                values: new object[,]
                {
                    { 1, "123456-0", "371", new DateTime(2022, 2, 26, 0, 0, 0, 0, DateTimeKind.Local), "C", 36.450000000000003 },
                    { 2, "123456-0", "371", new DateTime(2022, 2, 26, 0, 0, 0, 0, DateTimeKind.Local), "D", 11.5 },
                    { 3, "345678-9", "371", new DateTime(2022, 2, 26, 0, 0, 0, 0, DateTimeKind.Local), "C", 78.0 },
                    { 4, "345678-9", "371", new DateTime(2022, 2, 26, 0, 0, 0, 0, DateTimeKind.Local), "D", 96.120000000000005 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
