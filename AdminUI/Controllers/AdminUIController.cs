using Microsoft.AspNetCore.Mvc;
using AuthenticationDll.Models;
using AdminUI.Services;

namespace AdminUI.Controllers
{
    public class AdminUIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
