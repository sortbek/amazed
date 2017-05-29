using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Util;

namespace Items.Tests
{
    [TestClass()]
    public class LootTableManagerTests
    {
        static Random r;
        public int sampleSize;
        public int errorMargin;

        [TestInitialize()]
        public void Start()
        {
            r = new Random();
            sampleSize = 1000;
            errorMargin = 6;
        }

        [TestMethod()]
        public void GetRandomLootTest_100()
        {
            var dropRate = 1.0f;
            var result = new Dictionary<Item, int>(GetRandomLootTest(dropRate));
            PrintResult(result, dropRate);
        }

        [TestMethod()]
        public void GetRandomLootTest_75()
        {
            var dropRate = 0.75f;
            var result = new Dictionary<Item, int>(GetRandomLootTest(dropRate));
            PrintResult(result, dropRate);
        }

        [TestMethod()]
        public void GetRandomLootTest_50()
        {
            var dropRate = 0.5f;
            var result = new Dictionary<Item, int>(GetRandomLootTest(dropRate));
            PrintResult(result, dropRate);
        }

        [TestMethod()]
        public void GetRandomLootTest_25()
        {
            var dropRate = 0.25f;
            var result = new Dictionary<Item, int>(GetRandomLootTest(dropRate));
            PrintResult(result, dropRate);
        }

        [TestMethod()]
        public void GetRandomLootTest_00()
        {
            var dropRate = 0.0f;
            var result = new Dictionary<Item, int>(GetRandomLootTest(dropRate));
            PrintResult(result, dropRate);
        }

        /* OUTPUT RESULTS - DOES NOT CHANGE FUNCTIONALITY OF TESTED ITEMS */
        public void PrintResult(Dictionary<Item, int> result, float dropRate)
        {
            var droprate = (int)(dropRate * 100);
            Console.WriteLine(String.Format("Drop Rate = {0}%", droprate));
            Console.WriteLine(String.Format("Allowed Margin of Error = {0}%", errorMargin));
            var margin = Math.Round(Math.Abs(droprate - Math.Abs(100 - ((double)result[Item.Null] / (double)sampleSize) * 100)), 2);
            Console.WriteLine(String.Format("Actual Margin = {0}%", margin));
            Console.WriteLine();

            var orderedList = result.OrderBy(x => orderMap[x.Key]);

            foreach (var item in orderedList)
            {
                Console.WriteLine(String.Format("{1}% \t {0}", item.Key, ((double)item.Value / (double)sampleSize) * 100));
            }
        }

        /* PREPERATION FOR TEST SCENARIOS - DOES NOT CHANGE FUNCTIONALITY OF TESTED ITEMS */
        public Dictionary<Item, int> GetRandomLootTest(float chance)
        {
            var results = new Dictionary<Item, int>();
            var defaultTable = LootTableManager.GetLootTable(LootTable.Default);
            for (int i = 0; i < sampleSize; i++)
            {
                var table = new List<KeyValuePair<Item, float>>(defaultTable);

                var item = GetRandomLootTest(table, chance);

                if (results.ContainsKey(item))
                {
                    results[item] += 1;
                }
                else
                {
                    results.Add(item, 1);
                }
            }

            if (results.ContainsKey(Item.Null))
            {
                Assert.IsTrue(results[Item.Null] <= sampleSize * (1 - chance + errorMargin * 0.01f) && results[Item.Null] >= sampleSize * (1 - chance - errorMargin * 0.01f));
            }
            else
            {
                results.Add(Item.Null, 0);
                Assert.IsTrue(true);
            }

            return results;
        }
        
        /* COPY OF CODE TO BE TESTED - MIGHT CHANGE FUNCTIONALITY OF TESTE ITEMS */
        /*      MAKE SURE THIS CODE IS ROUGHLY THE SAME AS THE TESTED ITEM       */
        public static Item GetRandomLootTest(List<KeyValuePair<Item, float>> items, float dropRate)
        {
            // Get the total value of all values
            var total = LootTableManager.GetTotalOfValues(items, 0, 0.0f);
            // Convert dropRate (in %) to a usable value in the context of 'items'
            var dropRateInContext = LootTableManager.GetDropRateInContext(dropRate, total);

            var temp = LootTableManager.ApplyDropRate(items, dropRateInContext);

            // Different from tested code due to use of UnityEngine.Random
            var itemNr = GetRandomItemNr(0.0, total + dropRateInContext);

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

        /* ALTERNATE FUNCTION TO GET RANDOM NUMBER - SHOULD NOT CHANCE FUNCTIONALITY OF TESTED ITEMS */
        static double GetRandomItemNr(double a, double b)
        {
            return a + r.NextDouble() * (b - a);
        }

        /* USED TO ORDER RESULTS - DOES NOT CHANCE FUNCTIONALITY OF TESTED ITEMS */
        public static Dictionary<Item, int> orderMap = new Dictionary<Item, int>() {
            { Item.Null, 0 },
            { Item.HealthPot, 1 },
            { Item.HealthRegenPot, 2 },
            { Item.DamagePot, 3 },
            { Item.DefensePot, 4 },
            { Item.SpeedPot, 5 },
            { Item.GuidancePot, 6 },
            { Item.Sword, 7 },
            { Item.BattleAxe, 8 },
            { Item.Maul, 9 },
            { Item.Dagger, 10 },
        };
    }
}