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

    public List<Transform> Slot;
    public GameObject SlotItem;
    List<GameObject> items = new List<GameObject>();

    public int selectedIndex = -1;

    private void UpdateInventory()
    {
        for(int i = 0; i < Mathf.Min(9, Slot.Count); i++)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1 + 1))
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
                SetSelectedIndex(idx);
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
    public BlockType GetInventorySlot()
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
            var go = Instantiate(SlotItem, Slot[idx].transform);
            go.transform.localPosition = Vector3.zero;
            SlotitemPrefab sItem = go.GetComponent<SlotitemPrefab>();
            items.Add(go);


            switch(item.Key)
            {
                case BlockType.Dirt:
                    // Dirt 아이템을 슬롯에 생성
                    // Instantiate 활용
                    sItem.itemSeetting(Dirt, "x" + item.Value.ToString(), item.Key);
                    break;

                case BlockType.Grass:
                    sItem.itemSeetting(Grass, "x" + item.Value.ToString(), item.Key);
                    break;

                case BlockType.Water:
                    sItem.itemSeetting(Water, "x" + item.Value.ToString(), item.Key);
                    break;

                case BlockType.Stone:
                    sItem.itemSeetting(Stone, "x" + item.Value.ToString(), item.Key);
                    break;
            }
            idx++;
        }
    }
}
