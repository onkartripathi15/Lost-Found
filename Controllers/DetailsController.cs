using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LostOrFound.Models;
using Microsoft.AspNetCore.Authorization;

namespace LostOrFound.Controllers
{
    [Authorize]
    public class DetailsController : Controller
    {
        private readonly DetailsDbContext _context;

        public DetailsController(DetailsDbContext context)
        {
            _context = context;
        }

        // GET: Details
        public async Task<IActionResult> Index(
     string sortOrder,
     string currentFilter,
     string searchString,
     string filterString,
     int? pageNumber)
        {

            ViewBag.options = new List<string>() { "ItemLost", "ItemFound" };
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var details = from s in _context.Details
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
               details = details.Where(s => s.FirstName.Contains(searchString)
                                       || s.LastName.Contains(searchString) || s.ItemDetails.Contains(searchString));
            }
            ViewData["CurrentFilter1"] = filterString;

            if (!String.IsNullOrEmpty(filterString))
            {
                details = details.Where(s => s.ItemDetails.Contains(filterString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    details = details.OrderByDescending(s => s.FirstName);
                    break;
                case "Date":
                    details = details.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    details = details.OrderByDescending(s => s.Date);
                    break;
                default:
                    details = details.OrderBy(s => s.FirstName);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Details>.CreateAsync(details.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Details/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var details = await _context.Details
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (details == null)
            {
                return NotFound();
            }

            return View(details);
        }

        // GET: Details/Create
        public IActionResult Create()
        {
            ViewBag.options = new List<string>() { "ItemLost", "ItemFound" };
            return View();
        }

        // POST: Details/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,FirstName,LastName,ItemDetails,Contact,City,State,Status,Date,Time")] Details details)
        {
            if (ModelState.IsValid)
            {
                _context.Add(details);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(details);
        }

        // GET: Details/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var details = await _context.Details.FindAsync(id);
            if (details == null)
            {
                return NotFound();
            }
            return View(details);
        }

        // POST: Details/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,FirstName,LastName,ItemDetails,Contact,City,State,Status,Date,Time")] Details details)
        {
            if (id != details.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(details);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetailsExists(details.UserId))
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
            return View(details);
        }

        // GET: Details/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var details = await _context.Details
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (details == null)
            {
                return NotFound();
            }

            return View(details);
        }

        // POST: Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var details = await _context.Details.FindAsync(id);
            _context.Details.Remove(details);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetailsExists(int id)
        {
            return _context.Details.Any(e => e.UserId == id);
        }
    }
}
