using System;
using System.Collections.Generic;
using UnityEngine;
using Util;
using Random = UnityEngine.Random;

namespace Items {
    public static class LootTableManager {
        public static Item GetRandomLoot(LootTable type, float dropChance) {
            return GetRandomLoot(GetLootTable(type), dropChance);
        }

        public static Item GetRandomLoot(List<KeyValuePair<Item, float>> items, float dropChance) {
            // Get the total value of all values
            var total = GetTotalOfValues(items, 0, 0.0f);

            var dropChanceInContext = (float) (-0.15 + 8.5f * Math.Pow(2.7, (-4 * dropChance))) * total;
            /* This function is based on the following points:

            dropchance = 1;
            value = total * 0;
            - if the dropchance is 1 we want 0% of the loot table to be nothing -

            dropchance = 0.75;
            value = total * (1/3);
            - if the dropchance is 0.75 we want 75% of the total to drop something,
                meaning we want 25% to be nothing
                if total = 100% we want it to become 75% of the new total;
                1 / 0.75 = 1(1/3) | since we wanted 25% to drop nothing we have to remove 75% of 1(1/3)
                75% was the original total, in this example that is 1
                this leaves us with a value of (1/3) -

            dropchance = 0.5;
            value = total * 1;
            - if the dropchance is 0.5 we want 50% of the total to drop something,
                1 / 0.5 = 2 | remove the 50% that was the original total, leaving us with 1 -

            dropchance = 0.25;
            value = total * 3;
            - if the dropchance is 0.25 we want 25% of the total to drop something,
                again => 1 / 0.25 = 4 => 4 - 1 = 3 => 0.25 leaves us with 3 times the total -

            function is generated using https://mycurvefit.com/
            full, unsimplified, function is y = -0.1331164 + 8.490543*e^(-3.991808*x)
            */

            if (dropChanceInContext < 0) {
                dropChanceInContext = 0;
            }

            items.Add(new KeyValuePair<Item, float>(Item.Null, total * dropChanceInContext));

            Debug.Log(total);
            Debug.Log(dropChanceInContext);

            var itemNr = Random.Range(0.0f, total + total * dropChanceInContext);

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

        public static List<KeyValuePair<Item, float>> GetLootTable(LootTable type) {
            switch (type) {
                case LootTable.Potions:
                    return PotionLootTable;
                case LootTable.Weapons:
                    return WeaponLootTable;
                default:
                    return DefaultLootTable;
            }
        }

        private static List<KeyValuePair<Item, float>> DefaultLootTable =
            new List<KeyValuePair<Item, float>>() {
                new KeyValuePair<Item, float>(Item.HealthPot, 1.0f),
                new KeyValuePair<Item, float>(Item.HealthRegenPot, 1.0f),
                new KeyValuePair<Item, float>(Item.DamagePot, 1.0f),
                new KeyValuePair<Item, float>(Item.DefensePot, 1.0f),
                new KeyValuePair<Item, float>(Item.SpeedPot, 1.0f),
                new KeyValuePair<Item, float>(Item.GuidancePot, 1.0f),
                new KeyValuePair<Item, float>(Item.Sword, 1.0f),
                new KeyValuePair<Item, float>(Item.BattleAxe, 1.0f),
                new KeyValuePair<Item, float>(Item.Maul, 1.0f),
                new KeyValuePair<Item, float>(Item.Dagger, 1.0f)
            };

        private static List<KeyValuePair<Item, float>> PotionLootTable =
            new List<KeyValuePair<Item, float>>() {
                new KeyValuePair<Item, float>(Item.HealthPot, 1.0f),
                new KeyValuePair<Item, float>(Item.HealthRegenPot, 1.0f),
                new KeyValuePair<Item, float>(Item.DamagePot, 1.0f),
                new KeyValuePair<Item, float>(Item.DefensePot, 1.0f),
                new KeyValuePair<Item, float>(Item.SpeedPot, 1.0f),
                new KeyValuePair<Item, float>(Item.GuidancePot, 1.0f)
            };

        private static List<KeyValuePair<Item, float>> WeaponLootTable =
            new List<KeyValuePair<Item, float>>() {
                new KeyValuePair<Item, float>(Item.Sword, 1.0f),
                new KeyValuePair<Item, float>(Item.BattleAxe, 1.0f),
                new KeyValuePair<Item, float>(Item.Maul, 1.0f),
                new KeyValuePair<Item, float>(Item.Dagger, 1.0f)
            };
    }

    public enum LootTable {
        Default,
        Potions,
        Weapons
    }
}