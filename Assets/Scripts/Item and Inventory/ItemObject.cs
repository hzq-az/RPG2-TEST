using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public ItemData itemData;
    public Rigidbody2D rb;
   // public Vector2 velocity;
    /* private SpriteRenderer sr;

     private void Start()
     {
         sr = GetComponent<SpriteRenderer>();
         sr.sprite = itemData.icon;
     }
    private void OnValidate()
    {
        SetupVisuals();
    }*/

    private void SetupVisuals()
    {
        if (itemData == null)
            return;

        GetComponent<SpriteRenderer>().sprite = itemData.itemIcon;
        gameObject.name = "Item object -" + itemData.name;
    }

   
    public void SetUpItem(ItemData _itemData, Vector2 _velocity)
    {
        itemData = _itemData;
        rb.velocity = _velocity;
        SetupVisuals();
    }

    public void PickupItem()
    {
        if(!Inventory.instance.CanAddItem()&& itemData.itemType == ItemType.Equipment)
        {
            rb.velocity = new Vector2(0,10);
            PlayerManager.instance.player.fx.CreatePoPupUpText("No Space");
            return;
        }
        Inventory.instance.AddItem(itemData);
        Destroy(gameObject);
    }
}
