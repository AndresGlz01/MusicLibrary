using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicLibrary.Models;

namespace MusicLibrary.Controllers
{
    public class ArtistaController : Controller
    {
        private readonly MusicLibraryContext _context;

        public ArtistaController(MusicLibraryContext context)
        {
            _context = context;
        }

        // GET: Artista
        public async Task<IActionResult> Index()
        {
            var musicLibraryContext = _context.Artista.Include(a => a.IdGeneroNavigation).Include("Albums");
            return View(await musicLibraryContext.ToListAsync());
        }

        // GET: Artista/Details/5
        public async Task<IActionResult> Detalles(int? id)
        {
            if (id == null || _context.Artista == null)
            {
                return NotFound();
            }

            var artistum = await _context.Artista
                .Include(a => a.IdGeneroNavigation)
                .Include("Albums")
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artistum == null)
            {
                return NotFound();
            }

            ViewBag.albumsCount = artistum.Albums.Count;

            return View(artistum);
        }

        // GET: Artista/Create
        public IActionResult Crear()
        {
            ViewData["IdGenero"] = new SelectList(_context.Generos, "Id", "Nombre");
            return View();
        }

        // POST: Artista/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Crear([Bind("Id,Nombre,Nacionalidad,FechaNacimiento,IdGenero")] Artistum artistum)
        {
            _context.Add(artistum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Artista/Edit/5
        public async Task<IActionResult> Modificar (int? id)
        {
            if (id == null || _context.Artista == null)
            {
                return NotFound();
            }

            var artistum = await _context.Artista.FindAsync(id);
            if (artistum == null)
            {
                return NotFound();
            }
            ViewData["IdGenero"] = new SelectList(_context.Generos, "Id", "Nombre", artistum.IdGenero);
            return View(artistum);
        }

        // POST: Artista/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modificar(int id, [Bind("Id,Nombre,Nacionalidad,FechaNacimiento,IdGenero")] Artistum artistum)
        {
            if (id != artistum.Id)
            {
                return NotFound();
            }


            try
            {
                _context.Update(artistum);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistumExists(artistum.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Artista/Delete/5
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null || _context.Artista == null)
            {
                return NotFound();
            }

            var artistum = await _context.Artista
                .Include(a => a.IdGeneroNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artistum == null)
            {
                return NotFound();
            }

            return View(artistum);
        }

        // POST: Artista/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            if (_context.Artista == null)
            {
                return Problem("Entity set 'MusicLibraryContext.Artista'  is null.");
            }
            var artistum = await _context.Artista.FindAsync(id);
            if (artistum != null)
            {
                _context.Artista.Remove(artistum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistumExists(int id)
        {
            return (_context.Artista?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
