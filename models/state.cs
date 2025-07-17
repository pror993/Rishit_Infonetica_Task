namespace WorkflowEngine.Models;

public record State(
    string Id,
    string Name,
    bool IsInitial,
    bool IsFinal,
    bool Enabled
    // add more fields if you want
);
