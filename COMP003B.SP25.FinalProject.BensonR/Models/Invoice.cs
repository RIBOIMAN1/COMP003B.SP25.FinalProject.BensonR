using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COMP003B.SP25.FinalProject.BensonR.Models
{
	public class Invoice
	{
		public int InvoiceId { get; set; }
		[Required]
		public string PayStatus { get; set; }
		[Required]
		public string PayType { get; set; }
		[Required]
		public int RepairTicketId { get; set; }
		[ForeignKey("RepairTicketId")]
		public virtual RepairTicket? RepairTicket { get; set; }
	}
}