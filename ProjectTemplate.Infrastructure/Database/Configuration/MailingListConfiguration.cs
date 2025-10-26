using ProjectTemplate.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectTemplate.Infrastructure.Database.Configuration;

public class MailingListConfiguration : EntityBaseConfiguration<MailingList>
{
	public override void Configure(EntityTypeBuilder<MailingList> builder)
	{
		// EntityBase configuration
		base.Configure(builder);
		
		// Entity to table mapping
		builder.ToTable(nameof(MailingList));

		// Unique constraint on Email
		builder.HasIndex(u => u.Email)
               .IsUnique();
	}
}
