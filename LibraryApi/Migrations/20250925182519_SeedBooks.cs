using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Author", "MemberId", "Title", "isAvailable" },
                values: new object[,]
                {
                    { new Guid("aaa11111-aaaa-1111-aaaa-111111aaaa11"), "Robert C. Martin", null, "Clean Code", true },
                    { new Guid("aaa11111-aaaa-1111-aaaa-111111aaaa12"), "Andrew Hunt & David Thomas", null, "The Pragmatic Programmer", true },
                    { new Guid("aaa11111-aaaa-1111-aaaa-111111aaaa13"), "Erich Gamma et al.", null, "Design Patterns", true },
                    { new Guid("aaa11111-aaaa-1111-aaaa-111111aaaa14"), "Martin Fowler", null, "Refactoring", true },
                    { new Guid("aaa11111-aaaa-1111-aaaa-111111aaaa15"), "Eric Evans", null, "Domain-Driven Design", true }
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "MemberId", "ApiKey", "MemberFirstName", "MemberLastName", "Role" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), "member-api-key-1234567890", "John", "Doe", "Member" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: new Guid("aaa11111-aaaa-1111-aaaa-111111aaaa11"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: new Guid("aaa11111-aaaa-1111-aaaa-111111aaaa12"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: new Guid("aaa11111-aaaa-1111-aaaa-111111aaaa13"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: new Guid("aaa11111-aaaa-1111-aaaa-111111aaaa14"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: new Guid("aaa11111-aaaa-1111-aaaa-111111aaaa15"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));
        }
    }
}
