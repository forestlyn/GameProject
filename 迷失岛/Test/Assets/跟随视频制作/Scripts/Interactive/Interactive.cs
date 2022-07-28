using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class Interactive : MonoBehaviour
{
    public PropName requireProp;
    public bool isDone;

    public void CheckItem(PropName propName)
    {
        if (propName == requireProp && !isDone)
        {
            isDone = true;
            OnClickedAction();
            EventHandler.CallPropUsedEvent(propName);
        }
    }

    /// <summary>
    /// Ĭ����ȷ��Ʒִ��
    /// </summary>
    protected virtual void OnClickedAction()
    {
    }

    public virtual void OnEmptyClickedAction()
    {
        Debug.LogWarning("�յ�");
    }
}