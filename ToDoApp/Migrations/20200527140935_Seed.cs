using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoApp.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tag",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Tag 1" },
                    { 2, "Tag 2" },
                    { 3, "Tag 3" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "ef42f3de-c5db-4060-96a0-89a474fdcea2", "mshomer@gmail.com", false, "Matan", "Shomer", false, null, null, null, "Qawsedr1234!", null, null, false, null, false, null },
                    { 2, 0, "6f0f320a-45b6-41fb-a6d7-332876fa9510", "noam-choen@gmail.com", false, "Noam", "Choen", false, null, null, null, "Qawsedr1234!", null, null, false, null, false, null }
                });

            migrationBuilder.InsertData(
                table: "Todos",
                columns: new[] { "Id", "Description", "Status", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, null, false, "Todo 1", 1 },
                    { 2, null, false, "Todo 2", 1 },
                    { 3, null, false, "Todo 3", 2 }
                });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Id", "Address", "Age", "PictureUrl", "UserID" },
                values: new object[,]
                {
                    { 1, null, 33, null, 1 },
                    { 2, null, 25, null, 2 }
                });

            migrationBuilder.InsertData(
                table: "TodoTags",
                columns: new[] { "TagId", "TodoId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "TodoTags",
                columns: new[] { "TagId", "TodoId" },
                values: new object[] { 2, 1 });

            migrationBuilder.InsertData(
                table: "TodoTags",
                columns: new[] { "TagId", "TodoId" },
                values: new object[] { 3, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TodoTags",
                keyColumns: new[] { "TagId", "TodoId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "TodoTags",
                keyColumns: new[] { "TagId", "TodoId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "TodoTags",
                keyColumns: new[] { "TagId", "TodoId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
