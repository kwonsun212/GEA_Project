using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class InvntoryUI : MonoBehaviour
{
    public Sprite Dirt;
    public Sprite Water;
    public Sprite Stone;
    public Sprite Grass;
    public Sprite weedSprite;
    public Sprite SnoopDoggSprite;

    [Header("Ores")]
    public Sprite IronOre;
    public Sprite GoldOre;
    public Sprite DiamondOre;

    [Header("Pickaxes")]
    public Sprite WoodPickaxe;
    public Sprite StonePickaxe;
    public Sprite IronPickaxe;
    public Sprite GoldPickaxe;
    public Sprite DiamondPickaxe;


    public List<Transform> Slot;
    public GameObject SlotItem;
    List<GameObject> items = new List<GameObject>();

    public int selectedIndex = -1;

    private void Update()
    {
        for(int i = 0; i < Mathf.Min(9, Slot.Count); i++)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SetSelectedIndex(i);
            }
        }
    }
    public void SetSelectedIndex(int idx)
    {
        ResetSelection();
        if(selectedIndex == idx)
        {
            selectedIndex = -1;
        }
        else
        {
            if(idx >= items.Count)
            {
                selectedIndex = -1;
            }
            else
            {
                SetSelection(idx);
                selectedIndex = idx;
            }
        }
    }

    public void ResetSelection()
    {
        foreach(var slot in Slot)
        {
            slot.GetComponent<Image>().color = Color.white; 
        }
    }

    void SetSelection(int _idx)
    {
        Slot[_idx].GetComponent<Image>().color = Color.yellow;
    }
    public ItemType GetInventorySlot()
    {
        return items[selectedIndex].GetComponent<SlotitemPrefab>().blockType;
    }

    //인벤토리 업데이트 시 호출
    public void UpdateInventory(Inventory myInven)
    {
        //1.기존 슬롯 초기화
        foreach(var slotItems in items)
        {
            Destroy(slotItems);
        }
        items.Clear();
        //2.내 인벤토리 데이터를 전테 탐색

        int idx = 0;
        foreach(var item in myInven.items)
        {
            if (idx >= Slot.Count)
            {
                Debug.LogWarning("인벤토리 슬롯이 부족합니다!");
                break;
            }

            var go = Instantiate(SlotItem, Slot[idx].transform);
            go.transform.localPosition = Vector3.zero;

            SlotitemPrefab sItem = go.GetComponent<SlotitemPrefab>();
            items.Add(go);


            switch (item.Key)
            {
                case ItemType.Dirt:
                    // Dirt 아이템을 슬롯에 생성
                    // Instantiate 활용
                    sItem.itemSeetting(Dirt, "x" + item.Value.ToString(), item.Key);
                    break;

                case ItemType.Grass:
                    sItem.itemSeetting(Grass, "x" + item.Value.ToString(), item.Key);
                    break;

                case ItemType.Water:
                    sItem.itemSeetting(Water, "x" + item.Value.ToString(), item.Key);
                    break;

                case ItemType.Stone:
                    sItem.itemSeetting(Stone, "x" + item.Value.ToString(), item.Key);
                    break;
                case ItemType.IronOre:
                    sItem.itemSeetting(IronOre, "x" + item.Value, item.Key);
                    break;

                case ItemType.GoldOre:
                    sItem.itemSeetting(GoldOre, "x" + item.Value, item.Key);
                    break;

                case ItemType.DiamondOre:
                    sItem.itemSeetting(DiamondOre, "x" + item.Value, item.Key);
                    break;

                case ItemType.WoodPickaxe:
                    sItem.itemSeetting(WoodPickaxe, "x", item.Key);
                    break;

                case ItemType.StonePickaxe:
                    sItem.itemSeetting(StonePickaxe, "x", item.Key);
                    break;

                case ItemType.IronPickaxe:
                    sItem.itemSeetting(IronPickaxe, "x", item.Key);
                    break;

                case ItemType.GoldPickaxe:
                    sItem.itemSeetting(GoldPickaxe, "x", item.Key);
                    break;

                case ItemType.DiamondPickaxe:
                    sItem.itemSeetting(DiamondPickaxe, "x", item.Key);
                    break;

                case ItemType.weed:
                    sItem.itemSeetting(weedSprite, "x" + item.Value, item.Key);
                    break;

                case ItemType.SnoopDogg:
                    sItem.itemSeetting(SnoopDoggSprite, "x" + item.Value, item.Key);
                    break;
            }
            idx++;
        }
    }
}
