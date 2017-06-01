
// Created By:
// Niek van den Brink
// S1078937
namespace Util {
    public enum Item {
        Null,
        HealthPot,
        HealthRegenPot,
        DamagePot,
        DefensePot,
        SpeedPot,
        GuidancePot,
        Sword,
        BattleAxe,
        Maul,
        Dagger
    }

    public static class ItemUtil {
        public static string ItemToString(Item item) {
            switch (item) {
                case Item.Null:
                    return "Nothing";
                case Item.HealthPot:
                    return "Health Potion";
                case Item.HealthRegenPot:
                    return "Health Regeneration Potion";
                case Item.DamagePot:
                    return "Damage Potion";
                case Item.DefensePot:
                    return "Defense Potion";
                case Item.SpeedPot:
                    return "Speed Potion";
                case Item.GuidancePot:
                    return "Guidance Potion";
                case Item.Sword:
                    return "Sword";
                case Item.BattleAxe:
                    return "Battle Axe";
                case Item.Maul:
                    return "Maul";
                case Item.Dagger:
                    return "Dagger";
                default:
                    return "Nothing";
            }
        }
    }
}