using System;
using System.Collections.Generic;
using System.Text;

namespace vehicles.Vehicles
{
    public class Garage
    {
        private List<Vehicle> vehicles;

        private int capacity;

        public Garage(int capacity)
        {
            this.capacity = capacity;
            vehicles = new List<Vehicle>();
        }

        public int GetCapacity()
        {
            return capacity;
        }

        public int CountVehicles()
        {
            return vehicles.Count;
        }

        public bool IsFull()
        {
            return vehicles.Count >= capacity;
        }

        // Returns false if the garage is full or the plate is already parked here.
        public bool Park(Vehicle vehicle)
        {
            if (IsFull())
            {
                return false;
            }
            if (FindVehicle(vehicle.LicensePlateNumber) != null)
            {
                return false;
            }

            vehicles.Add(vehicle);
            return true;
        }

        public bool Remove(string licensePlateNumber)
        {
            Vehicle? vehicle = FindVehicle(licensePlateNumber);
            if (vehicle == null)
            {
                return false;
            }

            vehicles.Remove(vehicle);
            return true;
        }

        // Returns the vehicle, or null if it isn't parked here.
        public Vehicle? FindVehicle(string licensePlateNumber)
        {
            foreach (Vehicle vehicle in vehicles)
            {
                if (vehicle.LicensePlateNumber == licensePlateNumber.ToUpper())
                {
                    return vehicle;
                }
            }
            return null;
        }

        public List<Vehicle> GetAllVehicles()
        {
            return new List<Vehicle>(vehicles);
        }

        public int CountType(string typeName)
        {
            int count = 0;
            foreach (Vehicle vehicle in vehicles)
            {
                if (vehicle.GetTypeName() == typeName)
                {
                    count++;
                }
            }
            return count;
        }

        //empty "" = anything
        //-1 wheels = any number of wheels
        public List<Vehicle> Search(string type, string color, int numberOfWheels)
        {
            List<Vehicle> result = new List<Vehicle>();
            foreach (Vehicle vehicle in vehicles)
            {
                if (Matches(vehicle, type, color, numberOfWheels))
                {
                    result.Add(vehicle);
                }
            }
            return result;
        }

        private bool Matches(Vehicle vehicle, string type, string color, int numberOfWheels)
        {
            if (type != "" && vehicle.GetTypeName().ToLower() != type.ToLower())
            {
                return false;
            }
            if (color != "" && vehicle.Color.ToLower() != color.ToLower())
            {
                return false;
            }
            if (numberOfWheels != -1 && vehicle.NumberOfWheels != numberOfWheels)
            {
                return false;
            }
            return true;
        }
    }
}
