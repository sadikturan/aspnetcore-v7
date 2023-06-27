using basics.Models;
using Microsoft.AspNetCore.Mvc;

namespace basics.Controllers;

public class CourseController: Controller {

    public IActionResult Details(int? id)
    {
        if(id==null) {
            // return Redirect("/course/list");
            return RedirectToAction("List","Course");
        }

        var kurs = Repository.GetById(id);

        return View(kurs);
    }

    public IActionResult List()
    {
        return View("CourseList", Repository.Courses);
    }

}