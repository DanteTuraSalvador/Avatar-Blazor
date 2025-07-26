using Avatar.Web.Components;
using Avatar.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add Aspire service defaults (telemetry, health checks, etc.)
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add Telerik UI for Blazor
builder.Services.AddTelerikBlazor();

// Add health checks
builder.Services.AddHealthChecks();

// Add Infrastructure services with In-Memory database for development
builder.Services.AddInfrastructureInMemory();

var app = builder.Build();

// Map Aspire service defaults (health checks, etc.)
app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Temporarily disable HTTPS redirection for development
// app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<Avatar.Infrastructure.Data.SkillsDbContext>();
    context.Database.EnsureCreated();
}

app.Run();
