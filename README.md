# Project Template — Entity Creation & Setup Guide

> A quick guide to adding new entities, managing migrations, and running a local Postgres database.

## Creating a New Entity  
_for an entity named `X`_

### `/ProjectTemplate.Domain/Models/X.cs`

```csharp
// Example: X.cs
public class X : EntityBase
{
    // Add your unique attributes
    // Use DataAnnotations if needed
}
```

**Steps:**
- Create a new `X` class inheriting from `EntityBase`
- Add its unique attributes  
- Use `[DataAnnotation]` attributes if needed to enforce rules

### `/ProjectTemplate.Infrastructure/Repository/XRepository.cs`

```csharp
public class XRepository : BaseRepository<X, CoreDbContext>
{
    public XRepository(CoreDbContext context) : base(context) {}
}
```

**Steps:**
- Create a new `XRepository` class inheriting from `BaseRepository<X, CoreDbContext>`  
- Inject `CoreDbContext` into the constructor  
- Pass it to the base repository  

### `/ProjectTemplate.Infrastructure/DependencyInjection.cs`

```csharp
services.AddScoped<XRepository>();
```

**Steps:**
- Register `XRepository` as **Scoped** in the `AddRepositories()` method

### `/ProjectTemplate.Infrastructure/Database/Configuration/XConfiguration.cs`

```csharp
public class XConfiguration : EntityBaseConfiguration<X>
{
    public override void Configure(EntityTypeBuilder<X> builder)
    {
        base.Configure(builder);
		builder.ToTable(nameof(X));
        // Add additional configuration for X's attributes
    }
}
```

**Steps:**
- Inherit from `EntityBaseConfiguration<X>`  
- Override `Configure()`  
- Always start with `base.Configure(builder)` to include base settings
- Configure entity to table mapping with `builder.ToTable(nameof(X));`

### `/ProjectTemplate.Applications/Services/XService.cs`

```csharp
public class XService : ServiceBase<XRepository, X>
{
    public XService(XRepository repository) : base(repository) {}

    // Add custom business logic methods here
}
```

**Steps:**
- Create a `XService` inheriting from `ServiceBase<XRepository, X>`  
- Inject the `XRepository`  
- Add your custom business logic methods  

### `/ProjectTemplate.Applications/DependencyInjection.cs`

```csharp
services.AddScoped<XService>();
```

**Steps:**
- Register `XService` as **Scoped** in the `AddRepositories()` method

Your service can now be injected and used in any API Controller!

---

## Useful Commands

### Create a New Migration

> Run this from the project root:

```bash
dotnet ef migrations add NameOfTheMigration \
  --project ProjectTemplate.Infrastructure \
  --startup-project ProjectTemplate.API/ProjectTemplate.API.csproj \
  --context CoreDbContext
```

### Apply the Migration

> Ensure the database is running and the connection string is correct in `appsettings.json`

```bash
dotnet ef database update \
  --project ProjectTemplate.Infrastructure \
  --startup-project ProjectTemplate.API/ProjectTemplate.API.csproj \
  --context CoreDbContext
```

**Note:** Be extra careful when merging branches that include EF migrations — conflicts can cause schema issues.

---

## Running a Local PostgreSQL Database

### Create a Mountable Directory

```bash
mkdir -p ~/Source/Volumes/db_volume
```

### Run the Container

> Docker Desktop is recommended for easy management.

```bash
docker run --name db_postgres \
  -e POSTGRES_PASSWORD=password \
  -v ~/Source/Volumes/db_volume:/var/lib/pgsql/data \
  -p 5432:5432 \
  -d postgres:18
```

Your local database is now ready for development!

---

## Summary

| Area | Task |
|------|------|
| Domain | Define the entity model |
| Repository | Add data access logic |
| Service | Implement business logic |
| Config | Extend EF Core configuration |
| Dependency Injection | Register the repository |
| Migrations | Keep the database up to date |
| Database | Run Postgres locally |
