// Create the application builder
var builder = DistributedApplication.CreateBuilder(args);

// Add PostgresSQL DB container
var placesDb = builder.AddPostgres("utraverse-placesdb");

// Add Redis cache
var placesCache = builder.AddRedis("utraverse-placescache");

// Add the place matcher AI container
var placeMatcher = builder.AddContainer("utraverse-placematcher", "utraverse/placematcher");

// Add the Places API project reference
var placesApi = builder.AddProject<Projects.uTraverse_PlacesAPI>("utraverse-placesapi")
    .WithReference(placesDb)
    .WithReference(placesCache);

// Add the AI API project reference
var aiApi = builder.AddProject<Projects.uTraverse_AiAPI>("utraverse-aiapi");

// Run the app (Aspire AppHost)
builder.Build().Run();