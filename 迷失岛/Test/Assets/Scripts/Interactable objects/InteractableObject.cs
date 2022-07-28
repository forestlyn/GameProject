using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class InteractableObject : ClickDownScript
{
    [SerializeField]
    private List<Item> items;

    public override void OnPointerDown(PointerEventData eventData)
    {
        Debug.LogWarning("clicked" + name);
        ChangeState();
    }

    public virtual void ChangeState()
    {
        if (Check(GameManager.Instance.Mouse.HoldingItem))
        {
            Debug.LogWarning("exsit item" + GameManager.Instance.Mouse.HoldingItem?.itemName);
            Clicked(GameManager.Instance.Mouse.HoldingItem);
            GameManager.Instance.UsedItem(GameManager.Instance.Mouse.HoldingItem);
        }
    }
    protected bool Check(Item item)
    {
        return items.Exists(x => x == item);
    }

    public abstract void Clicked(Item item);
}