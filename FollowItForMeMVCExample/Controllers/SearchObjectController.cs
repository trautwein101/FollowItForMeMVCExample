using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FollowItForMeMVCExample.Data;
using FollowItForMeMVCExample.Models;
using Microsoft.AspNetCore.Authorization;

namespace FollowItForMeMVCExample.Controllers
{
    public class SearchObjectController : Controller
    {
        private readonly ApplicationDbContext _context;
        [BindProperty] 
        public SearchObject searchObject { get; set; }
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
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: SearchObject/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
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
        [Authorize]
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

        //This is just better binding 6/30
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert()
        {

            if (ModelState.IsValid)
            {
                //searchObject = await _context.SearchObject.FirstOrDefaultAsync(m => m.Id == id);

                if (searchObject.Id == 0)
                {
                    return NotFound();
                }
                else
                {
                    _context.Update(searchObject);
                    await _context.SaveChangesAsync();

                }
            }


            return RedirectToAction("Index");
            // return RedirectToAction(nameof(Index));
        }


        // POST: SearchObject/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Type,Name,SearchCriteria,Description")] SearchObject searchObject)
        //{
        //    if (id != searchObject.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(searchObject);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!SearchObjectExists(searchObject.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(searchObject);
        //}


        //Using asp helpers
        // POST: SearchObject/Delete/5
        [Authorize]
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

        #region API Calls

        //Using javascript validation
        // GET: SearchObject/Delete/5

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
 
            var searchObject = await _context.SearchObject
                .FirstOrDefaultAsync(m => m.Id == id);
            if (searchObject == null)
            {
                //return NotFound();
                return Json(new { success = false, message = "Error while Deleting" });
            }

                _context.SearchObject.Remove(searchObject);
                await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Delete successful" });
            
        }
        #endregion




    }
}
