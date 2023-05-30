using Microsoft.EntityFrameworkCore;

namespace tp4.Models.Repositories
{
    public class SqlPanierRepository : IRepository<Panier>
    {
        private readonly AppDbContext context;
        public SqlPanierRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Panier Add(Panier P)
        {
            context.Paniers.Add(P);
            context.SaveChanges();
            return P;
        }
        public Panier Get(int Id)
        {
            return context.Paniers.Find(Id);
        }


        public Panier Delete(int Id)
        {
            Panier P = context.Paniers.Find(Id);
            if (P != null)
            {
                context.Paniers.Remove(P);
                context.SaveChanges();
            }
            return P;
        }

        public Panier Delete(Panier cat)
        {
            Panier P = context.Paniers.Find(cat.PanierId);
            if (P != null)
            {
                context.Paniers.Remove(P);
                context.SaveChanges();
            }
            return P;
        }


        public IEnumerable<Panier> GetAll()
        {
            return context.Paniers;
        }






        public Panier Update(Panier P)
        {
            var Product =
            context.Paniers.Attach(P);
            Product.State = EntityState.Modified;
            context.SaveChanges();
            return P;
        }
    }
}
