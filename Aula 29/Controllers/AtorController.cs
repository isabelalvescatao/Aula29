using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aula_29.Models;

namespace Aula_29.Controllers
{
    public class AtorController : Controller
    {
        private readonly Catalogo _context;

        public AtorController(Catalogo context)
        {
            _context = context;
        }

        // GET: Ator
        public async Task<IActionResult> Index()
        {
              return _context.Atores != null ? 
                          View(await _context.Atores.ToListAsync()) :
                          Problem("Entity set 'Catalogo.Atores'  is null.");
        }

        // GET: Ator/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Atores == null)
            {
                return NotFound();
            }

            var atorModels = await _context.Atores
                .FirstOrDefaultAsync(m => m.id == id);
            if (atorModels == null)
            {
                return NotFound();
            }

            return View(atorModels);
        }

        // GET: Ator/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ator/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nome,sobrenome")] AtorModels atorModels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(atorModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(atorModels);
        }

        // GET: Ator/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Atores == null)
            {
                return NotFound();
            }

            var atorModels = await _context.Atores.FindAsync(id);
            if (atorModels == null)
            {
                return NotFound();
            }
            return View(atorModels);
        }

        // POST: Ator/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nome,sobrenome")] AtorModels atorModels)
        {
            if (id != atorModels.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(atorModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AtorModelsExists(atorModels.id))
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
            return View(atorModels);
        }

        // GET: Ator/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Atores == null)
            {
                return NotFound();
            }

            var atorModels = await _context.Atores
                .FirstOrDefaultAsync(m => m.id == id);
            if (atorModels == null)
            {
                return NotFound();
            }

            return View(atorModels);
        }

        // POST: Ator/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Atores == null)
            {
                return Problem("Entity set 'Catalogo.Atores'  is null.");
            }
            var atorModels = await _context.Atores.FindAsync(id);
            if (atorModels != null)
            {
                _context.Atores.Remove(atorModels);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AtorModelsExists(int id)
        {
          return (_context.Atores?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
