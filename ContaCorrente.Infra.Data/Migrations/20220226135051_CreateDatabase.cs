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
                    AgencyNumber = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
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
                    Value = table.Column<double>(type: "float", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "Id", "AccountNumber", "AgencyNumber", "Balance", "BankCode" },
                values: new object[,]
                {
                    { 1, "123456-0", "0001", 37.0, "371" },
                    { 2, "678910-2", "0001", 79.0, "371" },
                    { 3, "345678-9", "0001", 135.0, "371" }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "AccountNumber", "BankCode", "Date", "Type", "Value" },
                values: new object[,]
                {
                    { 1, "123456-0", "371", new DateTime(2022, 2, 26, 0, 0, 0, 0, DateTimeKind.Local), 1, 36.45 },
                    { 2, "123456-0", "371", new DateTime(2022, 2, 26, 0, 0, 0, 0, DateTimeKind.Local), 2, 11.5 },
                    { 3, "345678-9", "371", new DateTime(2022, 2, 26, 0, 0, 0, 0, DateTimeKind.Local), 1, 78.0 },
                    { 4, "345678-9", "371", new DateTime(2022, 2, 26, 0, 0, 0, 0, DateTimeKind.Local), 3, 96.12 }
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
