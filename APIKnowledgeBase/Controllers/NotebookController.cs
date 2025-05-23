﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIKnowledgeBase.Data;
using APIKnowledgeBase.Models;

namespace APIKnowledgeBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotebookController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NotebookController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Notebook
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notebook>>> GetNotebooks()
        {
            return await _context.Notebooks.ToListAsync();
        }

        // GET: api/Notebook/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notebook>> GetNotebook(Guid id)
        {
            var notebook = await _context.Notebooks.FindAsync(id);

            if (notebook == null)
            {
                return NotFound();
            }

            return notebook;
        }

        // PUT: api/Notebook/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotebook(Guid id, Notebook notebook)
        {
            if (id != notebook.NotebookId)
            {
                return BadRequest();
            }

            _context.Entry(notebook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotebookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Notebook
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Notebook>> PostNotebook(Notebook notebook)
        {
            _context.Notebooks.Add(notebook);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotebook", new { id = notebook.NotebookId }, notebook);
        }

        // DELETE: api/Notebook/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotebook(Guid id)
        {
            var notebook = await _context.Notebooks.FindAsync(id);
            if (notebook == null)
            {
                return NotFound();
            }

            _context.Notebooks.Remove(notebook);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotebookExists(Guid id)
        {
            return _context.Notebooks.Any(e => e.NotebookId == id);
        }
    }
}
