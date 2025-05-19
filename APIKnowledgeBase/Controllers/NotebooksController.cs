using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using APIKnowledgeBase.Data;
using APIKnowledgeBase.Models;

namespace APIKnowledgeBase.Controllers
{
    public class NotebooksController : Controller
    {
        private readonly APIKnowledgeBaseContext _context;

        public NotebooksController(APIKnowledgeBaseContext context)
        {
            _context = context;
        }

        // GET: Notebooks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Notebook.ToListAsync());
        }

        // GET: Notebooks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notebook = await _context.Notebook
                .FirstOrDefaultAsync(m => m.NotebookId == id);
            if (notebook == null)
            {
                return NotFound();
            }

            return View(notebook);
        }

        // GET: Notebooks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notebooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NotebookId,Name,Content,CreatedDate,LastModifiedDate,IsFavorite,Version")] Notebook notebook)
        {
            if (ModelState.IsValid)
            {
                notebook.NotebookId = Guid.NewGuid();
                _context.Add(notebook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(notebook);
        }

        // GET: Notebooks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notebook = await _context.Notebook.FindAsync(id);
            if (notebook == null)
            {
                return NotFound();
            }
            return View(notebook);
        }

        // POST: Notebooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("NotebookId,Name,Content,CreatedDate,LastModifiedDate,IsFavorite,Version")] Notebook notebook)
        {
            if (id != notebook.NotebookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notebook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotebookExists(notebook.NotebookId))
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
            return View(notebook);
        }

        // GET: Notebooks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notebook = await _context.Notebook
                .FirstOrDefaultAsync(m => m.NotebookId == id);
            if (notebook == null)
            {
                return NotFound();
            }

            return View(notebook);
        }

        // POST: Notebooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var notebook = await _context.Notebook.FindAsync(id);
            if (notebook != null)
            {
                _context.Notebook.Remove(notebook);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotebookExists(Guid id)
        {
            return _context.Notebook.Any(e => e.NotebookId == id);
        }
    }
}
