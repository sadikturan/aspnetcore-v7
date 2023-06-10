using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using basics.Models;

namespace basics.Controllers;

// localhost              => home/index
// localhost/home         => home/index
// localhost/home/index   => home/index
public class HomeController : Controller
{
    public string Index()
    {
        return "home/index";
    }

    public string Contact()
    {
        return "home/contact";
    }

}
