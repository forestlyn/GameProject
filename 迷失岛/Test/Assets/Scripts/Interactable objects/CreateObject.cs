using UnityEngine;

public class CreateObject : InteractableObject
{
    [SerializeField]
    private GameObject gb;

    [SerializeField]
    private Vector3 pos;

    [SerializeField]
    private Transform t;

    public override void Clicked(Item item)
    {
        Quaternion q = new Quaternion();
        q.x = q.y = q.z = 0;
        Instantiate(gb, pos, q, t);
    }
}