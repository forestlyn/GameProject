using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Teleport : MonoBehaviour
{
    public string sceneFrom;
    public string sceneTo;

    public void TeleportToScene()
    {
        TransitionManager.Instance.Transition(sceneFrom, sceneTo);
    }
}