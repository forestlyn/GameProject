using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiniGame : MonoBehaviour
{
    public UnityEvent OnGameFinshed;
     public string gameName;

    public bool isPassed;

    public void UpdateMiniGameState()
    {
        if (isPassed)
        {
            OnGameFinshed?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
