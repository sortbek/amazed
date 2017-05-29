using System;
using System.Collections.Generic;
using Util;
using Random = UnityEngine.Random;

namespace Items
{
    public static class LootTableManager
    {
        public static Item GetRandomLoot(LootTable type, float dropChance)
        {
            return GetRandomLoot(GetLootTable(type), dropChance);
        }

        public static Item GetRandomLoot(List<KeyValuePair<Item, float>> items, float dropRate)
        {
            // Get the total value of all values
            var total = GetTotalOfValues(items, 0, 0.0f);
            // Convert dropRate (in %) to a usable value in the context of 'items'
            var dropRateInContext = GetDropRateInContext(dropRate, total);

            var temp = ApplyDropRate(items, dropRateInContext);
            
            var itemNr = GetRandomItemNr(total, dropRateInContext);

            var chance = 0.0f;

            foreach (var item in temp)
            {
                if (itemNr >= chance && itemNr < item.Value + chance)
                {
                    return item.Key;
                }
                chance += item.Value;
            }
            return Item.Null;
        }

        public static float GetRandomItemNr(float total, float dropChanceInContext)
        {
            return Random.Range(0.0f, total + dropChanceInContext);
        }

        public static float GetDropRateInContext(float dropRate, float total)
        {
            // If the droprate is 1 we want 'nothing' to drop 0 percent of the time
            //  the function below i only an approximation
            if (dropRate >= 1) return 0;
            // If the droprate is 0 the best we can do it return a maximum value - this value is dependent on the total
            //  because we multiply it with the total we will now divide it - later we will also add total so now we subtract it
            if (dropRate <= 0) return int.MaxValue / total - total;

            var dropChanceInContext = (float)(-0.15 + 8.5f * Math.Pow(2.7, (-4 * dropRate))) * total;
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

            if (dropChanceInContext < 0)
            {
                dropChanceInContext = 0;
            }

            return dropChanceInContext;
        }

        public static List<KeyValuePair<Item, float>> ApplyDropRate(List<KeyValuePair<Item, float>> items, float dropRateInContext)
        {
            var temp = new List<KeyValuePair<Item, float>>(items);
            temp.Add(new KeyValuePair<Item, float>(Item.Null, dropRateInContext));
            return temp;
        }

        public static float GetTotalOfValues(List<KeyValuePair<Item, float>> list,
            int index, float total)
        {
            if (index >= list.Count) return 0.0f;

            var currentItem = list[index];
            index++;
            total += GetTotalOfValues(list, index, total);
            total += currentItem.Value;
            return total;
        }

        public static List<KeyValuePair<Item, float>> GetLootTable(LootTable type)
        {
            switch (type)
            {
                case LootTable.Potions:
                    return PotionLootTable;
                case LootTable.Weapons:
                    return WeaponLootTable;
                default:
                    return DefaultLootTable;
            }
        }

        private static readonly List<KeyValuePair<Item, float>> DefaultLootTable =
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

        private static readonly List<KeyValuePair<Item, float>> PotionLootTable =
            new List<KeyValuePair<Item, float>>() {
                new KeyValuePair<Item, float>(Item.HealthPot, 1.0f),
                new KeyValuePair<Item, float>(Item.HealthRegenPot, 1.0f),
                new KeyValuePair<Item, float>(Item.DamagePot, 1.0f),
                new KeyValuePair<Item, float>(Item.DefensePot, 1.0f),
                new KeyValuePair<Item, float>(Item.SpeedPot, 1.0f),
                new KeyValuePair<Item, float>(Item.GuidancePot, 1.0f)
            };

        private static readonly List<KeyValuePair<Item, float>> WeaponLootTable =
            new List<KeyValuePair<Item, float>>() {
                new KeyValuePair<Item, float>(Item.Sword, 1.0f),
                new KeyValuePair<Item, float>(Item.BattleAxe, 1.0f),
                new KeyValuePair<Item, float>(Item.Maul, 1.0f),
                new KeyValuePair<Item, float>(Item.Dagger, 1.0f)
            };
    }

    public enum LootTable
    {
        Default,
        Potions,
        Weapons
    }
}