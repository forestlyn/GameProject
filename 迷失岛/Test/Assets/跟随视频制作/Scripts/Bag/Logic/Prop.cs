using UnityEngine;

public class Prop : MonoBehaviour
{
    public PropName propName;

    /// <summary>
    /// �����Ʒ��
    /// </summary>
    public void ClickedProp()
    {
        BagManager.Instance.AddProp(propName);
        gameObject.SetActive(false);
    }
}