using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShop.EF.Migrations
{
    public partial class testing12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Customer_CustomerID",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Customer_CustomerID",
                table: "Transactions",
                column: "CustomerID",
                principalSchema: "PetShop",
                principalTable: "Customer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Customer_CustomerID",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Customer_CustomerID",
                table: "Transactions",
                column: "CustomerID",
                principalSchema: "PetShop",
                principalTable: "Customer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
