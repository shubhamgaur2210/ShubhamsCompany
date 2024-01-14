using Microsoft.AspNetCore.Mvc;
using ShubhamsCompany.Models;
using System.Diagnostics;

namespace ShubhamsCompany.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
