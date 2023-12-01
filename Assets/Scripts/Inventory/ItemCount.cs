using System;
[System.Serializable]
public class ItemCount
{
    public Item Item;
    public int Count;
    public bool Empty;
    public bool Craft=false;
    
    public ItemCount AddItem(ItemCount AddItemObj)
    {
        int CountAdd = AddItemObj.Count;
        
        if (Empty)
        {
            Count = AddItemObj.Count;
            Item = AddItemObj.Item;
            Empty = false;
            AddItemObj.Count = 0;
            return AddItemObj;
        }
        if (Count<Item.MaxCount)
        {
            Count+=CountAdd;
            if (Count > Item.MaxCount)
            {
                AddItemObj.Count = Count - Item.MaxCount;
                Count = Item.MaxCount;
            }
            else
            {
                AddItemObj.Count = 0;
            }
        }
        return AddItemObj;
    }
    
    public ItemCount RemoveItem(ItemCount RemoveItemObj)
    {
        if (RemoveItemObj.Count > 0)
        {
            Count -= RemoveItemObj.Count;
            if (Count >= 0)
            {
                RemoveItemObj.Count = 0;
            }
            else
            {
                RemoveItemObj.Count = 0 - Count;
            }
            Count =Math.Max(0,Count);
        }
        if (Count <= 0)
        {
            Item = null;
            Empty = true;
        }
        return RemoveItemObj;
    }
    
    public ItemCount CheckItem(ItemCount CheckItemObj)
    {
        CheckItemObj.Count -= Count;
        CheckItemObj.Count = Math.Max(CheckItemObj.Count, 0);
        return CheckItemObj;
    }

    public void OneCreate(Item item)
    {
        Item = item;
        Count = 1;
        Craft = false;
        Empty = false;
    }
}