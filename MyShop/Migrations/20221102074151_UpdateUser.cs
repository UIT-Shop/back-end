using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyShop.Migrations
{
    public partial class UpdateUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "2", 1 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 1, 21, 10, 7, 572, DateTimeKind.Local).AddTicks(2243), new DateTime(2022, 11, 1, 21, 10, 7, 572, DateTimeKind.Local).AddTicks(2244) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "3", 1 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 1, 21, 10, 7, 572, DateTimeKind.Local).AddTicks(2246), new DateTime(2022, 11, 1, 21, 10, 7, 572, DateTimeKind.Local).AddTicks(2247) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "4", 1 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 1, 21, 10, 7, 572, DateTimeKind.Local).AddTicks(2248), new DateTime(2022, 11, 1, 21, 10, 7, 572, DateTimeKind.Local).AddTicks(2248) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "2", 2 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 1, 21, 10, 7, 572, DateTimeKind.Local).AddTicks(2249), new DateTime(2022, 11, 1, 21, 10, 7, 572, DateTimeKind.Local).AddTicks(2250) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "2", 3 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 1, 21, 10, 7, 572, DateTimeKind.Local).AddTicks(2251), new DateTime(2022, 11, 1, 21, 10, 7, 572, DateTimeKind.Local).AddTicks(2251) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "1", 4 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 1, 21, 10, 7, 572, DateTimeKind.Local).AddTicks(2252), new DateTime(2022, 11, 1, 21, 10, 7, 572, DateTimeKind.Local).AddTicks(2253) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "2", 4 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 1, 21, 10, 7, 572, DateTimeKind.Local).AddTicks(2253), new DateTime(2022, 11, 1, 21, 10, 7, 572, DateTimeKind.Local).AddTicks(2254) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "3", 4 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 1, 21, 10, 7, 572, DateTimeKind.Local).AddTicks(2255), new DateTime(2022, 11, 1, 21, 10, 7, 572, DateTimeKind.Local).AddTicks(2255) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 1, 21, 10, 7, 572, DateTimeKind.Local).AddTicks(2215), new DateTime(2022, 11, 1, 21, 10, 7, 572, DateTimeKind.Local).AddTicks(2215) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 1, 21, 10, 7, 572, DateTimeKind.Local).AddTicks(2217), new DateTime(2022, 11, 1, 21, 10, 7, 572, DateTimeKind.Local).AddTicks(2217) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 1, 21, 10, 7, 572, DateTimeKind.Local).AddTicks(2218), new DateTime(2022, 11, 1, 21, 10, 7, 572, DateTimeKind.Local).AddTicks(2219) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 1, 21, 10, 7, 572, DateTimeKind.Local).AddTicks(2219), new DateTime(2022, 11, 1, 21, 10, 7, 572, DateTimeKind.Local).AddTicks(2220) });
        }
    }
}
