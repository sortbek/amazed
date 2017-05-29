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
                    break;
                case Item.HealthPot:
                    return "Health Potion";
                    break;
                case Item.HealthRegenPot:
                    return "Health Regenration Potion";
                    break;
                case Item.DamagePot:
                    return "Damage Potion";
                    break;
                case Item.DefensePot:
                    return "Defense Potion";
                    break;
                case Item.SpeedPot:
                    return "Speed Potion";
                    break;
                case Item.GuidancePot:
                    return "Guidance Potion";
                    break;
                case Item.Sword:
                    return "Sword";
                    break;
                case Item.BattleAxe:
                    return "Battle Axe";
                    break;
                case Item.Maul:
                    return "Maul";
                    break;
                case Item.Dagger:
                    return "Dagger";
                    break;
                default:
                    return "Nothing";
                    break;
            }
        }
    }
}