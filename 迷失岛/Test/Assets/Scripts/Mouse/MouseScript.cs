using UnityEngine;

[System.Serializable]
public class MouseScript
{
    [SerializeField]
    private MouseState state;

    [SerializeField]
    private Item holdingItem;

    public MouseState State
    {
        get => state;
    }

    public Item HoldingItem
    {
        get => holdingItem;
    }

    public void SetItem(Item item)
    {
        holdingItem = item;
    }

    public void ChangeHoldingItem()
    {
        holdingItem = state switch
        {
            MouseState.Default => null,
            MouseState.Holding => GameManager.Instance.BagUI.GetItem(),
            _ => null,
        };
        Debug.LogWarning("now holding" + holdingItem?.itemName);
    }

    public void ChangeState(string tag)
    {
        state = tag switch
        {
            "props" => MouseState.Holding,
            _ => MouseState.Default
        };
        ChangeHoldingItem();
        Debug.LogWarning("now state" + state + tag /*+ holdingItem.itemName*/);
    }
}