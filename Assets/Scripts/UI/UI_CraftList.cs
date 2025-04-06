using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CraftList : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] private Transform craftSlotParent;
    [SerializeField] private GameObject craftSlotPrefab;

    [SerializeField]private List<ItemData_Equipment> craftEquipment;
   // [SerializeField] private List<UI_CraftSlot> craftSlots;单一列表时所用

    private void Start()
    {
        //AssingCraftSlots();
        transform.parent.GetChild(0).GetComponent<UI_CraftList>().SetupCraftList();
        SetupDefaultCrafWindow();
    }

    //private void AssingCraftSlots()
    //{
    //    for (int i = 0; i < craftSlotParent.childCount; i++)
    //    {
    //        craftSlots.Add(craftSlotParent.GetChild(i).GetComponent<UI_CraftSlot>());
    //    }
    //}
    public void SetupCraftList()
    {
        //for(int i = 0; i < craftSlots.Count; i++)
        //{
        //    Destroy(craftSlots[i].gameObject);
        //}
        for(int i = 0; i < craftSlotParent.childCount; i++)
        {
            Destroy(craftSlotParent.GetChild(i).gameObject);
        }
       // craftSlots = new List<UI_CraftSlot>();

        for(int i = 0;i<craftEquipment.Count; i++)
        {
            GameObject newSlot = Instantiate(craftSlotPrefab, craftSlotParent);
            newSlot.GetComponent<UI_CraftSlot>().SetupCraftSlot(craftEquipment[i]);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SetupCraftList();
    }
    public void SetupDefaultCrafWindow()
    {
        if (craftEquipment[0] != null)
            GetComponentInParent<UI>().craftWindow.SetupCraftWindow(craftEquipment[0]);
    }
}
