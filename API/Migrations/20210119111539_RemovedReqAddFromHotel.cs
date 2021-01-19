using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class RemovedReqAddFromHotel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Addresses_AddressId",
                table: "Hotels");

            migrationBuilder.AlterColumn<long>(
                name: "AddressId",
                table: "Hotels",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Addresses_AddressId",
                table: "Hotels",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Addresses_AddressId",
                table: "Hotels");

            migrationBuilder.AlterColumn<long>(
                name: "AddressId",
                table: "Hotels",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Addresses_AddressId",
                table: "Hotels",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
