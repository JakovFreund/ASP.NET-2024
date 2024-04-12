using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Vjezba.Web.Models;

namespace Vjezba.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult FAQ(int? selected = null)
        {
            ViewData["SelectedQuestion"] = selected;
            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Message = "Jednostavan način proslijeđivanja poruke iz Controller -> View.";
            return View();
        }

        ///POST zahtjev isključivo - ne može se napraviti GET zahtjev zbog [HttpPost] parametra
        [HttpPost]
        public IActionResult SubmitQuery(IFormCollection formData)
        {

            var imePrezime = formData["imePrezime"];
            var email = formData["email"];
            var poruka = formData["poruka"];
            var tipPoruke = formData["tipPoruke"];
            var primatiNewsletter = formData["newsletter"].Count > 0 ? "obavijestit ćemo vas" : "nećemo vas obavijestiti";

            var odgovor = $"Poštovani {imePrezime} ({email}) zaprimili smo vašu poruku. " +
                          $"Sadržaj vaše ({tipPoruke}) poruke je: {poruka}. " +
                          $"Također, {primatiNewsletter} o daljnjim promjenama preko newslettera.";

            ViewBag.Odgovor = odgovor;

            return View("ContactSuccess");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
