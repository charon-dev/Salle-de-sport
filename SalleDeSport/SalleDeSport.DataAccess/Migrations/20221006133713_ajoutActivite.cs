using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalleDeSport.DataAccess.Migrations
{
    public partial class ajoutActivite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActiviteId",
                table: "coachs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "activites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activites", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_coachs_ActiviteId",
                table: "coachs",
                column: "ActiviteId");

            migrationBuilder.AddForeignKey(
                name: "FK_coachs_activites_ActiviteId",
                table: "coachs",
                column: "ActiviteId",
                principalTable: "activites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_coachs_activites_ActiviteId",
                table: "coachs");

            migrationBuilder.DropTable(
                name: "activites");

            migrationBuilder.DropIndex(
                name: "IX_coachs_ActiviteId",
                table: "coachs");

            migrationBuilder.DropColumn(
                name: "ActiviteId",
                table: "coachs");
        }
    }
}
