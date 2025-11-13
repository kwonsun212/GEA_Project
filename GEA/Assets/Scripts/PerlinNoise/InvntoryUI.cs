using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class InvntoryUI : MonoBehaviour
{
    public Sprite Dirt;
    public Sprite Water;
    public Sprite Stone;
    public Sprite Grass;

    public List<Transform> Slot;
    public GameObject SlotItem;
    List<GameObject> items = new List<GameObject>();
    private void Update()
    {

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
                    sItem.itemSeetting(Dirt, item.Value.ToString());
                    break;

                case BlockType.Grass:
                    sItem.itemSeetting(Grass, item.Value.ToString());
                    break;

                case BlockType.Water:
                    sItem.itemSeetting(Water, item.Value.ToString());
                    break;

                case BlockType.Stone:
                    sItem.itemSeetting(Stone, item.Value.ToString());
                    break;
            }
            idx++;
        }
    }
}
