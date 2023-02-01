using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MochiApi.Migrations
{
    public partial class ChangeForeignkeyWalletMemberModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WalletMember_User_WalletId",
                table: "WalletMember");

            migrationBuilder.DropForeignKey(
                name: "FK_WalletMember_Wallet_UserId",
                table: "WalletMember");

            migrationBuilder.AddForeignKey(
                name: "FK_WalletMember_User_UserId",
                table: "WalletMember",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WalletMember_Wallet_WalletId",
                table: "WalletMember",
                column: "WalletId",
                principalTable: "Wallet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WalletMember_User_UserId",
                table: "WalletMember");

            migrationBuilder.DropForeignKey(
                name: "FK_WalletMember_Wallet_WalletId",
                table: "WalletMember");

            migrationBuilder.AddForeignKey(
                name: "FK_WalletMember_User_WalletId",
                table: "WalletMember",
                column: "WalletId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WalletMember_Wallet_UserId",
                table: "WalletMember",
                column: "UserId",
                principalTable: "Wallet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
