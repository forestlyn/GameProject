using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    public abstract void UpdateUI();
    public virtual void Open()
    {
        this.gameObject.SetActive(true);
    }
    public virtual void Close()
    {
        this.gameObject.SetActive(false);
    }
}
