using System.ComponentModel.DataAnnotations;
namespace COMP003B.SP25.FinalProject.BensonR.Models
{
	public class Customer
	{
		public int CustomerId { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		public virtual ICollection<RepairTicket>? RepairTickets { get; set; }
	}
}