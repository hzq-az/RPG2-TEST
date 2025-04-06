using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ItemToolTip : UI_ToolTip
{
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemTypeText;
    public TextMeshProUGUI itemDescription;

    public int defaultFontSize = 33;

    public void ShowToolTip(ItemData_Equipment item)
    {
        if (item == null) return;
        itemNameText.text = item.itemName;
        itemTypeText.text = item.equipmentType.ToString();
        itemDescription.text = item.GetDescription();

        AdjustPosition();

        if (itemNameText.text.Length > 12)
            itemNameText.fontSize *= 0.7f;
        else
            itemNameText.fontSize = defaultFontSize;

        gameObject.SetActive(true);
    }


    public void HideToolTip()
    {
        itemNameText.fontSize = defaultFontSize;
        gameObject.SetActive(false);
    }
}
