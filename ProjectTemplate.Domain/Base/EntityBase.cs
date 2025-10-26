using System;

namespace ProjectTemplate.Domain.Base;

public abstract class EntityBase
{
	public int Id { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }

	public void SetCreationTime()
	{
		DateTime now = DateTime.UtcNow;

		CreatedAt = now;
		UpdatedAt = now;
	}

	public void SetUpdateTime()
	{
		UpdatedAt = DateTime.UtcNow;
	}
}
