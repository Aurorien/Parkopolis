﻿using Parkopolis.Interfaces;

namespace Parkopolis
{
    abstract class Vehicle : IVehicle
    {
        public string RegNum { get; }
        public string Color { get; }
        public bool NeedsElectricalStation { get; }


        public Vehicle(string regNum, string color, bool needsElectricalStation)
        {
            this.RegNum = regNum;
            this.Color = color;
            this.NeedsElectricalStation = needsElectricalStation;
        }

        public override string ToString()
        {
            return $"\nType: {GetType().Name} \n" +
                    $"Registration number: {RegNum} \n" +
                    $"Color: {Color} \n" +
                    $"Needs electrical station: {(NeedsElectricalStation ? "Yes" : "No")} \n";
        }
    }
}
