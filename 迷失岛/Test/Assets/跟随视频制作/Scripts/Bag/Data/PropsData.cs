using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PropsData", menuName = "Bag/PropsData")]
public class PropsData : ScriptableObject
{
    public List<PropDetails> propDetailsList;

    public PropDetails GetPropDetails(PropName propName)
    {
        return propDetailsList.Find(i => i.propName == propName);
    }
}

[System.Serializable]
public class PropDetails
{
    public PropName propName;
    public Sprite propSprite;
}