using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class updatedHotelReq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Hotels_HotelId",
                table: "MenuItems");

            migrationBuilder.RenameColumn(
                name: "PinCode",
                table: "Addresses",
                newName: "Pincode");

            migrationBuilder.AlterColumn<long>(
                name: "HotelId",
                table: "MenuItems",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Hotels_HotelId",
                table: "MenuItems",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Hotels_HotelId",
                table: "MenuItems");

            migrationBuilder.RenameColumn(
                name: "Pincode",
                table: "Addresses",
                newName: "PinCode");

            migrationBuilder.AlterColumn<long>(
                name: "HotelId",
                table: "MenuItems",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Hotels_HotelId",
                table: "MenuItems",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
