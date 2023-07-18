namespace Utilities
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class GachaHelper<TItem>
    {
        public class Entry
        {
            public TItem Item   { get; set; }
            public int   Weight { get; set; }

            public override int GetHashCode()
            {
                return this.Item.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                return obj is Entry entry && this.Item.Equals(entry.Item);
            }
        }

        public HashSet<Entry> Entries { get; private set; } = new();

        public void Add(TItem item, int weight)
        {
            this.Entries.Add(new Entry { Item = item, Weight = weight });
        }

        public void Remove(TItem item)
        {
            this.Entries.Remove(new Entry { Item = item });
        }

        public TItem Get()
        {
            var totalWeight = this.Entries.Sum(entry => entry.Weight);

            var random = Random.Range(0, totalWeight);

            var currentWeight = 0;
            foreach (var entry in this.Entries)
            {
                currentWeight += entry.Weight;

                if (random < currentWeight)
                {
                    return entry.Item;
                }
            }

            return default;
        }
    }

    public static class GachaExtensions
    {
        public static T GetRandom<T>(this IEnumerable<T> items)
        {
            var enumerable = items.ToList();
            return enumerable.ElementAt(Random.Range(0, enumerable.Count()));
        }
    }
}