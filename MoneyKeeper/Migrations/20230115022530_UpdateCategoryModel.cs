using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MochiApi.Migrations
{
    public partial class UpdateCategoryModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_User_CreatorId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_Wallet_WalletId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_CreatorId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Category");

            migrationBuilder.AlterColumn<int>(
                name: "WalletId",
                table: "Category",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Wallet_WalletId",
                table: "Category",
                column: "WalletId",
                principalTable: "Wallet",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Wallet_WalletId",
                table: "Category");

            migrationBuilder.AlterColumn<int>(
                name: "WalletId",
                table: "Category",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Category",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Category_CreatorId",
                table: "Category",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_User_CreatorId",
                table: "Category",
                column: "CreatorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Wallet_WalletId",
                table: "Category",
                column: "WalletId",
                principalTable: "Wallet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
