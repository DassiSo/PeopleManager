using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeopleManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAfterModelChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "People");

            migrationBuilder.AddColumn<string>(
                name: "IdentityNumber",
                table: "People",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "People",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityNumber",
                table: "People");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "People");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "People",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
