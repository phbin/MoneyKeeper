using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MochiApi.Migrations
{
    public partial class CategorySeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Group", "Icon", "Name", "Type", "WalletId" },
                values: new object[,]
                {
                    { 17, 1, "1", "Ăn uống", 1, 1 },
                    { 18, 1, "1", "Di chuyển", 1, 1 },
                    { 19, 1, "1", "Thuê nhà", 1, 1 },
                    { 20, 1, "1", "Hóa đơn điện thoại", 1, 1 },
                    { 21, 1, "1", "Hóa đơn internet", 1, 1 },
                    { 22, 1, "1", "Hóa đơn tiện ích khác", 1, 1 },
                    { 23, 2, "1", "Sửa & trang trí khác", 1, 1 },
                    { 24, 1, "1", "Bảo dưỡng xe", 1, 1 },
                    { 25, 2, "1", "Khám sức khỏe", 1, 1 },
                    { 26, 1, "1", "Thể dục thể thao", 1, 1 },
                    { 27, 0, "1", "Lương", 0, 1 },
                    { 28, 0, "1", "Tiền ăn vặt", 0, 1 },
                    { 29, 0, "1", "Thu nhập khác", 0, 1 },
                    { 30, 4, "1", "Đầu tư", 1, 1 },
                    { 31, 4, "1", "Nợ", 1, 1 },
                    { 32, 4, "1", "Cho vay", 1, 1 },
                    { 33, 1, "1", "Ăn uống", 1, 2 },
                    { 34, 1, "1", "Di chuyển", 1, 2 },
                    { 35, 1, "1", "Thuê nhà", 1, 2 },
                    { 36, 1, "1", "Hóa đơn điện thoại", 1, 2 },
                    { 37, 1, "1", "Hóa đơn internet", 1, 2 },
                    { 38, 1, "1", "Hóa đơn tiện ích khác", 1, 2 },
                    { 39, 2, "1", "Sửa & trang trí khác", 1, 2 },
                    { 40, 1, "1", "Bảo dưỡng xe", 1, 2 },
                    { 41, 2, "1", "Khám sức khỏe", 1, 2 },
                    { 42, 1, "1", "Thể dục thể thao", 1, 2 },
                    { 43, 0, "1", "Lương", 0, 2 },
                    { 44, 0, "1", "Tiền ăn vặt", 0, 2 },
                    { 45, 0, "1", "Thu nhập khác", 0, 2 },
                    { 46, 4, "1", "Đầu tư", 1, 2 },
                    { 47, 4, "1", "Nợ", 1, 2 },
                    { 48, 4, "1", "Cho vay", 1, 2 },
                    { 49, 1, "1", "Ăn uống", 1, 3 },
                    { 50, 1, "1", "Di chuyển", 1, 3 },
                    { 51, 1, "1", "Thuê nhà", 1, 3 },
                    { 52, 1, "1", "Hóa đơn điện thoại", 1, 3 },
                    { 53, 1, "1", "Hóa đơn internet", 1, 3 },
                    { 54, 1, "1", "Hóa đơn tiện ích khác", 1, 3 },
                    { 55, 2, "1", "Sửa & trang trí khác", 1, 3 },
                    { 56, 1, "1", "Bảo dưỡng xe", 1, 3 },
                    { 57, 2, "1", "Khám sức khỏe", 1, 3 },
                    { 58, 1, "1", "Thể dục thể thao", 1, 3 },
                    { 59, 0, "1", "Lương", 0, 3 },
                    { 60, 0, "1", "Tiền ăn vặt", 0, 3 },
                    { 61, 0, "1", "Thu nhập khác", 0, 3 },
                    { 62, 4, "1", "Đầu tư", 1, 3 },
                    { 63, 4, "1", "Nợ", 1, 3 },
                    { 64, 4, "1", "Cho vay", 1, 3 },
                    { 65, 1, "1", "Ăn uống", 1, 4 },
                    { 66, 1, "1", "Di chuyển", 1, 4 },
                    { 67, 1, "1", "Thuê nhà", 1, 4 },
                    { 68, 1, "1", "Hóa đơn điện thoại", 1, 4 },
                    { 69, 1, "1", "Hóa đơn internet", 1, 4 },
                    { 70, 1, "1", "Hóa đơn tiện ích khác", 1, 4 },
                    { 71, 2, "1", "Sửa & trang trí khác", 1, 4 },
                    { 72, 1, "1", "Bảo dưỡng xe", 1, 4 },
                    { 73, 2, "1", "Khám sức khỏe", 1, 4 },
                    { 74, 1, "1", "Thể dục thể thao", 1, 4 },
                    { 75, 0, "1", "Lương", 0, 4 },
                    { 76, 0, "1", "Tiền ăn vặt", 0, 4 },
                    { 77, 0, "1", "Thu nhập khác", 0, 4 },
                    { 78, 4, "1", "Đầu tư", 1, 4 },
                    { 79, 4, "1", "Nợ", 1, 4 },
                    { 80, 4, "1", "Cho vay", 1, 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 80);
        }
    }
}
