using basics.Models;
using Microsoft.AspNetCore.Mvc;

namespace basics.Controllers;

// course
public class CourseController: Controller {

    // course
    // course/index
    public IActionResult Index()
    {
        var kurs = new Course();

        kurs.Id = 1;
        kurs.Title = "Aspnet core kursu";
        kurs.Description = "Güzel bir kurs";

        return View(kurs);
    }

    // course/list
    public IActionResult List()
    {
        return View("CourseList");
    }

}