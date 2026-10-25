using System;
using System.Collections.Generic;
using System.Text;
using vehicles.Vehicles;

namespace vehicles
{
    public class UserInterface
    {
        private Garage garage;

        public UserInterface()
        {
            Console.WriteLine("Welcome to the garage!");
            garage = CreateGarage();
        }

        public void Run()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine();
                Console.WriteLine($"=== Garage ({garage.CountVehicles()}/{garage.GetCapacity()} spots taken) ===");
                Console.WriteLine("1. List all parked vehicles");
                Console.WriteLine("2. List vehicle types and how many of each");
                Console.WriteLine("3. Park a vehicle");
                Console.WriteLine("4. Remove a vehicle");
                Console.WriteLine("5. Find a vehicle by license plate");
                Console.WriteLine("6. Search vehicles");
                Console.WriteLine("7. Add some sample vehicles");
                Console.WriteLine("8. Create a new garage (the old one is thrown away)");
                Console.WriteLine("0. Exit");

                string choice = AskText("Choose: ");

                switch (choice)
                {
                    case "1":
                        ListAllVehicles();
                        break;
                    case "2":
                        ListVehicleTypes();
                        break;
                    case "3":
                        ParkVehicle();
                        break;
                    case "4":
                        RemoveVehicle();
                        break;
                    case "5":
                        FindVehicle();
                        break;
                    case "6":
                        SearchVehicles();
                        break;
                    case "7":
                        AddSampleVehicles();
                        break;
                    case "8":
                        garage = CreateGarage();
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please enter a number from 0 to 8.");
                        break;
                }
            }

