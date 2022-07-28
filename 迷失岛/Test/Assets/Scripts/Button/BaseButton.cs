using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BaseButton : MonoBehaviour
{
    [SerializeField]
    public Button button;

    [SerializeField]
    public Text text;

    [SerializeField]
    public Image image;

    public UnityEvent OnClick = new UnityEvent();

    private void Awake()
    {
        button = GetComponent<Button>();
        text = GetComponentInChildren<Text>();
        image = GetComponent<Image>();
    }

    protected virtual void Start()
    {
        button.onClick.AddListener(OnClick.Invoke);
    }

    public void SetText(string buttonText)
    {
        if (text)
            text.text = buttonText;
        else
        {
            Debug.LogError(name + "无Text组件");
        }
    }

    public void SetSprite(Sprite buttonSprite)
    {
        if (image)
            image.sprite = buttonSprite;
        else
        {
            Debug.LogError(name + "无Image组件");
        }
    }
}