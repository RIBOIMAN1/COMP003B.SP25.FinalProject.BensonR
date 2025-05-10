using System.ComponentModel.DataAnnotations;

namespace COMP003B.SP25.FinalProject.BensonR.Models
{
	public class Technician
	{
		public int TechnicianId { get; set; }

		[Required]
		public string Name { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string ExperienceType { get; set; }
		public virtual ICollection<RepairTicket>? RepairTickets { get; set; }
	}
}