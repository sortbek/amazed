using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public static class LootDropTableManager {
    public static String GetRandomLoot() {
        List<KeyValuePair<String, int>> items = new List<KeyValuePair<string, int>>();
        items.Add(new KeyValuePair<string, int>("Health Regeneration Potion", 20));
        items.Add(new KeyValuePair<string, int>("Speed Potion", 20));
        items.Add(new KeyValuePair<string, int>("Damage Potion", 20));
        items.Add(new KeyValuePair<string, int>("Defence Potion", 20));
        items.Add(new KeyValuePair<string, int>("Health Potion", 20));
        // the numbers must allways add up to 100
        var itemNr = Random.RandomRange(0, 101);

        int chance = 1;

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
}