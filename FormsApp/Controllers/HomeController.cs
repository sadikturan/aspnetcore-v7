using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormsApp.Models;

namespace FormsApp.Controllers;

public class HomeController : Controller
{
    public HomeController()
    {
    }

    public IActionResult Index()
    {
        return View(Repository.Products);
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
