using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class ItemContainerSlot
{
    [Header("Массив предметов")] public List<ItemCount> Items;
    public List<GameObject> ItemsImage;
    public int ContainerSize;
    
    public GameObject CeilGameObject;
    public GameObject ContainerGameObject;

    public void off()
    {
        Image off = ContainerGameObject.GetComponent<Image>();
        off.enabled = false;
        ItemContainerDestoy();
        ContainerGameObject.SetActive(false);
    }

    public void on()
    {
        Image on = ContainerGameObject.GetComponent<Image>();
        on.enabled = true;
        ItemContainerRestart();
        ContainerGameObject.SetActive(true);
    }
    public void ItemContainerStart()
    {
        
        for (int i = 0; i < ContainerSize; i++)
        {
            ItemCount item = new ItemCount();
            item.Empty = true;
            Items.Add(item);
            //
            GameObject ItemImageObj = GameObject.Instantiate(CeilGameObject);
            ItemImageObj.transform.SetParent(ContainerGameObject.transform);
            ItemImageObj.transform.localScale = new Vector3(1, 1, 1);
            //
            ItemsImage.Add(ItemImageObj);
        }

        UpdateItems();
    }


    public void ItemContainerRestart()
    {
        for (int i = 0; i < ContainerSize; i++)
        {
            GameObject ItemImageObj = GameObject.Instantiate(CeilGameObject);
            ItemImageObj.transform.SetParent(ContainerGameObject.transform);
            ItemImageObj.transform.localScale = new Vector3(1, 1, 1);
            //
            Slot slot = ItemImageObj.GetComponent<Slot>();
            slot.Item = Items[i];
            ItemsImage.Add(ItemImageObj);
        }
    }
    public void ItemContainerDestoy()
    {
        foreach (var i in ItemsImage)
        {
            GameObject.Destroy(i);
        }
    }
    
    public bool LootPush(ItemCount ItemPushObj)
    {
        // ParentGO= Resources.Load<GameObject>("Prefabs/ItemCeil");
        for (int i = 0; i < Items.Count; i++)
        {
            if (ItemPushObj.Count <= 0)
            {
                break;
            }
            if (Items[i].Empty)
            {
                Items[i].Item = ItemPushObj.Item;
                Items[i].Count = ItemPushObj.Count;
                Items[i].Empty = false;
                ItemPushObj.Count = 0;
            }
            else if (Items[i].Item.Name == ItemPushObj.Item.Name)
            {
                ItemPushObj = Items[i].AddItem(ItemPushObj);
            }
        }
        UpdateItems();
        return ItemPushObj.Count > 0;
    }
    
    public bool LootPop(ItemCount ItemPopObj)
    {
        // ParentGO= Resources.Load<GameObject>("Prefabs/ItemCeil");
        for (int i = 0; i < Items.Count; i++)
        {
            if (ItemPopObj.Count <= 0)
            {
                break;
            }
            if (Items[i].Item.Name == ItemPopObj.Item.Name)
            {
                ItemPopObj = Items[i].RemoveItem(ItemPopObj);
            }
        }
        UpdateItems();
        return ItemPopObj.Count > 0;
    }

    public bool LootCheck(ItemCount ItemCheckObj)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (ItemCheckObj.Count <= 0)
            {
                break;
            }

            try
            {
                if (Items[i].Item.Name == ItemCheckObj.Item.Name)
                {
                    ItemCheckObj = Items[i].CheckItem(ItemCheckObj);
                }
            }
            catch
            {
                break;
            }
        }
        Debug.Log(ItemCheckObj.Count);
        // UpdateItems();
        return ItemCheckObj.Count <= 0;
    }
    
    public void UpdateItems()
    {
        for (int i = 0; i < ContainerSize; i++)
        {
            Slot slot= ItemsImage[i].GetComponent<Slot>();
            slot.Item = Items[i];
        }
    }

}