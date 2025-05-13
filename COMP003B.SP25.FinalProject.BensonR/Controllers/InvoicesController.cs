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
	public class InvoicesController : Controller
	{
		private readonly ComputerContext _context;

		public InvoicesController(ComputerContext context)
		{
			_context = context;
		}

		// GET: Invoices
		public async Task<IActionResult> Index()
		{
			var invoices = await _context.Invoices
				.Include(i => i.RepairTicket)
				.ToListAsync();
			return View(invoices);
		}

		// GET: Invoices/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var invoice = await _context.Invoices
				.Include(i => i.RepairTicket)
				.FirstOrDefaultAsync(m => m.InvoiceId == id);
			if (invoice == null)
			{
				return NotFound();
			}

			return View(invoice);
		}

		// GET: Invoices/Create
		public IActionResult Create()
		{
			// Populate a dropdown list of repair tickets that don't have invoices yet
			ViewData["RepairTicketId"] = new SelectList(_context.RepairTickets
				.Where(rt => rt.InvoiceId == null), "Id", "Id");
			return View();
		}

		// POST: Invoices/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("InvoiceId,PayStatus,PayType,RepairTicketId")] Invoice invoice)
		{
			if (ModelState.IsValid)
			{
				_context.Add(invoice);
				var repairTicket = await _context.RepairTickets.FindAsync(invoice.RepairTicketId);
				if (repairTicket != null)
				{
					repairTicket.InvoiceId = invoice.InvoiceId;
					_context.Update(repairTicket);
				}
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["RepairTicketId"] = new SelectList(_context.RepairTickets
				.Where(rt => rt.InvoiceId == null), "Id", "Id", invoice.RepairTicketId);
			return View(invoice);
		}

		// GET: Invoices/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var invoice = await _context.Invoices.FindAsync(id);
			if (invoice == null)
			{
				return NotFound();
			}
			ViewData["RepairTicketId"] = new SelectList(_context.RepairTickets
				.Where(rt => rt.InvoiceId == null || rt.InvoiceId == id), "Id", "Id", invoice.RepairTicketId);
			return View(invoice);
		}

		// POST: Invoices/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("InvoiceId,PayStatus,PayType,RepairTicketId")] Invoice invoice)
		{
			if (id != invoice.InvoiceId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					var oldRepairTicket = await _context.RepairTickets
						.FirstOrDefaultAsync(rt => rt.InvoiceId == invoice.InvoiceId);
					if (oldRepairTicket != null && oldRepairTicket.Id != invoice.RepairTicketId)
					{
						oldRepairTicket.InvoiceId = null;
						_context.Update(oldRepairTicket);
					}
					var newRepairTicket = await _context.RepairTickets.FindAsync(invoice.RepairTicketId);
					if (newRepairTicket != null && newRepairTicket.Id != (oldRepairTicket?.Id ?? 0))
					{
						newRepairTicket.InvoiceId = invoice.InvoiceId;
						_context.Update(newRepairTicket);
					}
					_context.Update(invoice);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!InvoiceExists(invoice.InvoiceId))
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
			ViewData["RepairTicketId"] = new SelectList(_context.RepairTickets
				.Where(rt => rt.InvoiceId == null || rt.InvoiceId == id), "Id", "Id", invoice.RepairTicketId);
			return View(invoice);
		}

		// GET: Invoices/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var invoice = await _context.Invoices
				.Include(i => i.RepairTicket)
				.FirstOrDefaultAsync(m => m.InvoiceId == id);
			if (invoice == null)
			{
				return NotFound();
			}

			return View(invoice);
		}

		// POST: Invoices/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var invoice = await _context.Invoices.FindAsync(id);
			if (invoice != null)
			{
				var repairTicket = await _context.RepairTickets
					.FirstOrDefaultAsync(rt => rt.InvoiceId == id);
				if (repairTicket != null)
				{
					repairTicket.InvoiceId = null;
					_context.Update(repairTicket);
				}
				_context.Invoices.Remove(invoice);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool InvoiceExists(int id)
		{
			return _context.Invoices.Any(e => e.InvoiceId == id);
		}
	}
}