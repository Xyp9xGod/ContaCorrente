using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace ContaCorrente.Infra.Data.Migrations
{
    public partial class Mysql : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    AccountNumber = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    BankCode = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    AgencyNumber = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: false),
                    Balance = table.Column<double>(type: "double", nullable: false)
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
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    AccountNumber = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    BankCode = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    Value = table.Column<double>(type: "double", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "Id", "AccountNumber", "AgencyNumber", "Balance", "BankCode" },
                values: new object[] { 1, "123456-0", "0001", 40.0, "371" });

            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "Id", "AccountNumber", "AgencyNumber", "Balance", "BankCode" },
                values: new object[] { 2, "678910-2", "0001", 60.0, "371" });

            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "Id", "AccountNumber", "AgencyNumber", "Balance", "BankCode" },
                values: new object[] { 3, "345678-9", "0001", 150.0, "371" });
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
