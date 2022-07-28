using System.Collections.Generic;
using UnityEngine;

public class GBManager : MonoBehaviour
{
    [SerializeField]
    protected List<BaseGB> baseGBs;

    protected virtual void Start()
    {
        Initialize();
    }
    public virtual void Initialize()
    {
        foreach (BaseGB bg in baseGBs)
        {
            bg.Initialize();
        }
    }
}