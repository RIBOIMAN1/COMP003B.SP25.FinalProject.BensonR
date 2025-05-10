using System.ComponentModel.DataAnnotations;
namespace COMP003B.SP25.FinalProject.BensonR.Models
{
	public class RepairTicket
	{
		public int Id { get; set; }
		public int CustomerId { get; set; }
		public int TechnicianId { get; set; }
		public virtual Customer? Customer { get; set; }
		public virtual Technician? Technician { get; set; }
	}
}