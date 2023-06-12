using basics.Models;
using Microsoft.AspNetCore.Mvc;

namespace basics.Controllers;

public class CourseController: Controller {

    public IActionResult Index()
    {
        var kurs = new Course();

        kurs.Id = 1;
        kurs.Title = "Aspnet core kursu";
        kurs.Description = "Güzel bir kurs";

        return View(kurs);
    }

    public IActionResult List()
    {
        var kurslar = new List<Course>()
        {
            new Course() { Id = 1, Title = "aspnet kursu", Description = "güzel bir kurs", Image="1.jpg"},
            new Course() { Id = 2, Title = "php kursu", Description = "güzel bir kurs", Image="2.jpg"},
            new Course() { Id = 3, Title = "django kursu", Description = "güzel bir kurs", Image="3.jpg"}
        };
        return View("CourseList", kurslar);
    }

}