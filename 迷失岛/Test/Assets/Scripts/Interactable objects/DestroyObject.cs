using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : InteractableObject
{
    public override void Clicked(Item item)
    {
        gameObject.SetActive(false);
    }
}
