using System.ComponentModel.DataAnnotations;

namespace ProjectTemplate.Domain.DTO;

public class MailingListDto
{
	[EmailAddress]
	public required string Email { get; set; }
}
