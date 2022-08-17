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
    public class AlbumController : Controller
    {
        private readonly MusicLibraryContext _context;

        public AlbumController(MusicLibraryContext context)
        {
            _context = context;
        }

        // GET: Album
        public async Task<IActionResult> Index()
        {
            var musicLibraryContext = _context.Albums.Include(a => a.IdArtistaNavigation);
            return View(await musicLibraryContext.ToListAsync());
        }

        // GET: Album/Details/5
        public async Task<IActionResult> Detalles(int? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(a => a.IdArtistaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (album == null)
            {
                return NotFound();
            }

            ViewBag.albums = _context.Albums.Where(x => x.Id != id && x.IdArtista == album.IdArtista);
            ViewBag.albumsCount = _context.Albums.Where(x => x.Id != id && x.IdArtista == album.IdArtista).Count();

            return View(album);
        }

        // GET: Album/Create
        public IActionResult Crear()
        {
            ViewData["IdArtista"] = new SelectList(_context.Artista, "Id", "Nombre");
            return View();
        }

        // POST: Album/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Crear([Bind("Id,IdArtista,Nombre,FechaLanzamiento")] Album album)
        {
            _context.Add(album);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Album/Edit/5
        public async Task<IActionResult> Modificar(int? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            ViewData["IdArtista"] = new SelectList(_context.Artista, "Id", "Nombre", album.IdArtista);
            return View(album);
        }

        // POST: Album/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modificar(int id, [Bind("Id,IdArtista,Nombre,FechaLanzamiento")] Album album)
        {
            if (id != album.Id)
            {
                return NotFound();
            }
            try
            {
                _context.Update(album);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumExists(album.Id))
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

        // GET: Album/Delete/5
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(a => a.IdArtistaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Album/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            if (_context.Albums == null)
            {
                return Problem("Entity set 'MusicLibraryContext.Albums'  is null.");
            }
            var album = await _context.Albums.FindAsync(id);
            if (album != null)
            {
                _context.Albums.Remove(album);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumExists(int id)
        {
            return (_context.Albums?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
