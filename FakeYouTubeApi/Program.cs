/* 
    To run you must:
    * Have .net 6 installed
    * Have latest version of "C# for Visual Studio Code (powered by OmniSharp)"
    * Browse to https://developers.google.com/youtube/v3/getting-started to make an api key. The key must then be added to appsettings.json
    * Execute: dotnet dev-certs https --trust
    * Press Ctrl+F5.
    * At the Select environment prompt, choose .NET Core.
    * Select Add Configuration > .NET: Launch a local .NET Core Console App.
    * In the configuration JSON:
        Replace <target-framework> with net6.0.
        Replace <project-name.dll> with TodoApi.dll.
    * Press Ctrl+F5.
    * In the Could not find the task 'build' dialog, select Configure Task.
    * Select Create tasks.json file from template.
    * Select the .NET Core task template.
    * Press Ctrl+F5.

    Browse to: https://localhost:7072/swagger/
*/

using FakeYouTubeApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IActualYoutube, ActualYoutube>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
