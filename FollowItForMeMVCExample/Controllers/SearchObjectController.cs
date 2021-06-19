using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FollowItForMeMVCExample.Data;
using FollowItForMeMVCExample.Models;

namespace FollowItForMeMVCExample.Controllers
{
    public class SearchObjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SearchObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SearchObject
        public async Task<IActionResult> Index()
        {
            return View(await _context.SearchObject.ToListAsync());
        }

        // GET: ShowSearchForm
        public IActionResult ShowSearchForm()
        {
            return View();
        }


        //POST: SearchObject/ShowSearchResults
        [HttpPost]
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.SearchObject.Where(obj => obj.Name.Contains(SearchPhrase)).ToListAsync());
        } 


        // GET: SearchObject/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var searchObject = await _context.SearchObject
                .FirstOrDefaultAsync(m => m.Id == id);
            if (searchObject == null)
            {
                return NotFound();
            }

            return View(searchObject);
        }

        // GET: SearchObject/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SearchObject/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Type,Name,SearchCriteria,Description")] SearchObject searchObject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(searchObject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(searchObject);
        }

        // GET: SearchObject/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var searchObject = await _context.SearchObject.FindAsync(id);
            if (searchObject == null)
            {
                return NotFound();
            }
            return View(searchObject);
        }

        // POST: SearchObject/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Type,Name,SearchCriteria,Description")] SearchObject searchObject)
        {
            if (id != searchObject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(searchObject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SearchObjectExists(searchObject.Id))
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
            return View(searchObject);
        }

        // GET: SearchObject/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var searchObject = await _context.SearchObject
                .FirstOrDefaultAsync(m => m.Id == id);
            if (searchObject == null)
            {
                return NotFound();
            }

            return View(searchObject);
        }

        // POST: SearchObject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var searchObject = await _context.SearchObject.FindAsync(id);
            _context.SearchObject.Remove(searchObject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SearchObjectExists(int id)
        {
            return _context.SearchObject.Any(e => e.Id == id);
        }
    }
}
