using WorkflowEngine.Models;
using WorkflowEngine.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<WorkflowEngineService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Just a quick check
app.MapGet("/", () => "Workflow Engine is running!");

#region Workflow Definition Endpoints

app.MapPost("/workflow", (WorkflowDefinition def, WorkflowEngineService engine) =>
{
    if (engine.CreateDefinition(def, out var error))
        return Results.Ok(new { message = "Workflow definition created." });
    return Results.BadRequest(new { error });
});

app.MapGet("/workflow/{id}", (string id, WorkflowEngineService engine) =>
{
    var def = engine.GetDefinition(id);
    return def is not null ? Results.Ok(def) : Results.NotFound();
});

app.MapGet("/workflow", (WorkflowEngineService engine) =>
{
    return Results.Ok(engine.GetAllDefinitions());
});

#endregion

#region Workflow Runtime Endpoints

app.MapPost("/instance/{definitionId}", (string definitionId, WorkflowEngineService engine) =>
{
    var instance = engine.StartInstance(definitionId);
    return instance is not null ? Results.Ok(instance) : Results.BadRequest(new { error = "Invalid workflow definition ID." });
});

app.MapGet("/instance/{id}", (string id, WorkflowEngineService engine) =>
{
    var instance = engine.GetInstance(id);
    return instance is not null ? Results.Ok(instance) : Results.NotFound();
});

app.MapPost("/instance/{instanceId}/execute/{actionId}", (string instanceId, string actionId, WorkflowEngineService engine) =>
{
    if (engine.ExecuteAction(instanceId, actionId, out var error))
        return Results.Ok(new { message = "Action executed successfully." });
    return Results.BadRequest(new { error });
});

app.MapGet("/instance", (WorkflowEngineService engine) =>
{
    return Results.Ok(engine.GetAllInstances());
});

#endregion

app.Run();
