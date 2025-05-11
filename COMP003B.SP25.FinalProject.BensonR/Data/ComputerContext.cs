using COMP003B.SP25.FinalProject.BensonR.Models;
using Microsoft.EntityFrameworkCore;

namespace COMP003B.SP25.FinalProject.BensonR.Data
{
	public class ComputerContext : DbContext
	{
		public ComputerContext(DbContextOptions<ComputerContext> options) : base(options)
		{
		}

		public DbSet<Customer> Customers { get; set; }
		public DbSet<Technician> Technicians { get; set; }
		public DbSet<RepairTicket> RepairTickets { get; set; }
		public DbSet<Inventory> Inventories { get; set; }
		public DbSet<Invoice> Invoices { get; set; }
	}
}