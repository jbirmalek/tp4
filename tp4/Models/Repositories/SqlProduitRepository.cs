using Microsoft.EntityFrameworkCore;

namespace tp4.Models.Repositories
{
    public class SqlProduitRepository 
    {
        
            readonly AppDbContext context;
            public SqlProduitRepository(AppDbContext context)
            {
                this.context = context;
            }
            public IList<Produit> GetAll()
            {
                return context.Produits.OrderBy(x => x.Nom).Include(x => x.Categorie).ToList();
            }
            public IList<Produit> GetAllByCategorie(string cat)
            {
                //return context.Categories.Include("Produits").Single(g => g.Nom == cat);


                return context.Produits.OrderBy(x => x.Nom).Include(x => x.Categorie).Where(x => x.Nom == cat).ToList();
            }
            public Produit Get(int id)
            {
                return context.Produits.Where(x => x.ProduitId == id).Include(x => x.Categorie).SingleOrDefault();
            }
            public Produit Add(Produit p)
            {
                context.Produits.Add(p);
                context.SaveChanges();
                return p;
            }
            public Produit Update(Produit p)
            {
                Produit p1 = context.Produits.Find(p.ProduitId);
                if (p1 != null)
                {
                    p1.Nom = p.Nom;
                    p1.Prix = p.Prix;
                    p1.Categorie = p.Categorie;
                    p1.CategorieId = p.CategorieId;
                    p1.Quantite = p.Quantite;
                    p1.Image = p.Image;
                    context.SaveChanges();
                }
                return p1;
            }
            public Produit Delete(int Id)
            {
                Produit p1 = context.Produits.Find(Id);
                if (p1 != null)
                {
                    context.Produits.Remove(p1);
                    context.SaveChanges();
                }
                return p1;
            }
            public IList<Produit> GetProduitsByCategorieID(int? categorieId)
            {
                return context.Produits.Where(p => p.CategorieId.Equals(categorieId))
                    .OrderBy(p => p.Nom)
                    .Include(pdt => pdt.Categorie).ToList();
            }
            public IList<Produit> FindByName(string name)
            {
                return context.Produits.Where(p => p.Nom.Contains(name)).Include(pdt => pdt.Categorie).ToList();
            }
        }
    }
