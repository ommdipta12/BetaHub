var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.IdentityServer>("identityserver");

builder.AddProject<Projects.BetaHub_Auth>("betahub-auth");

builder.Build().Run();
