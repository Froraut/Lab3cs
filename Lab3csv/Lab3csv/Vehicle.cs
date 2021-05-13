using System;
using System.Text;

namespace lab3csv

{
    class Vehicle
    {
        public string SerialNumber { get; private set; }
        public double Weight { get; private set; }
        public double CarryingCapacity { get; private set; }
        public double MaxSpeed { get; private set; }
        public double Cost { get; private set; }
        public double Mileage { get; private set; }
        public DateTime ManufacturingTime { get; private set; }
        public bool IsBroken { get; private set; }
        public int MyId { get; private set; }
        public static int Id = 0;

        public Vehicle(string serialNumber, double weight, double carryingCapacity, double maxSpeed, double cost)
        {
            SerialNumber = serialNumber;
            Weight = weight;
            CarryingCapacity = carryingCapacity;
            MaxSpeed = maxSpeed;
            Cost = cost;
            Mileage = 0;
            ManufacturingTime = DateTime.Now;
            IsBroken = false;
            MyId = ++Id;
        }

        public Vehicle(string serialNumber, double weight, double carryingCapacity, double maxSpeed, double cost, double mileage, DateTime manufacturingTime, bool isBroken = false)
            : this(serialNumber, weight, carryingCapacity, maxSpeed, cost)
        {
            ManufacturingTime = manufacturingTime;
            Mileage = mileage;
            IsBroken = isBroken;
        }

        static public bool IsValidSerialNumber(string serialNumber)
        {
            if (serialNumber.Length > 20)
            {
                return false;
            }
            foreach (var ch in serialNumber)
            {
                if ((ch < 'A' || ch > 'Z') && (ch < '0' || ch > '9'))
                {
                    return false;
                }
            }
            return true;
        }

        public void ChangeSerialNumber(string newSerialNumber)
        {
            if (!IsValidSerialNumber(newSerialNumber))
            {
                return;
            }
            SerialNumber = newSerialNumber;
        }

        public string InfoToString()
        {
            StringBuilder info = new StringBuilder();
            info.AppendLine($"Id: {MyId}");
            info.AppendLine($"Serial number: {SerialNumber}");
            info.AppendLine($"Weight: { Weight}kg");
            info.AppendLine($"Carrying capacity: {CarryingCapacity}kg");
            info.AppendLine($"Max speed: {MaxSpeed}m/s");
            info.AppendLine($"Mileage: {Mileage}m");
            info.AppendLine($"Cost: {Cost}$");
            info.AppendLine($"Manufacturing time: { ManufacturingTime.ToShortDateString()}, {GetHowManyYears().ToString()} year(s)");
            info.AppendLine(IsBroken ? "Vehicle is broken!" : "Vehicle is not broken");
            return info.ToString();
        }

        public int GetHowManyYears()
        {
            return DateTime.Now.Year - ManufacturingTime.Year;
        }

        public void Run()
        {
            Mileage++;
        }

        public void Run(double distance)
        {
            Mileage += distance;
        }

        public void Repair()
        {
            IsBroken = false;
        }

        public void Break()
        {
            IsBroken = true;
        }

    }
}
