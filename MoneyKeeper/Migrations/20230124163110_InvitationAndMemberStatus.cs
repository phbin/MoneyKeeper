using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace MochiApi.Migrations
{
    public partial class InvitationAndMemberStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "WalletMember",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InvitationId",
                table: "Notification",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Invitation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    WalletId = table.Column<int>(type: "int", nullable: false),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitation_User_SenderId",
                        column: x => x.SenderId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invitation_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invitation_Wallet_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "WalletMember",
                keyColumns: new[] { "UserId", "WalletId" },
                keyValues: new object[] { 1, 1 },
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "WalletMember",
                keyColumns: new[] { "UserId", "WalletId" },
                keyValues: new object[] { 2, 2 },
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "WalletMember",
                keyColumns: new[] { "UserId", "WalletId" },
                keyValues: new object[] { 3, 3 },
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "WalletMember",
                keyColumns: new[] { "UserId", "WalletId" },
                keyValues: new object[] { 4, 4 },
                column: "Status",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_InvitationId",
                table: "Notification",
                column: "InvitationId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_SenderId",
                table: "Invitation",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_UserId",
                table: "Invitation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_WalletId",
                table: "Invitation",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Invitation_InvitationId",
                table: "Notification",
                column: "InvitationId",
                principalTable: "Invitation",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Invitation_InvitationId",
                table: "Notification");

            migrationBuilder.DropTable(
                name: "Invitation");

            migrationBuilder.DropIndex(
                name: "IX_Notification_InvitationId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "WalletMember");

            migrationBuilder.DropColumn(
                name: "InvitationId",
                table: "Notification");
        }
    }
}
