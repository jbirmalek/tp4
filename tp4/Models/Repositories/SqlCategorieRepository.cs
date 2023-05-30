using Microsoft.EntityFrameworkCore;

namespace tp4.Models.Repositories
{
    public class SqlCategorieRepository 
    {
        private readonly AppDbContext context;
        public SqlCategorieRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Categorie Add(Categorie P)
        {
            context.Categories.Add(P);
            context.SaveChanges();
            return P;
        }
        public Categorie Get(int Id)
        {
            return context.Categories.Find(Id);
        }

        public Categorie GetProduitsByCateg(string cat)
        {
            return context.Categories.Include("Produits").Single(g => g.Nom == cat);
        }
        public Categorie Delete(int Id)
        {
            Categorie P = context.Categories.Find(Id);
            if (P != null)
            {
                context.Categories.Remove(P);
                context.SaveChanges();
            }
            return P;
        }

        public Categorie Delete(Categorie cat)
        {
            Categorie P = context.Categories.Find(cat.CategorieId);
            if (P != null)
            {
                context.Categories.Remove(P);
                context.SaveChanges();
            }
            return P;
        }


        public IList<Categorie> GetAll()
        {
            return context.Categories.OrderBy(x => x.Nom).ToList();
        }

        public Categorie Update(Categorie P)
        {
            var Product =
            context.Categories.Attach(P);
            Product.State = EntityState.Modified;
            context.SaveChanges();
            return P;
        }
    }
}
