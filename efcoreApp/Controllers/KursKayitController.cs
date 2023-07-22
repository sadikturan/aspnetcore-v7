using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class KursKayitController:Controller
    {
        private readonly DataContext _context;
        public KursKayitController(DataContext context)
        {   
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        } 

        public async Task<IActionResult> Create()
        {
            ViewBag.Ogrenciler = new SelectList(await _context.Ogrenciler.ToListAsync(), "OgrenciId","OgrenciAd");
            ViewBag.Kurslar = new SelectList(await _context.Kurslar.ToListAsync(), "KursId","Baslik");

            return View();
        }

    }
}