            Console.WriteLine("Goodbye!");
        }

        private Garage CreateGarage()
        {
            int capacity = AskNumber("How many parking spots should the garage have? ", 1, 1000);
            Console.WriteLine($"A garage with {capacity} spots was created.");
            return new Garage(capacity);
        }

        private void ListAllVehicles()
        {
            List<Vehicle> vehicles = garage.GetAllVehicles();
            PrintVehicles(vehicles);
        }

        private void ListVehicleTypes()
        {
            List<string> types = new List<string> { "Car", "Motorcycle", "Bus", "Airplane", "Boat" };

            foreach (string type in types)
            {
                Console.WriteLine($"{type}: {garage.CountType(type)}");
            }
        }

        private void ParkVehicle()
        {
            if (garage.IsFull())
            {
                Console.WriteLine("The garage is full. Remove a vehicle before parking a new one.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("What kind of vehicle?");
            Console.WriteLine("1. Car");
            Console.WriteLine("2. Motorcycle");
            Console.WriteLine("3. Bus");
            Console.WriteLine("4. Airplane");
            Console.WriteLine("5. Boat");
            int vehicleType = AskNumber("Choose: ", 1, 5);

            //questions that every vehicle has.
            string plate = AskLicensePlate("License plate number (ABC123, ABC12A): ");
            if (garage.FindVehicle(plate) != null)
            {
                Console.WriteLine($"A vehicle with license plate {plate} is already parked here.");
                return;
            }
            string color = AskText("Color: ");
            int wheels = AskNumber("Number of wheels: ", 0, 20);

            //question depending on type of vehicle
            Vehicle vehicle;
            if (vehicleType == 1)
            {
                string fuelType = AskText("Fuel type (for example Gasoline or Diesel): ");
                vehicle = new Car(plate, color, wheels, fuelType);
            }
            else if (vehicleType == 2)
            {
                int cylinderVolume = AskNumber("Cylinder volume (cc): ", 1, 5000);
                vehicle = new Motorcycle(plate, color, wheels, cylinderVolume);
            }
            else if (vehicleType == 3)
            {
                int seats = AskNumber("Number of seats: ", 1, 200);
                vehicle = new Bus(plate, color, wheels, seats);
            }
            else if (vehicleType == 4)
            {
                int engines = AskNumber("Number of engines: ", 1, 10);
                vehicle = new Airplane(plate, color, wheels, engines);
            }
            else
            {
                int length = AskNumber("Length (meters): ", 1, 500);
                vehicle = new Boat(plate, color, wheels, length);
            }

            if (garage.Park(vehicle))
            {
                Console.WriteLine($"{vehicle.GetTypeName()} {vehicle.LicensePlateNumber} is now parked.");
            }
            else
            {
                Console.WriteLine("Sorry, the vehicle could not be parked.");
            }
        }

        private void RemoveVehicle()
        {
            string plate = AskLicensePlate("License plate of the vehicle to remove: ");

            if (garage.Remove(plate))
            {
                Console.WriteLine($"{plate} has left the garage.");
            }
            else
            {
                Console.WriteLine($"No vehicle with license plate {plate} is parked here.");
            }
        }

        private void FindVehicle()
        {
            string plate = AskLicensePlate("License plate: ");
            Vehicle? vehicle = garage.FindVehicle(plate);

            if (vehicle == null)
            {
                Console.WriteLine($"No vehicle with license plate {plate} is parked here.");
            }
            else
            {
                Console.WriteLine(vehicle);
            }
        }

        private void SearchVehicles()
        {
            Console.WriteLine("Press Enter without typing anything to skip a filter.");

            Console.Write("Type (Car, Motorcycle, Bus, Airplane, Boat): ");
            string type = ReadLine();

            Console.Write("Color: ");
            string color = ReadLine();

            // -1 because some vehicles have 0, so 0 is vcalid input
            int wheels = -1;
            while (true)
            {
                Console.Write("Number of wheels: ");
                string input = ReadLine();
                if (input == "")
                {
                    break;
                }
                if (int.TryParse(input, out wheels) && wheels >= 0)
                {
                    break;
                }
                Console.WriteLine("Please enter a number, or just press Enter to skip.");
            }

            List<Vehicle> result = garage.Search(type, color, wheels);
            PrintVehicles(result);
        }

        private void AddSampleVehicles()
        {
            List<Vehicle> samples = new List<Vehicle>
            {
                new Car("ABC123", "Red", 4, "Gasoline"),
                new Car("RED456", "Red", 6, "Gasoline"),
                new Motorcycle("MCK11A", "Pink", 2, 600),
                new Motorcycle("BRM222", "Orange", 2, 1000),
                new Bus("BUS333", "Yellow", 6, 52),
                new Airplane("FLY44B", "White", 3, 2),
                new Boat("SEA555", "Blue", 0, 8),
            };

            int added = 0;
            foreach (Vehicle vehicle in samples)
            {
                //park returns false when the garage is full or same plate is there
                if (garage.Park(vehicle))
                {
                    added++;
                }
            }

            Console.WriteLine($"{added} sample vehicle(s) were parked.");
        }

        private void PrintVehicles(List<Vehicle> vehicles)
        {
            if (vehicles.Count == 0)
            {
                Console.WriteLine("No vehicles found.");
                return;
            }

            foreach (Vehicle vehicle in vehicles)
            {
                Console.WriteLine(vehicle);
            }
            Console.WriteLine($"{vehicles.Count} vehicle(s).");
        }

        //helper methods

        //reads one line. "?? """ means: if ReadLine gives back null, use "" instead.
        private string ReadLine()
        {
            string input = Console.ReadLine() ?? "";
            return input.Trim();
        }

        private string AskText(string question)
        {
            while (true)
            {
                Console.Write(question);
                string input = ReadLine();
                if (input != "")
                {
                    return input;
                }
                Console.WriteLine("You must type something.");
            }
        }

        private string AskLicensePlate(string question)
        {
            while (true)
            {
                string plate = AskText(question).Replace(" ", "").ToUpper();
                if (Vehicle.IsValidLicensePlate(plate))
                {
                    return plate;
                }
                Console.WriteLine("Not a valid swedish license plate, must be ABC123 or ABC12A.");
            }
        }

        private int AskNumber(string question, int min, int max)
        {
            while (true)
            {
                Console.Write(question);
                string input = ReadLine();
                if (int.TryParse(input, out int number) && number >= min && number <= max)
                {
                    return number;
                }
                Console.WriteLine($"Please enter a whole number from {min} to {max}.");
            }
        }
    }
}
