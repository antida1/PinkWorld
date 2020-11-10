using Microsoft.EntityFrameworkCore.Migrations;

namespace PinkWorld.Web.Migrations
{
    public partial class ChangeNameAddressInSIte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Adress",
                table: "Site",
                newName: "Address");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Site",
                newName: "Adress");
        }
    }
}
