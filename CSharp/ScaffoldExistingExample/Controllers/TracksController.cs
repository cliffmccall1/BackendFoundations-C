using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScaffoldExistingExample.Models;

namespace ScaffoldExistingExample.Controllers {
    public class TracksController : Controller {
        private readonly ApplicationDbContext _context;

        public TracksController (ApplicationDbContext context) {
            _context = context;
        }

        // GET: Tracks
        public async Task<IActionResult> Index () {
            return View (await _context.tracks.Take (10).ToListAsync ());
        }

        // GET: Tracks/Details/5
        public async Task<IActionResult> Details (long? id) {
            if (id == null) {
                return NotFound ();
            }

            var tracks = await _context.tracks
                .FirstOrDefaultAsync (m => m.TrackId == id);
            if (tracks == null) {
                return NotFound ();
            }

            return View (tracks);
        }

        // GET: Tracks/Create
        public IActionResult Create () {
            return View ();
        }

        // POST: Tracks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ([Bind ("TrackId,Name,AlbumId,MediaTypeId,GenreId,Composer,Milliseconds,Bytes,UnitPrice")] tracks tracks) {
            if (ModelState.IsValid) {
                _context.Add (tracks);
                await _context.SaveChangesAsync ();
                return RedirectToAction (nameof (Index));
            }
            return View (tracks);
        }

        // GET: Tracks/Edit/5
        public async Task<IActionResult> Edit (long? id) {
            if (id == null) {
                return NotFound ();
            }

            var tracks = await _context.tracks.FindAsync (id);
            if (tracks == null) {
                return NotFound ();
            }
            return View (tracks);
        }

        // POST: Tracks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (long id, [Bind ("TrackId,Name,AlbumId,MediaTypeId,GenreId,Composer,Milliseconds,Bytes,UnitPrice")] tracks tracks) {
            if (id != tracks.TrackId) {
                return NotFound ();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update (tracks);
                    await _context.SaveChangesAsync ();
                } catch (DbUpdateConcurrencyException) {
                    if (!tracksExists (tracks.TrackId)) {
                        return NotFound ();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction (nameof (Index));
            }
            return View (tracks);
        }

        // GET: Tracks/Delete/5
        public async Task<IActionResult> Delete (long? id) {
            if (id == null) {
                return NotFound ();
            }

            var tracks = await _context.tracks
                .FirstOrDefaultAsync (m => m.TrackId == id);
            if (tracks == null) {
                return NotFound ();
            }

            return View (tracks);
        }

        // POST: Tracks/Delete/5
        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed (long id) {
            var tracks = await _context.tracks.FindAsync (id);
            _context.tracks.Remove (tracks);
            await _context.SaveChangesAsync ();
            return RedirectToAction (nameof (Index));
        }

        private bool tracksExists (long id) {
            return _context.tracks.Any (e => e.TrackId == id);
        }
    }
}