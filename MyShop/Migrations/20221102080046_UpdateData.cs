using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyShop.Migrations
{
    public partial class UpdateData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "2", 1 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 0, 45, 723, DateTimeKind.Local).AddTicks(823), new DateTime(2022, 11, 2, 15, 0, 45, 723, DateTimeKind.Local).AddTicks(824) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "3", 1 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 0, 45, 723, DateTimeKind.Local).AddTicks(826), new DateTime(2022, 11, 2, 15, 0, 45, 723, DateTimeKind.Local).AddTicks(827) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "4", 1 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 0, 45, 723, DateTimeKind.Local).AddTicks(828), new DateTime(2022, 11, 2, 15, 0, 45, 723, DateTimeKind.Local).AddTicks(829) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "2", 2 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 0, 45, 723, DateTimeKind.Local).AddTicks(830), new DateTime(2022, 11, 2, 15, 0, 45, 723, DateTimeKind.Local).AddTicks(831) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "2", 3 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 0, 45, 723, DateTimeKind.Local).AddTicks(832), new DateTime(2022, 11, 2, 15, 0, 45, 723, DateTimeKind.Local).AddTicks(833) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "1", 4 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 0, 45, 723, DateTimeKind.Local).AddTicks(834), new DateTime(2022, 11, 2, 15, 0, 45, 723, DateTimeKind.Local).AddTicks(835) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "2", 4 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 0, 45, 723, DateTimeKind.Local).AddTicks(836), new DateTime(2022, 11, 2, 15, 0, 45, 723, DateTimeKind.Local).AddTicks(837) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "3", 4 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 0, 45, 723, DateTimeKind.Local).AddTicks(838), new DateTime(2022, 11, 2, 15, 0, 45, 723, DateTimeKind.Local).AddTicks(839) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 0, 45, 723, DateTimeKind.Local).AddTicks(785), new DateTime(2022, 11, 2, 15, 0, 45, 723, DateTimeKind.Local).AddTicks(786) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 0, 45, 723, DateTimeKind.Local).AddTicks(788), new DateTime(2022, 11, 2, 15, 0, 45, 723, DateTimeKind.Local).AddTicks(788) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 0, 45, 723, DateTimeKind.Local).AddTicks(790), new DateTime(2022, 11, 2, 15, 0, 45, 723, DateTimeKind.Local).AddTicks(791) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 0, 45, 723, DateTimeKind.Local).AddTicks(792), new DateTime(2022, 11, 2, 15, 0, 45, 723, DateTimeKind.Local).AddTicks(793) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "2", 1 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 2, 14, 41, 51, 281, DateTimeKind.Local).AddTicks(9056), new DateTime(2022, 11, 2, 14, 41, 51, 281, DateTimeKind.Local).AddTicks(9057) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "3", 1 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 2, 14, 41, 51, 281, DateTimeKind.Local).AddTicks(9059), new DateTime(2022, 11, 2, 14, 41, 51, 281, DateTimeKind.Local).AddTicks(9060) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "4", 1 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 2, 14, 41, 51, 281, DateTimeKind.Local).AddTicks(9062), new DateTime(2022, 11, 2, 14, 41, 51, 281, DateTimeKind.Local).AddTicks(9063) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "2", 2 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 2, 14, 41, 51, 281, DateTimeKind.Local).AddTicks(9064), new DateTime(2022, 11, 2, 14, 41, 51, 281, DateTimeKind.Local).AddTicks(9065) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "2", 3 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 2, 14, 41, 51, 281, DateTimeKind.Local).AddTicks(9067), new DateTime(2022, 11, 2, 14, 41, 51, 281, DateTimeKind.Local).AddTicks(9068) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "1", 4 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 2, 14, 41, 51, 281, DateTimeKind.Local).AddTicks(9069), new DateTime(2022, 11, 2, 14, 41, 51, 281, DateTimeKind.Local).AddTicks(9070) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "2", 4 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 2, 14, 41, 51, 281, DateTimeKind.Local).AddTicks(9071), new DateTime(2022, 11, 2, 14, 41, 51, 281, DateTimeKind.Local).AddTicks(9072) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "3", 4 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 2, 14, 41, 51, 281, DateTimeKind.Local).AddTicks(9073), new DateTime(2022, 11, 2, 14, 41, 51, 281, DateTimeKind.Local).AddTicks(9074) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 2, 14, 41, 51, 281, DateTimeKind.Local).AddTicks(9012), new DateTime(2022, 11, 2, 14, 41, 51, 281, DateTimeKind.Local).AddTicks(9014) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 2, 14, 41, 51, 281, DateTimeKind.Local).AddTicks(9016), new DateTime(2022, 11, 2, 14, 41, 51, 281, DateTimeKind.Local).AddTicks(9017) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 2, 14, 41, 51, 281, DateTimeKind.Local).AddTicks(9018), new DateTime(2022, 11, 2, 14, 41, 51, 281, DateTimeKind.Local).AddTicks(9019) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 2, 14, 41, 51, 281, DateTimeKind.Local).AddTicks(9020), new DateTime(2022, 11, 2, 14, 41, 51, 281, DateTimeKind.Local).AddTicks(9021) });
        }
    }
}
