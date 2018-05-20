using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using vega;
using vega.Models;

namespace vega.Controllers
{
    public class makesController : Controller
    {
        private readonly VegaDbContext _context;

        public makesController(VegaDbContext context)
        {
            _context = context;
        }

        // GET: makes
        public async Task<IActionResult> Index()
        {
            return View(await _context.make.ToListAsync());
        }

        // GET: makes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var make = await _context.make
                .SingleOrDefaultAsync(m => m.ID == id);
            if (make == null)
            {
                return NotFound();
            }

            return View(make);
        }

        // GET: makes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: makes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] make make)
        {
            if (ModelState.IsValid)
            {
                _context.Add(make);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(make);
        }

        // GET: makes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var make = await _context.make.SingleOrDefaultAsync(m => m.ID == id);
            if (make == null)
            {
                return NotFound();
            }
            return View(make);
        }

        // POST: makes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] make make)
        {
            if (id != make.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(make);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!makeExists(make.ID))
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
            return View(make);
        }

        // GET: makes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var make = await _context.make
                .SingleOrDefaultAsync(m => m.ID == id);
            if (make == null)
            {
                return NotFound();
            }

            return View(make);
        }

        // POST: makes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var make = await _context.make.SingleOrDefaultAsync(m => m.ID == id);
            _context.make.Remove(make);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool makeExists(int id)
        {
            return _context.make.Any(e => e.ID == id);
        }
    }
}
