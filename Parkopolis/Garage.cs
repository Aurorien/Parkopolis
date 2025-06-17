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