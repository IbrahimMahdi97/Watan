# Overview
This repository contains a .NET 7 based REST API.
The solution (`Watan.sln`) groups several projects:
```
Watan/                 – API host (Program.cs, middleware, migrations)
Watan.Presentation/    – API controllers
Service/               – Business logic implementations
Service.Interface/     – Service layer interfaces
Repository/            – Dapper repositories + raw SQL
Interfaces/            – Repository and logger interfaces
Entities/              – Domain models, enums, custom exceptions
Shared/                – DTOs, helper extensions, request parameter classes
LoggerService/         – NLog logger implementation
```
## API Host
`Watan/Program.cs` wires everything together. It sets up logging, DI, migrations, JWT auth, and registers controllers. The application uses a custom middleware to handle missing/expired tokens and an exception handler. Swagger is configured for API documentation.
```
builder.Services.ConfigureCors();
builder.Services.ConfigureLoggerService();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton<Database>();
builder.Services.ConfigureFluentMigrator(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSwagger();
builder.Services.AddAuthentication();
builder.Services.ConfigureJwt(builder.Configuration);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(WatanPresentation.AssemblyReference).Assembly)
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateConverter());
    });
...
app.UseAuthentication();
app.UseMiddleware<ExpiredOrMissedTokenMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.MigrateDatabase(logger);
```

## Layers
1. Entities
Holds POCO classes for database entities (e.g., `User`, `Post`) and enumerations. It also defines custom exception types for error handling.

2. Shared
Contains Data Transfer Objects (DTOs), parameter objects for paging/filtering, and helper extensions (e.g., SHA-512 hashing and retrieving user ID from JWT claims).
```
public static class ClaimsPrincipleExtensions
{
    public static int RetrieveUserIdFromPrincipal(this ClaimsPrincipal user)
    {
        return Convert.ToInt32(
            user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    }
    ...
}
```
3. Interfaces
Defines repository interfaces (`IUserRepository`, `IPostRepository`, etc.) and other abstractions such as `ILoggerManager` or `IFileStorageService`.

4. Repository
Implements repositories using Dapper. SQL queries live in `Repository/Query/*Query.cs`. Example: `PostRepository` retrieves posts, creates them inside transactions, and maps results to DTOs.

5. Service.Interface / Service
The service layer implements business logic. Each service depends on `IRepositoryManager` and optional helpers like `FileStorageService`. Example: `PostService` retrieves posts and attaches image URLs by reading from local storage.
```
public async Task<IEnumerable<PostDto>> GetAllPosts(int userId)
{
    var posts = (await _repository.Post.GetAllPosts(userId)).ToList();
    foreach (var post in posts)
    {
        var images = _fileStorageService.GetFilesUrlsFromServer(
            post.Id,
            _configuration["PostImagesSetStorageUrl"]!,
            _configuration["PostImagesGetStorageUrl"]!).ToList();
        post.ImageUrl = images.Any() ? images.First() : "";
    }
    return posts;
}
```

`ServiceManager` and `RepositoryManager` lazily instantiate services and repositories to centralize dependency management.

6. Watan.Presentation
Houses ASP.NET controllers which are thin wrappers around the service layer. An example is `PostsController`, providing CRUD operations with JWT authorization.

7. LoggerService
Implements `ILoggerManager` using NLog.

8. Migrations
Database schema is managed with FluentMigrator. `MigrationManager` runs migrations on startup. `Database.cs` creates the database if needed.

## Configuration and Helpers
- `nlog.config` configures file-based logging.
- `serviceAccountKey.json` stores Firebase credentials for sending push notifications via NotificationService.
- `global.json` pins the .NET SDK version (7.0).
- `launchSettings.json` contains local development profiles.

## What to Learn Next
1. Dapper basics – Understand how repositories build SQL queries and map them to DTOs.
2. FluentMigrator – Review migration classes in `Watan/Migrations` to learn the database schema and seeding process.
3. JWT authentication – Examine `UserService` for token creation/refresh logic and the custom middleware handling expired/missing tokens.
4. Dependency injection – Study `ServiceExtensions.cs` for how services, repositories, and logging are registered.
5. Controller to service flow – Observe how controllers call service methods and how responses are shaped using DTOs.
6. File storage utilities – See `FileStorageService` for saving/retrieving images.
7. Firebase push notifications – Explore `NotificationService` for sending notifications and retrieving unread counts.

Understanding these pieces will make it easier to contribute new endpoints, modify database queries, or adjust business rules within this codebase.
