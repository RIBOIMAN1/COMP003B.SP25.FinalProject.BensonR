using System;
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
    public class RepairTicketsController : Controller
    {
        private readonly ComputerContext _context;

        public RepairTicketsController(ComputerContext context)
        {
            _context = context;
        }

        // GET: RepairTickets
        public async Task<IActionResult> Index()
        {
            var computerContext = _context.RepairTickets.Include(r => r.Customer).Include(r => r.Inventory).Include(r => r.Invoice).Include(r => r.Technician);
            return View(await computerContext.ToListAsync());
        }

        // GET: RepairTickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repairTicket = await _context.RepairTickets
                .Include(r => r.Customer)
                .Include(r => r.Inventory)
                .Include(r => r.Invoice)
                .Include(r => r.Technician)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (repairTicket == null)
            {
                return NotFound();
            }

            return View(repairTicket);
        }

        // GET: RepairTickets/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Email");
            ViewData["InventoryId"] = new SelectList(_context.Inventories, "InventoryId", "CompType");
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "InvoiceId", "PayStatus");
            ViewData["TechnicianId"] = new SelectList(_context.Technicians, "TechnicianId", "Email");
            return View();
        }

        // POST: RepairTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,TechnicianId,InventoryId,InvoiceId")] RepairTicket repairTicket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(repairTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Email", repairTicket.CustomerId);
            ViewData["InventoryId"] = new SelectList(_context.Inventories, "InventoryId", "CompType", repairTicket.InventoryId);
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "InvoiceId", "PayStatus", repairTicket.InvoiceId);
            ViewData["TechnicianId"] = new SelectList(_context.Technicians, "TechnicianId", "Email", repairTicket.TechnicianId);
            return View(repairTicket);
        }

        // GET: RepairTickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repairTicket = await _context.RepairTickets.FindAsync(id);
            if (repairTicket == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Email", repairTicket.CustomerId);
            ViewData["InventoryId"] = new SelectList(_context.Inventories, "InventoryId", "CompType", repairTicket.InventoryId);
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "InvoiceId", "PayStatus", repairTicket.InvoiceId);
            ViewData["TechnicianId"] = new SelectList(_context.Technicians, "TechnicianId", "Email", repairTicket.TechnicianId);
            return View(repairTicket);
        }

        // POST: RepairTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,TechnicianId,InventoryId,InvoiceId")] RepairTicket repairTicket)
        {
            if (id != repairTicket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(repairTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepairTicketExists(repairTicket.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Email", repairTicket.CustomerId);
            ViewData["InventoryId"] = new SelectList(_context.Inventories, "InventoryId", "CompType", repairTicket.InventoryId);
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "InvoiceId", "PayStatus", repairTicket.InvoiceId);
            ViewData["TechnicianId"] = new SelectList(_context.Technicians, "TechnicianId", "Email", repairTicket.TechnicianId);
            return View(repairTicket);
        }

        // GET: RepairTickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repairTicket = await _context.RepairTickets
                .Include(r => r.Customer)
                .Include(r => r.Inventory)
                .Include(r => r.Invoice)
                .Include(r => r.Technician)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (repairTicket == null)
            {
                return NotFound();
            }

            return View(repairTicket);
        }

        // POST: RepairTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var repairTicket = await _context.RepairTickets.FindAsync(id);
            if (repairTicket != null)
            {
                _context.RepairTickets.Remove(repairTicket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepairTicketExists(int id)
        {
            return _context.RepairTickets.Any(e => e.Id == id);
        }
    }
}
