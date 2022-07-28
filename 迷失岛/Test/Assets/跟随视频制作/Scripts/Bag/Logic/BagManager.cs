using System.Collections.Generic;
using UnityEngine;

public class BagManager : MySingleton<BagManager>
{
    [SerializeField]
    private PropsData propsData;

    [SerializeField]
    private List<PropName> props;

    private void OnEnable()
    {
        EventHandler.PropUsedEvent += OnPropUsedEvent;
        EventHandler.ChangeItemEvent += OnChangeItemEvent;
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
        EventHandler.EndNewGameEvent += OnEndNewGameEvent;
    }

    private void OnDisable()
    {
        EventHandler.PropUsedEvent -= OnPropUsedEvent;
        EventHandler.ChangeItemEvent -= OnChangeItemEvent;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
        EventHandler.EndNewGameEvent -= OnEndNewGameEvent;
    }

    private void OnEndNewGameEvent(int week)
    {
        SaveManager.Instance.Save(props, "props" + week);
    }

    public void AddProp(PropName propName)
    {
        if (!props.Contains(propName))
        {
            props.Add(propName);
            EventHandler.CallUpdateUI(propsData.GetPropDetails(propName), props.Count - 1);
        }
    }

    private void OnStartNewGameEvent(int week)
    {
        props.Clear();
        SaveManager.Instance.Load(out props, "props" + week);
    }

    public void OnPropUsedEvent(PropName propName)
    {
        if (props.Contains(propName))
        {
            props.Remove(propName);
            if (props.Count > 0)
            {
                EventHandler.CallUpdateUI(propsData.GetPropDetails(props[props.Count - 1]), props.Count - 1);
            }
            else
            {
                EventHandler.CallUpdateUI(null, -1);
            }
        }
    }

    public void OnChangeItemEvent(int index)
    {
        //Debug.LogWarning("OnChangeItemEvent"+index);
        if (props.Count > index && index >= 0)
        {
            EventHandler.CallUpdateUI(propsData.GetPropDetails(props[index]), index);
        }
    }

    private void OnAfterSceneLoadedEvent()
    {
        if (props.Count == 0)
        {
            EventHandler.CallUpdateUI(null, -1);
        }
        else
        {
            for (int i = 0; i < props.Count; i++)
            {
                EventHandler.CallUpdateUI(propsData.GetPropDetails(props[i]), i);
            }
        }
    }

    private int GetPropIndex(PropName propName)
    {
        for (int i = 0; i < props.Count; i++)
        {
            if (props[i] == propName)
            {
                return i;
            }
        }
        return -1;
    }
}