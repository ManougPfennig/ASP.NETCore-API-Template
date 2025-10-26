using ProjectTemplate.Applications.Services;
using ProjectTemplate.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProjectTemplate.API.Controllers
{
    [Route("api/mailinglist")]
    [ApiController]
    public class MailingListController(MailingListService mailingList) : ControllerBase
	{
		MailingListService _mailingList = mailingList;

		[HttpPost]
		[Route("subscribe")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> AddToMailingList([FromBody] MailingListDto dto)
		{
			await _mailingList.CreateAsync(dto.Email);

			// Successfully created
			return Created();
		}
	}
}
