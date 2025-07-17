using WorkflowEngine.Models;

namespace WorkflowEngine.Services;

public class WorkflowEngineService
{
    private readonly Dictionary<string, WorkflowDefinition> _definitions = new();
    private readonly Dictionary<string, WorkflowInstance> _instances = new();

    public bool CreateDefinition(WorkflowDefinition def, out string error)
    {
        error = string.Empty;
        if (_definitions.ContainsKey(def.Id))
        {
            error = "Workflow definition with the same ID already exists.";
            return false;
        }
        var initialStates = def.States.Where(s => s.IsInitial).ToList();
        if (initialStates.Count != 1)
        {
            error = "Workflow must have exactly one initial state.";
            return false;
        }
        if (def.States.Select(s => s.Id).Distinct().Count() != def.States.Count)
        {
            error = "Duplicate state IDs found.";
            return false;
        }
        if (def.Actions.Select(a => a.Id).Distinct().Count() != def.Actions.Count)
        {
            error = "Duplicate action IDs found.";
            return false;
        }
        var stateIds = def.States.Select(s => s.Id).ToHashSet();
        foreach (var action in def.Actions)
        {
            if (!action.Enabled) continue;
            if (!stateIds.Contains(action.ToState) ||
                !action.FromStates.All(stateIds.Contains))
            {
                error = $"Action {action.Id} references unknown state(s).";
                return false;
            }
        }
        _definitions[def.Id] = def;
        return true;
    }

    public WorkflowDefinition? GetDefinition(string id) =>
        _definitions.TryGetValue(id, out var def) ? def : null;

    public WorkflowInstance? StartInstance(string definitionId)
    {
        if (!_definitions.TryGetValue(definitionId, out var def))
            return null;
        var initial = def.States.First(s => s.IsInitial);
        var instance = new WorkflowInstance
        {
            DefinitionId = def.Id,
            CurrentState = initial.Id
        };
        _instances[instance.Id] = instance;
        return instance;
    }

    public WorkflowInstance? GetInstance(string id) =>
        _instances.TryGetValue(id, out var inst) ? inst : null;

    public bool ExecuteAction(string instanceId, string actionId, out string error)
    {
        error = string.Empty;
        if (!_instances.TryGetValue(instanceId, out var instance))
        {
            error = "Workflow instance not found.";
            return false;
        }
        var def = _definitions[instance.DefinitionId];
        var currentState = def.States.First(s => s.Id == instance.CurrentState);
        if (currentState.IsFinal)
        {
            error = "Cannot execute action from a final state.";
            return false;
        }
        var action = def.Actions.FirstOrDefault(a => a.Id == actionId);
        if (action == null)
        {
            error = "Action not found.";
            return false;
        }
        if (!action.Enabled)
        {
            error = "Action is disabled.";
            return false;
        }
        if (!action.FromStates.Contains(currentState.Id))
        {
            error = "Action cannot be executed from the current state.";
            return false;
        }
        instance.CurrentState = action.ToState;
        instance.History.Add(new TransitionRecord
        {
            ActionId = action.Id,
            Timestamp = DateTime.UtcNow
        });
        return true;
    }

    public List<WorkflowDefinition> GetAllDefinitions() => _definitions.Values.ToList();
    public List<WorkflowInstance> GetAllInstances() => _instances.Values.ToList();
    // easy: add more methods if you need them
}
