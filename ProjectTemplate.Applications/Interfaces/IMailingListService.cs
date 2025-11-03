using ProjectTemplate.Domain.Models;

namespace ProjectTemplate.Applications.Interfaces;

public interface IMailingListService
{
	public Task<MailingList> CreateAsync(string email);
	public Task RemoveAsync(Guid deletionKey);
	public Task ConfirmEmailAsync(Guid confirmationKey);

}
