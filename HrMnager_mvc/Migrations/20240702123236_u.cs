using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HrMnagermvc.Migrations
{
    /// <inheritdoc />
    public partial class u : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$h8XCpRX9zGgoQTzYVjr8guXC9lMA39mrJrcm44kfwTyYzjZ./CPqm");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$UE.VW4yBM1d7IN051GL1XOwckXjIUtCNERhJGoOPnvpwk6mCee3Lu");
        }
    }
}
