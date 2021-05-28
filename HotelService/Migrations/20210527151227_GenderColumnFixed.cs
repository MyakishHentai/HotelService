using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelService.Migrations
{
    public partial class GenderColumnFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Users",
                type: "nvarchar(10)",
                nullable: true,
                comment: "Пол",
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldNullable: true,
                oldComment: "Пол");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Users",
                type: "nvarchar(3)",
                nullable: true,
                comment: "Пол",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldNullable: true,
                oldComment: "Пол");
        }
    }
}
