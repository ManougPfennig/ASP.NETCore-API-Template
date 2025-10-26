using System;
using ProjectTemplate.Domain.Models;
using ProjectTemplate.Infrastructure.Base;
using ProjectTemplate.Infrastructure.Database;

namespace ProjectTemplate.Infrastructure.Repository;

public class MailingListRepository(CoreDbContext databaseContext) : BaseRepository<MailingList, CoreDbContext>(databaseContext)
{
}
