﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using COMP003B.SP25.FinalProject.BensonR.Data;
using COMP003B.SP25.FinalProject.BensonR.Models;

namespace COMP003B.SP25.FinalProject.BensonR.Controllers
{
    public class TechniciansController : Controller
    {
        private readonly ComputerContext _context;

        public TechniciansController(ComputerContext context)
        {
            _context = context;
        }

        // GET: Technicians
        public async Task<IActionResult> Index()
        {
            return View(await _context.Technicians.ToListAsync());
        }

        // GET: Technicians/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technician = await _context.Technicians
                .FirstOrDefaultAsync(m => m.TechnicianId == id);
            if (technician == null)
            {
                return NotFound();
            }

            return View(technician);
        }

        // GET: Technicians/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Technicians/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TechnicianId,Name,Email,ExperienceType")] Technician technician)
        {
            if (ModelState.IsValid)
            {
                _context.Add(technician);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(technician);
        }

        // GET: Technicians/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technician = await _context.Technicians.FindAsync(id);
            if (technician == null)
            {
                return NotFound();
            }
            return View(technician);
        }

        // POST: Technicians/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TechnicianId,Name,Email,ExperienceType")] Technician technician)
        {
            if (id != technician.TechnicianId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(technician);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TechnicianExists(technician.TechnicianId))
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
            return View(technician);
        }

        // GET: Technicians/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technician = await _context.Technicians
                .FirstOrDefaultAsync(m => m.TechnicianId == id);
            if (technician == null)
            {
                return NotFound();
            }

            return View(technician);
        }

        // POST: Technicians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var technician = await _context.Technicians.FindAsync(id);
            if (technician != null)
            {
                _context.Technicians.Remove(technician);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TechnicianExists(int id)
        {
            return _context.Technicians.Any(e => e.TechnicianId == id);
        }
    }
}
