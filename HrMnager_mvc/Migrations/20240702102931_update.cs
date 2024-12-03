using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HrMnagermvc.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$ud7stobNeyFiFMayzYNq7eq9h2DzN1uRhTVmuJjMJSPq3dX9pJxNy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$ySPpi/SNbrY80UJfMlG...MQu8J7yvi3WTTPw1rUk9GVYPdvmgCN.");
        }
    }
}
