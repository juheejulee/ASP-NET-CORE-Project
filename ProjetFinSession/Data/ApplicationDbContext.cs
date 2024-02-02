using Microsoft.EntityFrameworkCore;
using ProjetFinSession.Models;



namespace ProjetFinSession.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<Service> Services { get; set; }

    }
}
