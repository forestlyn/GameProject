using UnityEngine;

public class Prop : MonoBehaviour
{
    public PropName propName;

    /// <summary>
    /// 点击物品后
    /// </summary>
    public void ClickedProp()
    {
        BagManager.Instance.AddProp(propName);
        gameObject.SetActive(false);
    }
}