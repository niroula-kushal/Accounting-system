﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ams.Data;
using Ams.Models;

namespace Ams.Controllers
{
    public class ReceivablesController : Controller
    {
        private readonly AppDbContext _context;

        public ReceivablesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Receivables
        public async Task<IActionResult> Index()
        {
              return _context.receivables != null ? 
                          View(await _context.receivables.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.receivables'  is null.");
        }

        // GET: Receivables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.receivables == null)
            {
                return NotFound();
            }

            var receivable = await _context.receivables
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receivable == null)
            {
                return NotFound();
            }

            return View(receivable);
        }

        // GET: Receivables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Receivables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,date,amount,ReceivableleLedger,remarks,user_id,rec_status")] Receivable receivable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receivable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(receivable);
        }

        // GET: Receivables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.receivables == null)
            {
                return NotFound();
            }

            var receivable = await _context.receivables.FindAsync(id);
            if (receivable == null)
            {
                return NotFound();
            }
            return View(receivable);
        }

        // POST: Receivables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,date,amount,ReceivableleLedger,remarks,user_id,rec_status")] Receivable receivable)
        {
            if (id != receivable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receivable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceivableExists(receivable.Id))
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
            return View(receivable);
        }

        // GET: Receivables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.receivables == null)
            {
                return NotFound();
            }

            var receivable = await _context.receivables
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receivable == null)
            {
                return NotFound();
            }

            return View(receivable);
        }

        // POST: Receivables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.receivables == null)
            {
                return Problem("Entity set 'AppDbContext.receivables'  is null.");
            }
            var receivable = await _context.receivables.FindAsync(id);
            if (receivable != null)
            {
                _context.receivables.Remove(receivable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceivableExists(int id)
        {
          return (_context.receivables?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}