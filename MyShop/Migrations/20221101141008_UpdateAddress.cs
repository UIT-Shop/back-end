using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyShop.Migrations
{
    public partial class UpdateAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Addresses_AddressId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AddressId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Users_UserId",
                table: "Addresses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Users_UserId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Addresses");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "2", 1 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 1, 15, 39, 32, 681, DateTimeKind.Local).AddTicks(2175), new DateTime(2022, 11, 1, 15, 39, 32, 681, DateTimeKind.Local).AddTicks(2176) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "3", 1 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 1, 15, 39, 32, 681, DateTimeKind.Local).AddTicks(2178), new DateTime(2022, 11, 1, 15, 39, 32, 681, DateTimeKind.Local).AddTicks(2178) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "4", 1 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 1, 15, 39, 32, 681, DateTimeKind.Local).AddTicks(2179), new DateTime(2022, 11, 1, 15, 39, 32, 681, DateTimeKind.Local).AddTicks(2179) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "2", 2 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 1, 15, 39, 32, 681, DateTimeKind.Local).AddTicks(2180), new DateTime(2022, 11, 1, 15, 39, 32, 681, DateTimeKind.Local).AddTicks(2180) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "2", 3 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 1, 15, 39, 32, 681, DateTimeKind.Local).AddTicks(2181), new DateTime(2022, 11, 1, 15, 39, 32, 681, DateTimeKind.Local).AddTicks(2182) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "1", 4 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 1, 15, 39, 32, 681, DateTimeKind.Local).AddTicks(2183), new DateTime(2022, 11, 1, 15, 39, 32, 681, DateTimeKind.Local).AddTicks(2183) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "2", 4 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 1, 15, 39, 32, 681, DateTimeKind.Local).AddTicks(2184), new DateTime(2022, 11, 1, 15, 39, 32, 681, DateTimeKind.Local).AddTicks(2184) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductColorId", "ProductId" },
                keyValues: new object[] { "3", 4 },
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 1, 15, 39, 32, 681, DateTimeKind.Local).AddTicks(2185), new DateTime(2022, 11, 1, 15, 39, 32, 681, DateTimeKind.Local).AddTicks(2186) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 1, 15, 39, 32, 681, DateTimeKind.Local).AddTicks(2152), new DateTime(2022, 11, 1, 15, 39, 32, 681, DateTimeKind.Local).AddTicks(2152) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 1, 15, 39, 32, 681, DateTimeKind.Local).AddTicks(2154), new DateTime(2022, 11, 1, 15, 39, 32, 681, DateTimeKind.Local).AddTicks(2154) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 1, 15, 39, 32, 681, DateTimeKind.Local).AddTicks(2155), new DateTime(2022, 11, 1, 15, 39, 32, 681, DateTimeKind.Local).AddTicks(2155) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 1, 15, 39, 32, 681, DateTimeKind.Local).AddTicks(2156), new DateTime(2022, 11, 1, 15, 39, 32, 681, DateTimeKind.Local).AddTicks(2156) });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressId",
                table: "Users",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Addresses_AddressId",
                table: "Users",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
