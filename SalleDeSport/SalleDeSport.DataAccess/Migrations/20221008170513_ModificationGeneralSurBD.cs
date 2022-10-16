using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalleDeSport.DataAccess.Migrations
{
    public partial class ModificationGeneralSurBD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_abonnements_AbonnementId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_coachs_CoachId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AbonnementId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CoachId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AbonnementId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CoachId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateDebutAbonnement",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateFintAbonnement",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Commande",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateDebutAbonnement = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFintAbonnement = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrixTotal = table.Column<double>(type: "float", nullable: false),
                    StatutDePayement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayementIntentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatePayement = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commande", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commande_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetailleCommande",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prix = table.Column<double>(type: "float", nullable: false),
                    AbonnementId = table.Column<int>(type: "int", nullable: false),
                    IdCommande = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailleCommande", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailleCommande_abonnements_AbonnementId",
                        column: x => x.AbonnementId,
                        principalTable: "abonnements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetailleCommande_Commande_IdCommande",
                        column: x => x.IdCommande,
                        principalTable: "Commande",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commande_ApplicationUserId",
                table: "Commande",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailleCommande_AbonnementId",
                table: "DetailleCommande",
                column: "AbonnementId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailleCommande_IdCommande",
                table: "DetailleCommande",
                column: "IdCommande");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetailleCommande");

            migrationBuilder.DropTable(
                name: "Commande");

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

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDebutAbonnement",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateFintAbonnement",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

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
        }
    }
}
