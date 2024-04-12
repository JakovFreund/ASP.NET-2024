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

        /// Ova akcija se poziva kada na formi za kontakt kliknemo "Submit"
        /// URL ove akcije je /Home/SubmitQuery, uz POST zahtjev isključivo - ne može se napraviti GET zahtjev zbog [HttpPost] parametra
        [HttpPost]
        public IActionResult SubmitQuery(IFormCollection formData)
        {
            //Ovdje je potrebno obraditi podatke i pospremiti finalni string u ViewBag
            //Kao rezultat se pogled /Views/Home/ContactSuccess.cshtml renderira u "pravi" HTML
            //Kao parametar se predaje naziv cshtml datoteke koju treba obraditi (ne koristi se default vrijednost)
            //Trazenu cshtml datoteku je potrebno samostalno dodati u projekt

            // Čitanje vrijednosti iz forme
            var imePrezime = formData["imePrezime"];
            var email = formData["email"];
            var poruka = formData["poruka"];
            var tipPoruke = formData["tipPoruke"];
            var primatiNewsletter = formData["newsletter"].Count > 0 ? "obavijestit ćemo vas" : "nećemo vas obavijestiti";

            // Sastavljanje odgovora
            var odgovor = $"Poštovani {imePrezime} ({email}) zaprimili smo vašu poruku. " +
                          $"Sadržaj vaše ({tipPoruke}) poruke je: {poruka}. " +
                          $"Također, {primatiNewsletter} o daljnjim promjenama preko newslettera.";

            // Pohrana odgovora u ViewBag za prikaz u sljedećem pogledu
            ViewBag.Odgovor = odgovor;

            // Prikazivanje ContactSuccess.cshtml sa sastavljenim odgovorom
            return View("ContactSuccess");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
