using Microsoft.AspNetCore.Mvc;

namespace MeetingApp.Controllers
{
    public class MeetingController : Controller
    {
        public string Index()
        {
            return "meeting/index";
        }
    }
}