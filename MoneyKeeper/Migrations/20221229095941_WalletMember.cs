using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyKeeper.Migrations
{
    public partial class WalletMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Wallet_WalletId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_User_OwnerId",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_Wallet_OwnerId",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_User_WalletId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "User");

            migrationBuilder.CreateTable(
                name: "WalletMember",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WalletId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    JoinAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletMember", x => new { x.UserId, x.WalletId });
                    table.ForeignKey(
                        name: "FK_WalletMember_User_WalletId",
                        column: x => x.WalletId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WalletMember_Wallet_UserId",
                        column: x => x.UserId,
                        principalTable: "Wallet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Wallet",
                columns: new[] { "Id", "Balance", "IsDefault", "Name", "Type" },
                values: new object[,]
                {
                    { 1, 100000, true, "Ví", 0 },
                    { 2, 200000, true, "Ví", 0 },
                    { 3, 300000, true, "Ví", 0 },
                    { 4, 400000, true, "Ví", 0 }
                });

            migrationBuilder.InsertData(
                table: "WalletMember",
                columns: new[] { "UserId", "WalletId", "JoinAt", "Role" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 2, 2, new DateTime(2022, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 3, 3, new DateTime(2022, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 4, 4, new DateTime(2022, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WalletMember_WalletId",
                table: "WalletMember",
                column: "WalletId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WalletMember");

            migrationBuilder.DeleteData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Wallet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WalletId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_OwnerId",
                table: "Wallet",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_User_WalletId",
                table: "User",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Wallet_WalletId",
                table: "User",
                column: "WalletId",
                principalTable: "Wallet",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_User_OwnerId",
                table: "Wallet",
                column: "OwnerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
