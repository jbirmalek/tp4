using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tp4.Models.Repositories;
using tp4.Models;
using tp4.Models.Help;

namespace tp4.Controllers
{
    public class PanierController : Controller
    {
        readonly SqlProduitRepository produitRepository;
        public PanierController(SqlProduitRepository produitRepository)
        {
            this.produitRepository = produitRepository;
        }
        public ActionResult Index()
        {
            ViewBag.Liste = ListeCart.Instance.Items;
            ViewBag.total = ListeCart.Instance.GetSubTotal();
            return View();
        }
        public ActionResult AjouterProduit(int id)
        {
            Produit pp = produitRepository.Get(id);
            ListeCart.Instance.AddItem(pp);
            ViewBag.Liste = ListeCart.Instance.Items;
            ViewBag.total = ListeCart.Instance.GetSubTotal();
            return View();
        }
        [HttpPost]
        public ActionResult PlusProduit(int id)
        {
            Produit pp = produitRepository.Get(id);
            ListeCart.Instance.AddItem(pp);
            Item trouve = null;
            foreach (Item a in ListeCart.Instance.Items)
            {
                if (a.Prod.ProduitId == pp.ProduitId)
                    trouve = a;
            }
            var results = new
            {
                ct = 1,
                Total = ListeCart.Instance.GetSubTotal(),
                Quatite = trouve.quantite,
                TotalRow = trouve.TotalPrice
            };
            return Json(results);
        }
        [HttpPost]
        public ActionResult MoinsProduit(int id)
        {
            Produit pp = produitRepository.Get(id);
            ListeCart.Instance.SetLessOneItem(pp);
            Item trouve = null;
            foreach (Item a in ListeCart.Instance.Items)
            {
                if (a.Prod.ProduitId == pp.ProduitId)
                    trouve = a;
            }
            if (trouve != null)
            {
                var results = new
                {
                    Total = ListeCart.Instance.GetSubTotal(),
                    Quatite = trouve.quantite,
                    TotalRow = trouve.TotalPrice,
                    ct = 1
                };
                return Json(results);
            }
            else
            {
                var results = new
                {
                    ct = 0
                };
                return Json(results);
            }
            return null;
        }
        public ActionResult CheckOut()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CheckOut(FormCollection collection)
        {
            ListeCart.Instance.Items.Clear();
            ViewBag.Message = "Commande effectuée zvec succès";
            return View();
        }
        [HttpPost]
        public ActionResult SupprimerProduit(int id)
        {
            Produit pp = produitRepository.Get(id);
            ListeCart.Instance.RemoveItem(pp);
            var results = new
            {
                Total = ListeCart.Instance.GetSubTotal(),
            };
            return Json(results);
        }
    }

}
