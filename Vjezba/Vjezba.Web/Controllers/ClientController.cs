using Microsoft.AspNetCore.Mvc;
using Vjezba.Web.Mock;

namespace Vjezba.Web.Controllers
{
    public class ClientController : Controller
    {

        public IActionResult Index(string query)
        {
            var clients = MockClientRepository.Instance.All();

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
