namespace WorkflowEngine.Models;

public class TransitionRecord
{
    public string ActionId { get; set; } = default!;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    // add notes if you want
}
