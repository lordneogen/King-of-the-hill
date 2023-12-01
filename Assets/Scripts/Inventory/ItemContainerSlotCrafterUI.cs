using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ItemContainerSlotCrafterUI : MonoBehaviour, IPointerClickHandler
{
    public GameObject ItemCeil;
    public GameObject CraftContainer;
    public GameObject CraftResultContainer;
    public Item Item;
    private ItemContainerSlot PlayerSlot;
    
    public void Start()
    {
        GameObject Result = Instantiate(ItemCeil);
        Slot ResultSlot = Result.GetComponent<Slot>();

        Player player = FindObjectOfType<Player>();
        PlayerSlot = player.Inventory;
        
        ResultSlot.Item = new ItemCount();
        ResultSlot.Item.Item = Item;
        ResultSlot.Item.Empty = false;
        ResultSlot.Item.Count = 1;
        ResultSlot.Item.Craft = true;
        
        ResultSlot.transform.SetParent(CraftResultContainer.transform);
        ResultSlot.transform.localScale=new Vector3(1f, 1f, 1f);
        foreach (var i in Item.Сraft)
        {
            GameObject Craft = Instantiate(ItemCeil);
            Slot CraftSlot = Craft.GetComponent<Slot>();
            CraftSlot.Item = i;
            Craft.transform.SetParent(CraftContainer.transform);
            Craft.transform.localScale=new Vector3(1f, 1f, 1f);
        }
    }

    public bool CheckCraft()
    {
        foreach (var i in Item.Сraft)
        {
            int prev= i.Count;
            if (!PlayerSlot.LootCheck(i))
            {
                Debug.Log(1);
                i.Count = prev;
                return false;
            }
            i.Count = prev;
        }
        return true;
    }

    public void Craft()
    {
        if (CheckCraft())
        {
            foreach (var i in Item.Сraft)
            {
                int prev= i.Count;
                Debug.Log(i.Count);
                PlayerSlot.LootPop(i);
                i.Count = prev;
            }
            ItemCount Craft = new ItemCount();
            Craft.Item = Item;
            Craft.Count = 1;
            Craft.Empty = false;
            PlayerSlot.LootPush(Craft);
        }
    }
    

    public void Update()
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Craft();
    }
}