## The Program
A console application that simulates a garage with a fixed number of parking spots, chosen when the garage is created. Vehicles (cars, motorcycles, buses, airplanes and boats) can be parked, removed and looked up by license plate. You can list every parked vehicle, or see how many of each vehicle type are parked. You can also search by type, color and number of wheels, in any combination. Only Swedish license plates are accepted (for example `ABC123` or `ABC12A`), and invalid input never crashes the program.

## How to start

Open `vehicles-List.slnx` in Visual Studio and press **F5**, or run this from the solution folder:

```
dotnet run --project vehicles-List
```

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

## Jag vill ha feedback på
- separation av olika funktionaliteter till olika klasser.
- Felhantering.
- Läsbarhet på koden.
