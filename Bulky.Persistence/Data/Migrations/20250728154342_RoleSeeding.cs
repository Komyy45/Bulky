using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bulky.Persistence.data.migrations
{
    /// <inheritdoc />
    public partial class RoleSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0b0de5a6-06b4-4b3b-b913-17c9b3ea4f2f", null, "Customer", "CUSTOMER" },
                    { "7f3f1b8c-34aa-48e9-bc0e-b0d5f0149c0b", null, "Company", "COMPANY" },
                    { "a3e18d92-5c8b-4e06-9b9b-1d1f8038b0ef", null, "Admin", "ADMIN" },
                    { "c928dc5f-6b5c-4c33-a52b-2fa1f78fc9d2", null, "Employee", "EMPLOYEE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b0de5a6-06b4-4b3b-b913-17c9b3ea4f2f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f3f1b8c-34aa-48e9-bc0e-b0d5f0149c0b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a3e18d92-5c8b-4e06-9b9b-1d1f8038b0ef");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c928dc5f-6b5c-4c33-a52b-2fa1f78fc9d2");
        }
    }
}
