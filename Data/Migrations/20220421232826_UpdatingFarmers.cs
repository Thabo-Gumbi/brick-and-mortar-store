using Microsoft.EntityFrameworkCore.Migrations;

namespace brick_and_mortar_store.Data.Migrations
{
    public partial class UpdatingFarmers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FarmerEmail",
                table: "Farmers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FarmerPassword",
                table: "Farmers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FarmerPicture",
                table: "Farmers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FarmerEmail",
                table: "Farmers");

            migrationBuilder.DropColumn(
                name: "FarmerPassword",
                table: "Farmers");

            migrationBuilder.DropColumn(
                name: "FarmerPicture",
                table: "Farmers");
        }
    }
}
