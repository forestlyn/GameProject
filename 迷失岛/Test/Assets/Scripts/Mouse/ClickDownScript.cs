using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
public class ClickDownScript : MonoBehaviour, IPointerDownHandler
{
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        GameManager.Instance.Mouse.ChangeState(tag);
    }
}