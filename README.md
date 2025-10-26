## Creating a new entity :
_for an entity named 'X'_

### In /ProjectTemplate.Domain/Models/X.cs
- Create a new 'X' class, inheriting from EntityBase
- Add it's unique attributes
- Use DataAnnotations if needed to add rules to it's attributes

### In /ProjectTemplate.Infrastructure/Repository
- Create a new XRepository class, inheriting from BaseRepository<X, CoreDbContext>
- Inject CoreDbContext in it's contructor and pass it to the BaseRepository

### In /ProjectTemplate.Infrastructure/DependencyInjection.cs
- Inject XRepository as scoped in the AddRepositories method

### In /ProjectTemplate.Infrastructure/Database/Configuration
- Create a new XConfiguration class, inheriting from EntityBaseConfiguration<X>
- Override the base Configure method to add additional configuration to X's attributes
- Start the override by calling base.Configure() to ensure EntityBase's attributes configuration

### In /ProjectTemplate.Applications/Services
- Create a new XService class, inheriting from ServiceBase<XRepository, X>
- Inject XRepository in it's contructor and pass it to the ServiceBase
- Add your custom methods with business logic related to your new X entity.
- You can use all inherited methods from ServiceBase to do database requests.


## Useful commands :

### To create a new migration, run at the project's root

dotnet ef migrations add NameOfTheMigration \
	--project ProjectTemplate.Infrastructure \
	--startup-project ProjectTemplate.API/ProjectTemplate.API.csproj \
	--context CoreDbContext

### To apply the migration, run at project's root :
_database must be running and the connectionstring correctly configured in appsettings_

dotnet ef database update \
	--project ProjectTemplate.Infrastructure \
	--startup-project ProjectTemplate.API/ProjectTemplate.API.csproj \
	--context CoreDbContext

### Stay especially careful with any git merges that involves migrations !



## Starting a local postgres database for testing :

### Create a mountable directory for the container

mkdir -p ~/Source/Volumes/db_volume

### Create and run the container
_DockerDesktop is recommended to easily start, stop or delete the database_

docker run --name db_postgres \
	-e POSTGRES_PASSWORD=password \
	-v ~/Source/Volumes/db_volume:/var/lib/pgsql/data \
	-p 5432:5432 \
	-d postgres:18
