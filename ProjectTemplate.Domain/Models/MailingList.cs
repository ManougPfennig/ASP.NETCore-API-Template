using System.ComponentModel.DataAnnotations;
using ProjectTemplate.Domain.Base;

namespace ProjectTemplate.Domain.Models
{
	public class MailingList : EntityBase
	{
		[EmailAddress]
		public required string Email { get; set; }

		[Required]
		public Guid DeletionKey { get; set; }

		[Required]
		public Guid ConfirmationKey { get; set; }

		[Required]
		public bool IsEmailValidated { get; set; } = false;
	}
}
