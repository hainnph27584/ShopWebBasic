using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellerProduct.Migrations
{
    public partial class v11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Starus",
                table: "Role",
                newName: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Role",
                newName: "Starus");
        }
    }
}
