using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utils
{
    [Serializable]
    public class RandomPrefab : IRandomEntry<GameObject>
    {
        [SerializeField] private GameObject objectPrefab = default;
        [SerializeField] private float chance = default;


        public double GetWeight() => chance;

        public GameObject GetItem() => objectPrefab;
    }

    public interface IRandomEntry<T>
    {
        double GetWeight();

        T GetItem();
    }

    public class WeightedRandom<T>
    {
        private readonly List<Entry> _entries = new List<Entry>();
        private double _accumulatedWeight;

        public int Count => _entries.Count;

        public WeightedRandom()
        {
        }

        public WeightedRandom(IRandomEntry<T>[] collection)
        {
            for (var index = 0; index < collection.Length; index++)
            {
                AddEntry(collection[index]);
            }
        }

        private struct Entry
        {
            public double accumulatedWeight;
            public T item;
        }

        public void AddEntry(IRandomEntry<T> randomEntry)
        {
            _accumulatedWeight += randomEntry.GetWeight();
            _entries.Add(new Entry {item = randomEntry.GetItem(), accumulatedWeight = _accumulatedWeight});
        }

        public T GetRandom()
        {
            double r = Random.value * _accumulatedWeight;

            foreach (var entry in _entries)
            {
                if (entry.accumulatedWeight >= r)
                {
                    return entry.item;
                }
            }

            Debug.LogError("Couldn't find random element!");
            return default(T); //should only happen when there are no entries
        }
    }
}