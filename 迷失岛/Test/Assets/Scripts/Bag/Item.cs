using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Bag/New Item")]
public class Item : ScriptableObject
{
    [SerializeField]
    public string itemName;

    [SerializeField]
    public Sprite itemSprite;

    [SerializeField]
    private int times;

    public bool Finshed => times == 0;

    public void Used()
    {
        times = times > 0 ? times - 1 : 0;
        Debug.LogWarningFormat("used item:{0} remain time:{1}", itemName, times);
    }
}