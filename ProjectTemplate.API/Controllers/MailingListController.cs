using ProjectTemplate.Applications.Services;
using ProjectTemplate.Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ProjectTemplate.API.Controllers
{
    [Route("api/mailinglist")]
    [ApiController]
    public class MailingListController(MailingListService mailingList) : ControllerBase
	{
		MailingListService _mailingList = mailingList;

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		public async Task<ActionResult> AddToMailingList([FromBody] MailingListDto dto)
		{
			await _mailingList.CreateAsync(dto.Email);

			// Successfully created
			return Created();
		}

		[HttpDelete]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> RemoveFromMailingList([FromQuery] Guid deletionKey)
		{
			await _mailingList.RemoveAsync(deletionKey);

			// Successfully deleted
			return NoContent();
		}

		[HttpPut]
		[Route("confirm")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> ConfirmEmailAddress([FromQuery] Guid confirmationKey)
		{
			await _mailingList.ConfirmEmailAsync(confirmationKey);

			// Successfully confirmed
			return NoContent();
		}
	}
}
