using Microsoft.EntityFrameworkCore.Migrations;

namespace brick_and_mortar_store.Data.Migrations
{
    public partial class CreationFarmerandProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "role",
                table: "Farmers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "role",
                table: "Farmers");
        }
    }
}
