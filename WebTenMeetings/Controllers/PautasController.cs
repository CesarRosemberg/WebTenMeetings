using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebTenMeetings.Models;

namespace WebTenMeetings.Controllers
{
    public class PautasController : Controller
    {
        private readonly contexto _context;

        public PautasController(contexto context)
        {
            _context = context;
        }

        // GET: Pautas
        public async Task<IActionResult> Index()
        {
              return _context.Pautas != null ? 
                          View(await _context.Pautas.ToListAsync()) :
                          Problem("Entity set 'contexto.Pautas'  is null.");
        }

        // GET: Pautas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pautas == null)
            {
                return NotFound();
            }

            var pauta = await _context.Pautas
                .FirstOrDefaultAsync(m => m.PautaId == id);
            if (pauta == null)
            {
                return NotFound();
            }

            return View(pauta);
        }

        // GET: Pautas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pautas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PautaId,Nome,QtdTotalVotos")] Pauta pauta)
        {
            
                _context.Add(pauta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            return View(pauta);
        }

        // GET: Pautas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pautas == null)
            {
                return NotFound();
            }

            var pauta = await _context.Pautas.FindAsync(id);
            if (pauta == null)
            {
                return NotFound();
            }
            return View(pauta);
        }

        // POST: Pautas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PautaId,Nome,QtdTotalVotos")] Pauta pauta)
        {
            if (id != pauta.PautaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pauta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PautaExists(pauta.PautaId))
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
            return View(pauta);
        }

        // GET: Pautas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pautas == null)
            {
                return NotFound();
            }

            var pauta = await _context.Pautas
                .FirstOrDefaultAsync(m => m.PautaId == id);
            if (pauta == null)
            {
                return NotFound();
            }

            return View(pauta);
        }

        // POST: Pautas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pautas == null)
            {
                return Problem("Entity set 'contexto.Pautas'  is null.");
            }
            var pauta = await _context.Pautas.FindAsync(id);
            if (pauta != null)
            {
                _context.Pautas.Remove(pauta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PautaExists(int id)
        {
          return (_context.Pautas?.Any(e => e.PautaId == id)).GetValueOrDefault();
        }
    }
}
