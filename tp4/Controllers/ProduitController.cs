using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting.Internal;
using tp4.Models;
using tp4.Models.Repositories;
using tp4.Models.ViewModels;

namespace tp4.Controllers
{
    public class ProduitController : Controller
    {
        readonly SqlProduitRepository produitRepository;
        readonly SqlCategorieRepository categorieRepository;
        private readonly IWebHostEnvironment hostingEnvironment;
        public ProduitController(SqlProduitRepository prodRepository, SqlCategorieRepository catRepository, IWebHostEnvironment hostingEnvironment)
        {
            produitRepository=prodRepository;
            categorieRepository=catRepository;
            this.hostingEnvironment = hostingEnvironment;
        }
        // GET: ProduitController
        public ActionResult Index()
        {
            var produits = produitRepository.GetAll();
            return View(produits);

        }

        // GET: ProduitController/Details/5
        public ActionResult Details(int id)
        {
            var produit = produitRepository.Get(id);
            return View(produit);

        }
        // GET: ProduitController/Details/5
        public ActionResult DetailsCat(int id)
        {
            var produit = produitRepository.Get(id);
            return View(produit);

        }

        // GET: ProduitController/Create
        public ActionResult Create()
        {
            ViewBag.CategorieId = new SelectList(categorieRepository.GetAll(), "CategorieId", "Nom");
            return View();
        }

        // POST: ProduitController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel model)
        {
            ViewBag.CategorieId = new SelectList(categorieRepository.GetAll(), "CategorieId", "Nom");

            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                // If the Photo property on the incoming model object is not null, then the user has selected an image to upload.
                if (model.ImagePath != null)
                {
                    // The image must be uploaded to the images folder in wwwroot
                    // To get the path of the wwwroot folder we are using the inject
                    // HostingEnvironment service provided by ASP.NET Core

                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");

                    // To make sure the file name is unique we are appending a new
                    // GUID value and an underscore to the file name

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImagePath.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder

                    model.ImagePath.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                Produit newProd = new Produit
                {
                    Nom = model.Nom,
                    Prix = model.Prix,
                    Quantite = model.Quantite,
                    Categorie = model.Categorie,
                    CategorieId = model.CategorieId,
                    // Store the file name in ImagePath property of the product object
                    // which gets saved to the Products database table
                    Image = uniqueFileName
                };

                produitRepository.Add(newProd);
                return RedirectToAction("details", new { id = newProd.ProduitId });
            }
            return View();
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.CategorieId = new SelectList(categorieRepository.GetAll(), "CategorieId", "Nom");

            Produit prod = produitRepository.Get(id);
            EditViewModel prodEditViewModel = new EditViewModel
            {
                Id = prod.ProduitId,
                Nom = prod.Nom,
                Prix = prod.Prix,
                Quantite = prod.Quantite,
                ExistingImagePath = prod.Image
            };
            return View(prodEditViewModel);

        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            ViewBag.CategorieId = new SelectList(categorieRepository.GetAll(), "CategorieId", "Nom");

            // Check if the provided data is valid, if not rerender the edit view
            // so the user can correct and resubmit the edit form
            if (ModelState.IsValid)
            {
                // Retrieve the product being edited from the database
                Produit prod = produitRepository.Get(model.Id);
                // Update the product object with the data in the model object
                prod.Nom = model.Nom;
                prod.Prix = model.Prix;
                prod.Quantite = model.Quantite;
                prod.Categorie = model.Categorie;
                prod.CategorieId = model.CategorieId;

                // If the user wants to change the photo, a new photo will be
                // uploaded and the Photo property on the model object receives
                // the uploaded photo. If the Photo property is null, user did
                // not upload a new photo and keeps his existing photo
                if (model.ImagePath != null)
                {
                    // If a new photo is uploaded, the existing photo must be
                    // deleted. So check if there is an existing photo and delete
                    if (model.ExistingImagePath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingImagePath);
                        System.IO.File.Delete(filePath);
                    }
                    // Save the new photo in wwwroot/images folder and update
                    // PhotoPath property of the product object which will be
                    // eventually saved in the database
                    prod.Image = ProcessUploadedFile(model);

                }

                // Call update method on the repository service passing it the
                // product object to update the data in the database table
                Produit updatedProduct = produitRepository.Update(prod);

                if (updatedProduct != null)
                    return RedirectToAction("Index");
                else
                    return NotFound();

            }

            return View(model);
        }
        [NonAction]
        private string ProcessUploadedFile(EditViewModel model)
        {
            string uniqueFileName = null;

            if (model.ImagePath != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImagePath.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImagePath.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        // GET: ProduitController/Delete/5
        public ActionResult Delete(int id)
        {
            var produit = produitRepository.Get(id);
            return View(produit);
        }

        // POST: ProduitController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Produit p)
        {
            try
            {
                produitRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
