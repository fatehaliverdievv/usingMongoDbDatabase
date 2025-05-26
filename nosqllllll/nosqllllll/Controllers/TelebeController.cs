using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using nosqllllll.Models;
using nosqllllll.Services;
using System.Drawing.Printing;


namespace nosqllllll.Controllers
{
    public class TelebeController : Controller
    {
        private readonly MongoDbService _mongoService;
        public TelebeController(MongoDbService mongoService) 
        {
            _mongoService = mongoService;
        }
        public ActionResult Index(string search,int page = 1)
        {
            int pageSize = 10;
            var telebeler = _mongoService.Get();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                telebeler = telebeler.Where(t =>
                    (!string.IsNullOrEmpty(t.Ad) && t.Ad.ToLower().Contains(search)) ||
                    (!string.IsNullOrEmpty(t.Soyad) && t.Soyad.ToLower().Contains(search)) ||
                    (!string.IsNullOrEmpty(t.Qrup) && t.Qrup.ToLower().Contains(search))
                ).ToList();
            }
            int totalCount = telebeler.Count();
            var telebelerPage = telebeler.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            ViewBag.Search = search;

            return View(telebelerPage);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Telebe telebe)
        {
            if (ModelState.IsValid)
            {
                _mongoService.Create(telebe);
                return RedirectToAction("Index");
            }
            return View(telebe); 
        }
        public IActionResult Edit(ObjectId id)
        {
            var telebe = _mongoService.Get(id);
            if (telebe == null)
                return NotFound();
            return View(telebe);
        }

        [HttpPost]
        public IActionResult Edit(Telebe telebe)
        {
            if (ModelState.IsValid)
            {
                _mongoService.Update(telebe);
                return RedirectToAction("Index");
            }
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage); // CMD-də görmək üçün
            }
            return View(telebe);
        }

        public IActionResult Delete(ObjectId id)
        {
            var telebe = _mongoService.Get(id);
            if (telebe == null)
                return NotFound();

            _mongoService.Delete(id); 
            return RedirectToAction("Index");
        }
    }
}
