using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PickaxeData;

public enum ItemType
{

    Air,//ºó °ø°£

    Dirt,
    Grass,
    Water,
    Stone,

    IronOre,
    GoldOre,
    DiamondOre,

    // Pickaxes
    WoodPickaxe,
    StonePickaxe,
    IronPickaxe,
    GoldPickaxe,
    DiamondPickaxe,

    weed,
    SnoopDogg
}

public class Block : MonoBehaviour
{
    [Header("Block Stat")]
    public ItemType type = ItemType.Dirt;
    public int maxHp = 3;
    [HideInInspector] public int hp;

    public int dropCount = 1;
    public bool mineable = true;

    [Header("Mining Requirement")]
    public PickaxeTier requiredTier = PickaxeTier.Wood;

    private void Awake()
    {
        hp = maxHp;

        if (GetComponent<Collider>() == null)
            gameObject.AddComponent<BoxCollider>();

        if (string.IsNullOrEmpty(gameObject.tag) || gameObject.tag == "Untagged")
            gameObject.tag = "Block";
    }

    public void Hit(int damage, Inventory inven)
    {
        if (!mineable) return;

        hp -= damage;

        if (hp <= 0)
        {
            if (inven != null && dropCount > 0)
                inven.Add(type, dropCount);

            Destroy(gameObject);
        }
    }
}
