var builder = DistributedApplication.CreateBuilder(args);

// Add the main web application (simplified for now)
var webApp = builder.AddProject<Projects.Avatar_Web>("skills-manager")
    .WithExternalHttpEndpoints();

builder.Build().Run();
