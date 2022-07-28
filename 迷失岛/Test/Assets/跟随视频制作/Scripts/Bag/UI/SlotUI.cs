using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
    public PropToolTip toolTip;
    private PropDetails currentProp;
    private bool isSelected;

    public void SetProp(PropDetails prop)
    {
        //Debug.LogWarning("setprop");
        currentProp = prop;
        this.gameObject.SetActive(true);
        SetSprite(prop.propSprite);
    }

    private void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
        image.SetNativeSize();
    }

    public void SetEmpty()
    {
        this.gameObject.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTip.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.activeInHierarchy)
        {
            toolTip.gameObject.SetActive(true);
            toolTip.UpdatePropName(currentProp.propName);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isSelected = !isSelected;
        EventHandler.CallPropSelectedEvent(currentProp, isSelected);
    }
}