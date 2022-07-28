using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    private event Action testevent;

    private int x = 1;

    private void Start()
    {
        testevent += Log;
        testevent += Change;
    }

    public void Call()
    {
        x = 1;
        testevent.Invoke();
    }

    public void Change()
    {
        x = 2;
    }

    public void Log()
    {
        Debug.LogWarning(x);
    }
}