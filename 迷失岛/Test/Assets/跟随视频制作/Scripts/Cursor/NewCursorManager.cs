using UnityEngine;
using UnityEngine.EventSystems;

public class NewCursorManager : MonoBehaviour
{
    public RectTransform hand;
    private Vector3 mouseWorldPos => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

    private PropName currentProp;

    private bool holdProp;

    private void OnEnable()
    {
        EventHandler.PropSelectedEvent += OnPropSelectedEvent;
        EventHandler.PropUsedEvent += OnPropUsedEvent;
    }

    private void OnPropUsedEvent(PropName propName)
    {
        currentProp = PropName.None;
        holdProp = false;
        hand.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        EventHandler.PropSelectedEvent -= OnPropSelectedEvent;
        EventHandler.PropUsedEvent -= OnPropUsedEvent;
    }

    public void Update()
    {
        if (hand.gameObject.activeInHierarchy)
        {
            hand.position = Input.mousePosition;
        }
        if (InteractWithUI()) 
            return;
        if (ObjectAtMousePostion() && Input.GetMouseButtonDown(0))
        {
            ClickAction(ObjectAtMousePostion().gameObject);
        }
    }

    public void ClickAction(GameObject clickedObject)
    {
        switch (clickedObject.tag)
        {
            case "Teleport":
                Teleport teleport = clickedObject.GetComponent<Teleport>();
                teleport?.TeleportToScene();
                break;

            case "Prop":
                Prop prop = clickedObject.GetComponent<Prop>();
                prop?.ClickedProp();
                break;

            case "Interactive":
                Interactive interactive = clickedObject.GetComponent<Interactive>();
                if (holdProp)
                    interactive?.CheckItem(currentProp);
                else
                    interactive?.OnEmptyClickedAction();
                break;
        }
    }

    /// <summary>
    /// 检测鼠标点击范围内的碰撞体
    /// </summary>
    /// <returns></returns>
    private Collider2D ObjectAtMousePostion()
    {
        return Physics2D.OverlapPoint(mouseWorldPos);
    }

    private bool InteractWithUI()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        return false;
    }

    private void OnPropSelectedEvent(PropDetails propDetails, bool isSelected)
    {
        holdProp = isSelected;
        hand.gameObject.SetActive(holdProp);
        if (isSelected)
        {
            currentProp = propDetails.propName;
            hand.gameObject.SetActive(true);
        }
    }
}