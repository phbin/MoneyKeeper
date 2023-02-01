using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MochiApi.Migrations
{
    public partial class SeedCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Icon", "Name", "Type", "WalletId" },
                values: new object[,]
                {
                    { 1, "https://picsum.photos/100", "Ăn uống", 1, null },
                    { 2, "https://picsum.photos/100", "Di chuyển", 1, null },
                    { 3, "https://picsum.photos/100", "Thuê nhà", 1, null },
                    { 4, "https://picsum.photos/100", "Hóa đơn điện thoại", 1, null },
                    { 5, "https://picsum.photos/100", "Hóa đơn internet", 1, null },
                    { 6, "https://picsum.photos/100", "Hóa đơn tiện ích khác", 1, null },
                    { 7, "https://picsum.photos/100", "Sửa & trang trí khác", 1, null },
                    { 8, "https://picsum.photos/100", "Bảo dưỡng xe", 1, null },
                    { 9, "https://picsum.photos/100", "Khám sức khỏe", 1, null },
                    { 10, "https://picsum.photos/100", "Thể dục thể thao", 1, null },
                    { 11, "https://picsum.photos/100", "Lương", 0, null },
                    { 12, "https://picsum.photos/100", "Tiền ăn vặt", 0, null },
                    { 13, "https://picsum.photos/100", "Thu nhập khác", 0, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 13);
        }
    }
}
