using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HrMnagermvc.Migrations
{
    /// <inheritdoc />
    public partial class up : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LeaveEndDate",
                table: "Requests",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LeaveStartDate",
                table: "Requests",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$UE.VW4yBM1d7IN051GL1XOwckXjIUtCNERhJGoOPnvpwk6mCee3Lu");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeaveEndDate",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "LeaveStartDate",
                table: "Requests");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$ud7stobNeyFiFMayzYNq7eq9h2DzN1uRhTVmuJjMJSPq3dX9pJxNy");
        }
    }
}
