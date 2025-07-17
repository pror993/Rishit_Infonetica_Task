namespace WorkflowEngine.Models;

public record WorkflowAction(
    string Id,
    string Name,
    bool Enabled,
    List<string> FromStates,
    string ToState
    // add more if you need
);
