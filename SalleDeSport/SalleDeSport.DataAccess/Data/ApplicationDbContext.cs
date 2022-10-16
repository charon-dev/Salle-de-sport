using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalleDeSport.Models;

namespace SalleDeSportWeb.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){ }

        public DbSet<Abonnement> abonnements { get; set; }
        public DbSet<Coach> coachs { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Chariot> chariots { get; set; }
        public DbSet<Activite> activites { get; set; }
        public DbSet<Commande> Commande { get; set; }
        public DbSet<DetailleCommande> DetailleCommande { get; set; }
    }
}
