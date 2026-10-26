## Where things live

| Class | Responsibility |
|---|---|
| `Program` | Entry point; starts `UserInterface`. |
| `UserInterface` | All console input/output: menus, questions, messages. |
| `Garage` | Stores vehicles in a private `List<Vehicle>` and handles all garage logic. No console code. |
| `Vehicle` + subclasses (`Vehicles/`) | Vehicle data. Shared properties live in `Vehicle`, type-specific ones in `Car`, `Motorcycle`, `Bus`, `Airplane` and `Boat`. |

## Functionality

| Functionality | Where |
|---|---|
| Main menu | `UserInterface.Run()` |
| Create garage / capacity | `UserInterface.CreateGarage()` → `new Garage(capacity)` |
| Park | `UserInterface.ParkVehicle()` → `Garage.Park()` |
| Remove | `UserInterface.RemoveVehicle()` → `Garage.Remove()` |
| Find by license plate | `UserInterface.FindVehicle()` → `Garage.FindVehicle()` |
| List all vehicles | `UserInterface.ListAllVehicles()` → `Garage.GetAllVehicles()` |
| Count per vehicle type | `UserInterface.ListVehicleTypes()` → `Garage.CountType()` |
| Search (type, color, wheels) | `UserInterface.SearchVehicles()` → `Garage.Search()` / `Garage.Matches()` |
| Sample vehicles | `UserInterface.AddSampleVehicles()` |
| Swedish license plate check | `Vehicle.IsValidLicensePlate()`, used by `UserInterface.AskLicensePlate()` |
| Input validation | `UserInterface.AskText()`, `AskNumber()`, `AskLicensePlate()` |
| How vehicles are printed | `ToString()` in `Vehicle`, overridden in each subclass |
