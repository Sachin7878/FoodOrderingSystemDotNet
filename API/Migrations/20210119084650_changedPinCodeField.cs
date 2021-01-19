using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class changedPinCodeField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PinCode",
                table: "Addresses",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "PinCode",
                table: "Addresses",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
