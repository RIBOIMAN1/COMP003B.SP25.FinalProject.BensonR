using System.ComponentModel.DataAnnotations;

namespace COMP003B.SP25.FinalProject.BensonR.Models
{
	public class Invoice
	{
		public int InvoiceId { get; set; }
		[Required]
		public string PayStatus { get; set; }
		[Required]
		public string PayType { get; set; }
		public int? RepairTicketId { get; set; }
		public virtual ICollection<RepairTicket>? RepairTickets { get; set; }
	}
}