using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HrMnagermvc.Migrations
{
    /// <inheritdoc />
    public partial class updateCon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Employees_ApprovedById",
                table: "Requests");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$ySPpi/SNbrY80UJfMlG...MQu8J7yvi3WTTPw1rUk9GVYPdvmgCN.");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_HrManagers_ApprovedById",
                table: "Requests",
                column: "ApprovedById",
                principalTable: "HrManagers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_HrManagers_ApprovedById",
                table: "Requests");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$lO8yfDjgPpQyy798/P6h3.hrha3mLcIctk/KzRT9O97cKolWou9Ge");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Employees_ApprovedById",
                table: "Requests",
                column: "ApprovedById",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
