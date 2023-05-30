using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tp4.Models.Repositories;
using tp4.Models;

namespace tp4.Controllers
{
    public class CategorieController : Controller
    {
        readonly IRepository<Categorie> categorieRepository;
        public CategorieController(IRepository<Categorie> categorieRepository)
        {
            this.categorieRepository = categorieRepository;
        }

        // GET: CategorieController
        public ActionResult Index()
        {
            var cats = categorieRepository.GetAll();
            return View(cats);
        }

        // GET: CategorieController/Details/5
        public ActionResult Details(int id)
        {
            var cat = categorieRepository.Get(id);
            return View(cat);
        }

        // GET: CategorieController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategorieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection, Categorie cat)
        {
            try
            {
                categorieRepository.Add(cat);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategorieController/Edit/5
        public ActionResult Edit(int id)
        {
            var cat = categorieRepository.Get(id);
            return View(cat);
        }

        // POST: CategorieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection,Categorie cat)
        {
            try
            {
                categorieRepository.Update(cat);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategorieController/Delete/5
        public ActionResult Delete(int id)
        {
            var cat = categorieRepository.Get(id);
            return View(cat);
        }

        // POST: CategorieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection,Categorie cat)
        {
            try
            {
                categorieRepository.Delete(cat);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
