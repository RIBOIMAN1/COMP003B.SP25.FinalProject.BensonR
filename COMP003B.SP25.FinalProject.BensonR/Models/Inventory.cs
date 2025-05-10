using System.ComponentModel.DataAnnotations;

namespace COMP003B.SP25.FinalProject.BensonR.Models
{
	public class Inventory
	{
		public int TechnicianId { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string CompType { get; set; }
		public virtual Technician? Technician { get; set; }
		public virtual ICollection<RepairTicket>? RepairTickets { get; set; }
	}
}