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
g
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

## Limitations
- No authentication or authorization
- No local storage or database (in-memory only)
- No file persistence
- No pagination or filtering
- No unit tests

## Assumptions
- All data is lost on restart
- Only basic validation is done
- Minimal comments and TODOs

## What next?
- Add authentication and user roles
- Add local storage or simple database
- Add persistence (save/load workflows)
- Add more validation and error handling
- Add tests
- Add pagination/filtering for large lists
