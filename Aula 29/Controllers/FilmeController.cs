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
    public class FilmeController : Controller
    {
        private readonly Catalogo _context;

        public FilmeController(Catalogo context)
        {
            _context = context;
        }

        // GET: Filme
        public async Task<IActionResult> Index()
        {
              return _context.Filmes != null ? 
                          View(await _context.Filmes.ToListAsync()) :
                          Problem("Entity set 'Catalogo.Filmes'  is null.");
        }

        // GET: Filme/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Filmes == null)
            {
                return NotFound();
            }

            var filmeModels = await _context.Filmes
                .FirstOrDefaultAsync(m => m.id == id);
            if (filmeModels == null)
            {
                return NotFound();
            }

            return View(filmeModels);
        }

        // GET: Filme/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Filme/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nome,dataLancamento,duracao")] FilmeModels filmeModels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filmeModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filmeModels);
        }

        // GET: Filme/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Filmes == null)
            {
                return NotFound();
            }

            var filmeModels = await _context.Filmes.FindAsync(id);
            if (filmeModels == null)
            {
                return NotFound();
            }
            return View(filmeModels);
        }

        // POST: Filme/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nome,dataLancamento,duracao")] FilmeModels filmeModels)
        {
            if (id != filmeModels.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filmeModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmeModelsExists(filmeModels.id))
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
            return View(filmeModels);
        }

        // GET: Filme/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Filmes == null)
            {
                return NotFound();
            }

            var filmeModels = await _context.Filmes
                .FirstOrDefaultAsync(m => m.id == id);
            if (filmeModels == null)
            {
                return NotFound();
            }

            return View(filmeModels);
        }

        // POST: Filme/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Filmes == null)
            {
                return Problem("Entity set 'Catalogo.Filmes'  is null.");
            }
            var filmeModels = await _context.Filmes.FindAsync(id);
            if (filmeModels != null)
            {
                _context.Filmes.Remove(filmeModels);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmeModelsExists(int id)
        {
          return (_context.Filmes?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
