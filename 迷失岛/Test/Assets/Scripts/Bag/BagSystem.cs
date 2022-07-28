using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BagSystem
{
    [SerializeField]
    private ItemList itemList;
    public void AddItem(Item item)
    {
        itemList.AddItem(item);
        GameManager.Instance.UpdateUI();
    }

    public void RemoveItem(Item item)
    {
        itemList.RemoveItem(item);
        GameManager.Instance.UpdateUI();
    }

    public Item GetItemListIndex(int index)
    {
        return itemList.itemList[index];
    }
    public int GetItemListCount()
    {
        return itemList.itemList.Count;
    }
    public void SetItemList(ItemList res)
    {
        itemList = res;
        //GameManager.Instance.UpdateUI();
    }
}
