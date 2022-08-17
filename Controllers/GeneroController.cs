using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLibrary.Models;
using System.Linq;

namespace MusicLibrary.Controllers
{
    public class GeneroController : Controller
    {
        private MusicLibraryContext context;
        public GeneroController(MusicLibraryContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var temp = context.Generos.Include("Artista").ToList();
            
            return View(model:temp);
        }

        [HttpGet]
        public IActionResult Detalles(int id)
        {
            var genero = context.Generos
              .Include(i => i.Artista)
              .FirstOrDefault(x => x.Id == id);

            ViewBag.Artistas = context.Artista.Where(context => context.IdGenero == id).ToList();

            return View(model:genero);
        }

        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            var genero = context.Generos.Find(id);
            return View(model: genero);
        }
        [HttpPost]
        public IActionResult EliminarConfirmado(int id)
        {
            context.Generos.Remove(context.Generos.Find(id));
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Modificar(int id)
        {
            var genero = context.Generos.Find(id);
            return View(model: genero);
        }
        [HttpPost]
        public IActionResult Modificar(int id, [Bind("Id","Nombre")] Genero genero)
        {
            if (id != genero.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            genero.Id = id;
            context.Update(genero);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Crear([Bind("Nombre")] Genero genero)
        {
            context.Add(genero);
            var temp = context.Generos.ToList();
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
