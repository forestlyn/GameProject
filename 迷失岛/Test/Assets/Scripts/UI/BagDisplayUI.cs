using UnityEngine;

public class BagDisplayUI : BaseUI
{
    [SerializeField]
    private BaseButton leftButton;

    [SerializeField]
    private BaseButton rightButton;

    [SerializeField]
    private BaseButton itemButton;

    [SerializeField]
    private int itemIndex;

    private void Start()
    {
        itemIndex = 0;
        leftButton.OnClick.AddListener(ClickLeftButton);
        rightButton.OnClick.AddListener(ClickRightButton);
        UpdateUI();
    }

    public override void UpdateUI()
    {
        if (CheckIndex(itemIndex))
        {
            SetItem(GetItem());
            //Debug.LogWarning("show");
        }
        else
        {
            itemIndex = 0;
            if (CheckIndex(0))
            {
                SetItem(GetItem());
            }
            else
                ClearItem();
        }
        leftButton.button.interactable = CheckIndex(itemIndex - 1);
        rightButton.button.interactable = CheckIndex(itemIndex + 1);
    }

    private void SetItem(Item item)
    {
        if (item)
        {
            itemButton.gameObject.SetActive(true);
            itemButton.SetSprite(item.itemSprite);
            itemButton.SetText(item.itemName);
        }
    }

    private void ClearItem()
    {
        itemButton.gameObject.SetActive(false);
    }

    private void ClickLeftButton()
    {
        itemIndex = CheckIndex(itemIndex - 1) ? itemIndex - 1 : itemIndex;
        UpdateUI();
    }

    private void ClickRightButton()
    {
        itemIndex = CheckIndex(itemIndex + 1) ? itemIndex + 1 : itemIndex;
        UpdateUI();
    }

    private bool CheckIndex(int index)
    {
        return (index >= 0 &&
            index < GameManager.Instance.BagSystem.GetItemListCount());
    }

    public Item GetItem()
    {
        if (CheckIndex(itemIndex))
        {
            return GameManager.Instance.BagSystem.GetItemListIndex(itemIndex);
        }
        return null;
    }
}