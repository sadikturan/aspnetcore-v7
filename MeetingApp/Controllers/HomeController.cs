using Microsoft.AspNetCore.Mvc;

namespace MeetingApp.Controllers
{
    public class HomeController: Controller 
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}