using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HPCN.UnionOnline.Data;
using HPCN.UnionOnline.Models;
using Microsoft.AspNetCore.Authorization;

namespace HPCN.UnionOnline.Controllers
{
    [Authorize]
    public class ActivityController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActivityController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Activity
        public async Task<IActionResult> Index()
        {
            return View(await _context.Activities.ToListAsync());
        }

        // GET: Activity/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities
                .SingleOrDefaultAsync(m => m.Id == id);
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        // GET: Activity/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Activity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,BeginTime,EndTime")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                activity.UpdatedTime = activity.CreatedTime = DateTime.Now;
                _context.Add(activity);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(activity);
        }

        // GET: Activity/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities.SingleOrDefaultAsync(m => m.Id == id);
            if (activity == null)
            {
                return NotFound();
            }
            return View(activity);
        }

        // POST: Activity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,BeginTime,EndTime,Status,CreatedTime,UpdatedTime")] Activity activity)
        {
            if (id != activity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    activity.UpdatedTime = DateTime.Now;
                    _context.Update(activity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityExists(activity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(activity);
        }

        // GET: Activity/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities
                .SingleOrDefaultAsync(m => m.Id == id);
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        // POST: Activity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var activity = await _context.Activities.SingleOrDefaultAsync(m => m.Id == id);
            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ActivityExists(Guid id)
        {
            return _context.Activities.Any(e => e.Id == id);
        }

        // GET: Activity/Open/5
        public async Task<IActionResult> Open(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities
                .SingleOrDefaultAsync(m => m.Id == id);
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        // POST: Activity/Open/5
        [HttpPost, ActionName("Open")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OpenConfirmed(Guid id)
        {
            var activity = await _context.Activities.SingleOrDefaultAsync(m => m.Id == id);
            activity.Status = ActivityState.Open;
            _context.Update(activity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Activity/Close/5
        public async Task<IActionResult> Close(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities
                .SingleOrDefaultAsync(m => m.Id == id);
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        // POST: Activity/Close/5
        [HttpPost, ActionName("Close")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CloseConfirmed(Guid id)
        {
            var activity = await _context.Activities.SingleOrDefaultAsync(m => m.Id == id);
            activity.Status = ActivityState.Closed;
            _context.Update(activity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
