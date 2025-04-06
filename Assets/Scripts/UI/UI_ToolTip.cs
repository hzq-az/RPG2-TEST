using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ToolTip : MonoBehaviour
{
    [SerializeField] private float xLimit = 960;
    [SerializeField] private float yLimit = 540;
    [SerializeField] private float xOffset = 150;
    [SerializeField] private float yOffset = 100;
    public virtual void AdjustPosition()
    {
        Vector2 mousePosition = Input.mousePosition;
        //Debug.Log(mousePosition);
        float newxOffset = 0;
        float newyOffset = 0;
        if (mousePosition.x > xLimit) newxOffset = -xOffset;
        else newxOffset = xOffset;

        if (mousePosition.y > yLimit) newyOffset = -yOffset;
        else newyOffset = yOffset;

        transform.position = new Vector2(mousePosition.x + newxOffset, mousePosition.y + newyOffset);
    }
   
}
