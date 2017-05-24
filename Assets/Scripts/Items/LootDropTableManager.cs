using System.Collections.Generic;
using Util;
using Random = UnityEngine.Random;

namespace Items {
    public static class LootDropTableManager {
        public static Item GetRandomLoot(List<KeyValuePair<Item, float>> items) {
            var itemNr = Random.Range(0, GetTotalOfValues(items, 0, 0.0f));

            var chance = 0.0f;

            foreach (var item in items) {
                if (itemNr >= chance && itemNr < item.Value + chance) {
                    return item.Key;
                }
                chance += item.Value;
            }
            return Item.Null;
        }

        private static float GetTotalOfValues(List<KeyValuePair<Item, float>> list,
            int index, float total) {
            if (index >= list.Count) return 0.0f;

            var currentItem = list[index];
            index++;
            total += GetTotalOfValues(list, index, total);
            total += currentItem.Value;
            return total;
        }

        public static List<KeyValuePair<Item, float>> Default = new List<KeyValuePair<Item, float>>() {
            new KeyValuePair<Item, float>(Item.HealthPot, 1.0f),
            new KeyValuePair<Item, float>(Item.HealthRegenPot, 1.0f),
            new KeyValuePair<Item, float>(Item.DamagePot, 1.0f),
            new KeyValuePair<Item, float>(Item.DefensePot, 1.0f),
            new KeyValuePair<Item, float>(Item.SpeedPot, 1.0f),
            new KeyValuePair<Item, float>(Item.GuidancePot, 1.0f),
            new KeyValuePair<Item, float>(Item.Null, 5.0f)
        };
    }
}