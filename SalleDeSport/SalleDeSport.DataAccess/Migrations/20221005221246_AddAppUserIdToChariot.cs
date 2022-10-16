using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalleDeSport.DataAccess.Migrations
{
    public partial class AddAppUserIdToChariot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "chariots",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_chariots_ApplicationUserId",
                table: "chariots",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_chariots_AspNetUsers_ApplicationUserId",
                table: "chariots",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id"/*,
                onDelete: ReferentialAction.Cascade*/);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chariots_AspNetUsers_ApplicationUserId",
                table: "chariots");

            migrationBuilder.DropIndex(
                name: "IX_chariots_ApplicationUserId",
                table: "chariots");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "chariots");
        }
    }
}
