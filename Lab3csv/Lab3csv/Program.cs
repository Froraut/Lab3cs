using System;
using System.Collections.Generic;
namespace lab3csv

{
    class Program
    {
        static void Main(string[] args)
        {
            var vehicles = new Dictionary<int, Vehicle>();
            vehicles[1] = new Vehicle("A012B12", 100, 200, 150, 2500, 10000, new DateTime(2002, 10, 10), true);
            vehicles[2] = new Vehicle("CC8D091", 50, 100, 100, 1500);
            while (true)
            {
                Console.WriteLine("1 to show all the vehicles");
                Console.WriteLine("2 to add a new vehicle");
                Console.WriteLine("3 to delete vehicle by id");
                Console.WriteLine("4 to change some vehicle");
                Console.WriteLine("5 to quit the program");
                switch (Choice(1, 5))
                {
                    case 1:
                        Console.Clear();
                        ShowAll(vehicles);
                        break;
                    case 2:
                        Console.Clear();
                        vehicles[Vehicle.Id + 1] = GetNewVehicle();
                        Console.WriteLine("\nCharacteristics of the new car:\n");
                        Console.WriteLine(vehicles[Vehicle.Id].InfoToString());
                        Console.WriteLine("Vehicle has been added!");
                        break;
                    case 3:
                        DeleteVehicle(vehicles);
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 4:
                        ChangeSome(vehicles);
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 5:
                        return;
                }
            }
        }

        static int Choice(int min = int.MinValue, int max = int.MaxValue, string text = "Choice: ")
        {
            int x;
            Console.Write(text);
            while (!int.TryParse(Console.ReadLine(), out x) || x < min || x > max)
            {
                Console.WriteLine("Error! Input a number from {0} to {1}", min, max);
                Console.Write(text);
            }
            return x;
        }

        static double ReadDouble(string text, double min = 0, double max = double.MaxValue)
        {
            double x;
            Console.Write(text);
            while (!double.TryParse(Console.ReadLine().Replace('.', ','), out x) || x < min || x > max)
            {
                Console.WriteLine("Error! It should be a number from {0} to {1}", min, max);
                Console.Write(text);
            }
            return x;
        }

        static void ShowAll(Dictionary<int, Vehicle> vehicles)
        {
            if (vehicles.Count == 0)
            {
                Console.WriteLine("There is no vehicles");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("******************************************************");
            foreach (var a in vehicles)
            {
                Console.Write(a.Value.InfoToString());
                Console.Write("******************************************************");
                Console.ReadLine();
            }
        }

        static string GetSerialNumber()
        {
            while (true)
            {
                Console.Write("Serial number - ");
                string serialNumber = Console.ReadLine();
                if (Vehicle.IsValidSerialNumber(serialNumber))
                {
                    return serialNumber;
                }
                Console.WriteLine("String should contain up to 20 symbols A-Z and 0-9");
            }
        }

        static DateTime GetManufacturingTime()
        {
            Console.WriteLine("Manufacturing time");
            DateTime manufacturingTime;
            while (true)
            {
                try
                {
                    manufacturingTime = new DateTime(Choice(1, 9999, "Year - "), Choice(1, 12, "Mounth - "), Choice(1, 30, "Day - "));
                    if (manufacturingTime < DateTime.Now)
                    {
                        break;
                    }
                    Console.WriteLine("Is the vehicle from the future??");
                }
                catch
                {
                    Console.WriteLine("Such date is not exist!");
                }
            }
            return manufacturingTime;
        }

        static Vehicle GetNewVehicle()
        {
            string serialNumber = GetSerialNumber();
            double weight = ReadDouble("Weight(kg) - ");
            double carryingCapacity = ReadDouble("Carrying capacity(kg) - ");
            double maxSpeed = ReadDouble("MaxSpeed(m/s) - ");
            double cost = ReadDouble("Cost($) - ");
            if (Choice(0, 1, "Is your vehicle new(1/0)? ") == 1)
            {
                return new Vehicle(serialNumber, weight, carryingCapacity, maxSpeed, cost);
            }
            double mileage = ReadDouble("Mileage(m) - ");
            bool isBroken = false;
            if (Choice(0, 1, "Is your vehicle broken(1/0)? ") == 1)
            {
                isBroken = true;
            }
            return new Vehicle(serialNumber, weight, carryingCapacity, maxSpeed, cost, mileage, GetManufacturingTime(), isBroken);
        }

        static int ChooseSpecific(Dictionary<int, Vehicle> vehicles)
        {
            Console.Write("Available vehicles: ");
            foreach (var a in vehicles)
            {
                Console.Write(a.Key + " ");
            }
            Console.WriteLine();
            while (true)
            {
                int choice = Choice();
                if (vehicles.ContainsKey(choice))
                {
                    return choice;
                }
                Console.WriteLine("There is no such vehicle. Try again");
            }
        }

        static void DeleteVehicle(Dictionary<int, Vehicle> vehicles)
        {
            if (vehicles.Count == 0)
            {
                Console.WriteLine("\nThere is no vehicles");
                return;
            }
            Console.WriteLine("\nChoose the id of the vehicle you want to delete");
            int index = ChooseSpecific(vehicles);
            vehicles.Remove(index);
            Console.WriteLine("It has been removed!");
        }

        static void ChangeSome(Dictionary<int, Vehicle> vehicles)
        {
            if (vehicles.Count == 0)
            {
                Console.WriteLine("\nThere is no vehicles");
                return;
            }
            Console.WriteLine("\nChoose the id of the vehicle you want to change");
            int index = ChooseSpecific(vehicles);
            Console.WriteLine("1 to change the serial number");
            Console.WriteLine("2 to run some kilometers");
            Console.WriteLine("3 to repair/break the vehicle");
            Console.WriteLine("4 to do nothing");
            switch (Choice(1, 4))
            {
                case 1:
                    vehicles[index].ChangeSerialNumber(GetSerialNumber());
                    Console.WriteLine("Serial number has been changed!");
                    break;
                case 2:
                    if (vehicles[index].IsBroken)
                    {
                        Console.WriteLine("You can not drive broken vehicle");
                        break;
                    }
                    Console.WriteLine("Input the distance you want to drive");
                    vehicles[index].Run(ReadDouble("Length(m) = "));
                    Console.WriteLine("OK! Current mileage is {0}", vehicles[index].Mileage);
                    break;
                case 3:
                    Console.WriteLine("1 to repair");
                    Console.WriteLine("2 to break it");
                    int choice = Choice(1, 2);
                    if (choice == 1 && vehicles[index].IsBroken == false)
                    {
                        Console.WriteLine("It's already not broken");
                        break;
                    }
                    if (choice == 2 && vehicles[index].IsBroken == true)
                    {
                        Console.WriteLine("It's already broken");
                        break;
                    }
                    if (choice == 1)
                    {
                        vehicles[index].Repair();
                        Console.WriteLine("it has been repaired");
                        break;
                    }
                    vehicles[index].Break();
                    Console.WriteLine("it has been broken");
                    break;
                case 4:
                    Console.WriteLine("Nothing has been changed");
                    return;
            }
        }

    }
}
