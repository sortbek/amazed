using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Items {
    public static class LootDropTableManager {
        // TODO: Replace KeyValuePair<String, int> with KeyValuePair<ItemClass, int>

        public static string GetRandomLoot(List<KeyValuePair<string, float>> items) {
            var itemNr = Random.RandomRange(0, 101);

            var chance = 1.0f;

            foreach (var item in items) {
                if (itemNr >= chance && itemNr < item.Value + chance) {
                    return item.Key;
                }
                else {
                    chance += item.Value;
                }
            }
            return null;
        }

        public static List<KeyValuePair<string, float>> Default = new List<KeyValuePair<string, float>>() {
            new KeyValuePair<string, float>("Health Regeneration Potion", 16.0f),
            new KeyValuePair<string, float>("Speed Potion", 16.0f),
            new KeyValuePair<string, float>("Damage Potion", 16.0f),
            new KeyValuePair<string, float>("Defence Potion", 16.0f),
            new KeyValuePair<string, float>("Health Potion", 16.0f),
            new KeyValuePair<string, float>("Nothing", 20.0f)
        };
    }
}