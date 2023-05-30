using Microsoft.EntityFrameworkCore;

namespace tp4.Models
{
	public class AppDbContext : DbContext 
	{
		public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
		{ 
		}
		public DbSet<Produit> Produits { get; set; }
		public DbSet<Categorie> Categories { get; set; }
		public DbSet<Commande> Commandes { get; set; }
		public DbSet<Panier> Paniers { get; set; }

	}
}
