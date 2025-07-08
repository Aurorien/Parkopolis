using Parkopolis.Interfaces;
using System.Collections;

namespace Parkopolis
{
    public class Garage<T> : IEnumerable<T> where T : IVehicle
    {
        private T[] _vehicles;
        public int Capacity { get; private set; }
        public int Count { get; private set; }

        public Garage(int capacity)
        {
            Capacity = capacity;
            Count = 0;
            _vehicles = new T[Capacity];
        }


        //ToDo : Move this logic to GarageHandler ?
        public bool IsFull => Count >= Capacity;
        public bool IsEmpty => Count == 0;
        //


        //ToDo : Move this logic to GarageHandler ?
        public bool IsRegNumExists(string regNum)
        {
            return _vehicles.Any(vehicle => vehicle != null && vehicle.RegNum == regNum);
        }
        //

        public string Add(T vehicle)
        {
            //ToDo : Move this logic to GarageHandler ?
            if (IsFull)
                return ("Adding vehicle fails. Garage is full.");
            else if (IsRegNumExists(vehicle.RegNum))
                return ($"Adding vehicle. A vehicle with registration number {vehicle.RegNum} are already in the garage.");
            //

            int firstEmptyIndex = Array.FindIndex(_vehicles, v => v == null);

            if (firstEmptyIndex >= 0)
            {
                _vehicles[firstEmptyIndex] = vehicle;
                Count++;
                return ("Success adding vehicle.");
            }

            return ("Unexpected error: No free spots found.");
        }

        public string Remove(string regNum)
        {

            int index = Array.FindIndex(_vehicles, vehicle =>
                vehicle != null && vehicle.RegNum.ToLower() == regNum.ToLower());


            if (index >= 0)
            {
                _vehicles[index] = default;
                Count--;
                return "Vehicle removed successfully.";
            }

            return "Vehicle not found.";
        }


        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in _vehicles)
            {
                if (item is not null)
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}