using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalleDeSport.DataAccess.Migrations
{
    public partial class AjoutNationaliteCommande : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nationalite",
                table: "Commande",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nationalite",
                table: "Commande");
        }
    }
}
