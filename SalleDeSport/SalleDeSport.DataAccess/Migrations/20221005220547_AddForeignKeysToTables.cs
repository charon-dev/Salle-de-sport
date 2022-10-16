using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalleDeSport.DataAccess.Migrations
{
    public partial class AddForeignKeysToTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AbonnementId",
                table: "chariots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AbonnementId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoachId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_chariots_AbonnementId",
                table: "chariots",
                column: "AbonnementId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AbonnementId",
                table: "AspNetUsers",
                column: "AbonnementId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CoachId",
                table: "AspNetUsers",
                column: "CoachId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_abonnements_AbonnementId",
                table: "AspNetUsers",
                column: "AbonnementId",
                principalTable: "abonnements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_coachs_CoachId",
                table: "AspNetUsers",
                column: "CoachId",
                principalTable: "coachs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_chariots_abonnements_AbonnementId",
                table: "chariots",
                column: "AbonnementId",
                principalTable: "abonnements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_abonnements_AbonnementId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_coachs_CoachId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_chariots_abonnements_AbonnementId",
                table: "chariots");

            migrationBuilder.DropIndex(
                name: "IX_chariots_AbonnementId",
                table: "chariots");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AbonnementId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CoachId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AbonnementId",
                table: "chariots");

            migrationBuilder.DropColumn(
                name: "AbonnementId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CoachId",
                table: "AspNetUsers");
        }
    }
}
