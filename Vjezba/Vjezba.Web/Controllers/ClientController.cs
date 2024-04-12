using Microsoft.AspNetCore.Mvc;
using Vjezba.Web.Mock;

namespace Vjezba.Web.Controllers
{
    public class ClientController : Controller
    {

        public IActionResult Index(string query)
        {
            var clients = MockClientRepository.Instance.All();

            return View(clients.ToList());
        }

        [HttpPost]
        public IActionResult Index(string queryName, string queryAddress)
        {
            var clients = MockClientRepository.Instance.All();

            if (!string.IsNullOrEmpty(queryName))
            {
                clients = clients.Where(c =>
                    c.FirstName != null && c.FirstName.Contains(queryName, StringComparison.OrdinalIgnoreCase)
                );
            }

            if (!string.IsNullOrEmpty(queryAddress))
            {
                clients = clients.Where(c =>
                    c.Address != null && c.Address.Contains(queryAddress, StringComparison.OrdinalIgnoreCase)
                );
            }

            return View(clients.ToList());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Client client = MockClientRepository.Instance.FindByID(id.Value);

            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

    }
}
