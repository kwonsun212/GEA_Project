using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotitemPrefab : MonoBehaviour
{
    public Image itemImage;
    public TextMeshProUGUI itemText;
    public BlockType blockType;

    public void itemSeetting(Sprite itemSprite, string txt, BlockType type)
    {
        itemImage.sprite = itemSprite;
        itemText.text = txt;
        blockType = type;
    }
}
