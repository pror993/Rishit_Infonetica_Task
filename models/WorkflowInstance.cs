namespace WorkflowEngine.Models;

public class WorkflowInstance
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string DefinitionId { get; set; } = default!;
    public string CurrentState { get; set; } = default!;
    public List<TransitionRecord> History { get; set; } = new();
    // add user info if you want
}
