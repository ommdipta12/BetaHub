var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.IdentityServer>("identityserver");

builder.Build().Run();
