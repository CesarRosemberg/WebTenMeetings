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
    public class AssembleiasController : Controller
    {
        private readonly contexto _context;

        public AssembleiasController(contexto context)
        {
            _context = context;
        }

        // GET: Assembleias
        public async Task<IActionResult> Index()
        {
              return _context.Assembleias != null ? 
                          View(await _context.Assembleias.ToListAsync()) :
                          Problem("Entity set 'contexto.Assembleias'  is null.");
        }

        // GET: Assembleias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Assembleias == null)
            {
                return NotFound();
            }

            var assembleia = await _context.Assembleias
                .FirstOrDefaultAsync(m => m.AssembleiaId == id);
            if (assembleia == null)
            {
                return NotFound();
            }

            return View(assembleia);
        }

        // GET: Assembleias/Create
        public async Task<IActionResult> Create()
        {
            
            
            List<Pauta> pautas = await _context.Pautas.ToListAsync();
            ViewData["Pautas"] = pautas;
            return View();
        }

        // POST: Assembleias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssembleiaId,Nome,Status")] Assembleia assembleia)
        {
                
                
                _context.Add(assembleia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            return View(assembleia);
        }

        // GET: Assembleias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Assembleias == null)
            {
                return NotFound();
            }

            var assembleia = await _context.Assembleias.FindAsync(id);
            if (assembleia == null)
            {
                return NotFound();
            }
            return View(assembleia);
        }

        // POST: Assembleias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssembleiaId,Nome,Status")] Assembleia assembleia)
        {
            if (id != assembleia.AssembleiaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assembleia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssembleiaExists(assembleia.AssembleiaId))
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
            return View(assembleia);
        }

        // GET: Assembleias/Delete/5
      
            public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Assembleias == null)
            {
                return NotFound();
            }

            var assembleia = await _context.Assembleias
                .FirstOrDefaultAsync(m => m.AssembleiaId == id);
            if (assembleia == null)
            {
                return NotFound();
            }

            return View(assembleia);
        }

        // POST: Assembleias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Assembleias == null)
            {
                return Problem("Entity set 'contexto.Assembleias'  is null.");
            }
            var assembleia = await _context.Assembleias.FindAsync(id);
            if (assembleia != null)
            {
                _context.Assembleias.Remove(assembleia);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssembleiaExists(int id)
        {
          return (_context.Assembleias?.Any(e => e.AssembleiaId == id)).GetValueOrDefault();
        }
        public async Task<IActionResult> AdicionarPauta(int? id)
        {

            var rep = new BaseGenericRepository();

            if (id == null || _context.Assembleias == null)
            {
                return NotFound();
            }

            var assembleia = await _context.Assembleias
                .FirstOrDefaultAsync(m => m.AssembleiaId == id);
            if (assembleia == null)
            {
                return NotFound();
            }

            return View(assembleia);
        }

    }
}
