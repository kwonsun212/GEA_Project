using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeData : MonoBehaviour
{
    public enum PickaxeTier
    {
        Wood = 0,
        Stone = 1,
        Iron = 2,
        Gold = 3,
        Diamond = 4
    }
    [System.Serializable]
    public class PickaxeInfo 
    {
        public ItemType type;
        public PickaxeTier tier;
        public int damage;
    }

    public static class PickaxeDatabase
    {
        public static readonly PickaxeInfo[] infos =
        {
        new PickaxeInfo{ type = ItemType.WoodPickaxe, tier = PickaxeTier.Stone, damage = 1 },
        new PickaxeInfo{ type = ItemType.StonePickaxe, tier = PickaxeTier.Iron, damage = 2 },
        new PickaxeInfo{ type = ItemType.IronPickaxe, tier = PickaxeTier.Gold, damage = 3 },
        new PickaxeInfo{ type = ItemType.GoldPickaxe, tier = PickaxeTier.Diamond, damage = 4 },
        new PickaxeInfo{ type = ItemType.DiamondPickaxe, tier = PickaxeTier.Diamond, damage = 5 },
    };

        public static PickaxeInfo Get(ItemType type)
        {
            foreach (var info in infos)
                if (info.type == type)
                    return info;
            return null;
        }
    }
}
