using ProjectTemplate.Applications.Base;
using ProjectTemplate.Domain.Exceptions;
using ProjectTemplate.Domain.Models;
using ProjectTemplate.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace ProjectTemplate.Applications.Services;

public class MailingListService(MailingListRepository repository) : ServiceBase<MailingListRepository, MailingList>(repository)
{
	public async Task<MailingList> CreateAsync(string email)
	{
		if (await ExistsAsync(e => e.Email == email))
		{
			throw new ConflictException();
		}

		MailingList form = new()
		{
			Email = email,
			DeletionKey = Guid.NewGuid(),
			ConfirmationKey = Guid.NewGuid(),
			IsEmailValidated = false
		};

		return await AddAsync(form);
	}
}
