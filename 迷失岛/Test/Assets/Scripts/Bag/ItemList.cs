using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemList", menuName = "Bag/New ItemList")]
public class ItemList : ScriptableObject
{
    [SerializeField]
    public List<Item> itemList;

    public void AddItem(Item item)
    {
        if (!itemList.Contains(item)) 
            itemList.Add(item);
    }

    public void RemoveItem(Item item)
    {
        if (itemList.Contains(item)) itemList.Remove(item);
    }
}