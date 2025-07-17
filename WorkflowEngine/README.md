# Infonetica – RISHITPROJ

## Configurable Workflow Engine (State-Machine API) 

### What this is

Backend service to:
- Define workflows using states and transitions (actions)
- Start workflow instances
- Move instances between states using valid actions
- Inspect definitions and running instances

Everything runs in-memory. No database or file storage.

---

### How to run
1. Make sure .NET SDK 8 or newer is installed (tested with .NET 9)
   To check:
   ```bash
   dotnet --version
   ```
2. Clone this repo
3. In the project folder, run:
   ```bash
   dotnet run
   ```
4. Open your browser at [http://localhost:5200/swagger](http://localhost:5200/swagger) for interactive API docs and testing.

---
