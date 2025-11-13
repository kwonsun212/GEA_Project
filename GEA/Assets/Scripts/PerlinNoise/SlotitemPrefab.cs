using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotitemPrefab : MonoBehaviour
{
    public Image itemImage;
    public TextMeshProUGUI itemText;

    public void itemSeetting(Sprite itemSprite, string txt)
    {
        itemImage.sprite = itemSprite;
        itemText.text = txt;
    }
}
