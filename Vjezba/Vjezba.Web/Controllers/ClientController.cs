using Microsoft.AspNetCore.Mvc;
using Vjezba.Web.Mock;

namespace Vjezba.Web.Controllers
{
    public class ClientController : Controller
    {
        // Ostale akcije...

        public IActionResult Index(string query)
        {
            var clients = MockClientRepository.Instance.All(); // Koristite .ToList() ovdje

            if (!string.IsNullOrEmpty(query))
            {
                clients = clients.Where(c =>
                    (c.FirstName != null && c.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                    (c.LastName != null && c.LastName.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                    (c.Email != null && c.Email.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                    (c.Address != null && c.Address.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                    (c.City != null && c.City.Name != null && c.City.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
                );
            }

            // Proslijedi listu klijenata u View
            return View(clients.ToList());
        }

        public IActionResult Details(int? id)
        {
            // Provjerite je li id null ili nije
            if (id == null)
            {
                return NotFound(); // Vratite NotFound rezultat ako je id null
            }

            // Dohvat klijenta prema ID-u
            Client client = MockClientRepository.Instance.FindByID(id.Value);

            if (client == null)
            {
                return NotFound(); // Vratite NotFound rezultat ako klijent nije pronađen
            }

            return View(client); // Proslijedite klijenta kao model u View
        }

    }
}
