using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class SlotitemPrefab : MonoBehaviour,IPointerClickHandler
{
    public Image itemImage;
    public TextMeshProUGUI itemText;
    public ItemType blockType;
    public CraftingPanel craftingPanel;

    public void itemSeetting(Sprite itemSprite, string txt, ItemType type)
    {
        itemImage.sprite = itemSprite;
        itemText.text = txt;
        blockType = type;
    }

    void Awake()
    {
        if (!craftingPanel)
            craftingPanel = FindObjectOfType<CraftingPanel>(true);
    }

    public void OnPointerClick(PointerEventData eventdata)
    {
        if (eventdata.button != PointerEventData.InputButton.Right) return;
        if (!craftingPanel) return;

        craftingPanel.AddPlanned(blockType, 1); 
    }
}
