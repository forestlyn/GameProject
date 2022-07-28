using UnityEngine;
using UnityEngine.UI;

public class PropToolTip : MonoBehaviour
{
    public Text propNameText;

    public void UpdatePropName(PropName propName)
    {
        propNameText.text = propName switch
        {
            PropName.Key => "ÐÅÏäÔ¿³×",
            PropName.Ticket => "´¬Æ±",
            _ => ""
        };
    }
}