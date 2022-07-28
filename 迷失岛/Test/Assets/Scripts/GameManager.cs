using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    public MouseScript Mouse;

    [SerializeField]
    public BagSystem BagSystem;

    [SerializeField]
    public BagDisplayUI BagUI;

    [SerializeField]
    public ItemList itemList;

    private static GameManager instance;

    public static GameManager Instance
    {
        get => instance;
    }

    private void Awake()
    {
        BagSystem = new BagSystem();
        BagSystem.SetItemList(itemList);

        Mouse = new MouseScript();

        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void UsedItem(Item item)
    {
        item.Used();
        if (item.Finshed)
        {
            BagSystem.RemoveItem(item);
            UpdateUI();
        }
    }

    public void SetCanvasActive(bool b)
    {
        canvas.SetActive(b);
    }

    public void UpdateUI()
    {
        BagUI.UpdateUI();
    }
}