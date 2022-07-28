using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PropsScript : MonoBehaviour
{
    [SerializeField]
    public Item item;

    public void OnMouseDown()
    {
        GameManager.Instance.BagSystem.AddItem(item);
        Destroy(this.gameObject);
    }
}
