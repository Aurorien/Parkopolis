using Parkopolis.Interfaces;
using System.Collections;

namespace Parkopolis
{
    internal class Garage<T> : IEnumerable<T> where T : IVehicle
    {
        private int _capacity;
        private int _count;
        private T[] _vehicles;

        public Garage(int capacity)
        {
            _capacity = capacity;
            _count = 0;
            _vehicles = new T[_capacity];
        }

        public int Count => _count;
        public bool IsFull => _count >= _capacity;


        public bool IsRegNumExists(string regNum)
        {
            for (int i = 0; i < _capacity; i++)
            {
                if (_vehicles[i] != null && _vehicles[i].RegNum == regNum)
                {
                    return true;
                }
            }
            return false;
        }

        public string Add(T vehicle)
        {
            if (IsFull)
                return ("Adding vehicle fails. Garage is full.");
            else if (IsRegNumExists(vehicle.RegNum))
                return ($"Adding vehicle. A vehicle with registration number {vehicle.RegNum} are already in the garage.");

            _vehicles[_count] = vehicle;
            _count++;
            return ("Success adding vehicle.");
        }


        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in _vehicles)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}