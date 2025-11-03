using ProjectTemplate.Applications.Base;
using ProjectTemplate.Domain.Exceptions;
using ProjectTemplate.Domain.Models;
using ProjectTemplate.Infrastructure.Repository;
using ProjectTemplate.Applications.Interfaces;

namespace ProjectTemplate.Applications.Services;

public class MailingListService(MailingListRepository repository) : ServiceBase<MailingListRepository, MailingList>(repository), IMailingListService
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

	public async Task RemoveAsync(Guid deletionKey)
	{
		MailingList? email = await FirstOrDefaultAsync(e => e.DeletionKey == deletionKey)
			?? throw new NotFoundException("The specified key does not exist.");

		await DeleteAsync(email);
	}

	public async Task ConfirmEmailAsync(Guid confirmationKey)
	{
		MailingList? email = await FirstOrDefaultAsync(e => e.ConfirmationKey == confirmationKey)
			?? throw new NotFoundException("The specified key does not exist.");

		if (email.IsEmailValidated == false)
		{
			email.IsEmailValidated = true;
			await UpdateAsync(email);
		}
	}
}
