using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private Dictionary<PropName, bool> propAvailableDic = new Dictionary<PropName, bool>();
    private Dictionary<string, bool> interactiveStateDic = new Dictionary<string, bool>();

    private void OnEnable()
    {
        EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        EventHandler.UpdateUIEvent += OnUpdateUIEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
        EventHandler.EndNewGameEvent += OnEndNewGameEvent;
    }

    private void OnDisable()
    {
        EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        EventHandler.UpdateUIEvent -= OnUpdateUIEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
        EventHandler.EndNewGameEvent -= OnEndNewGameEvent;
    }

    private void OnStartNewGameEvent(int week)
    {
        //propAvailableDic.Clear();
        //interactiveStateDic.Clear();
        SaveManager.Instance.Load(out propAvailableDic, "propAvailableDic" + week);
        SaveManager.Instance.Load(out interactiveStateDic, "interactiveStateDic" + week);
        //if (propAvailableDic.ContainsKey(PropName.Key))
        //    Debug.LogWarning("propc key" + propAvailableDic[PropName.Key]);
        //Debug.LogWarning(propAvailableDic.Count);
    }

    private void OnEndNewGameEvent(int week)
    {
        //Debug.LogWarning(propAvailableDic.Count);

        SaveManager.Instance.Save(propAvailableDic, "propAvailableDic" + week);
        SaveManager.Instance.Save(interactiveStateDic, "interactiveStateDic" + week);
    }

    private void OnBeforeSceneUnloadEvent()
    {
        foreach (Prop prop in FindObjectsOfType<Prop>())
        {
            if (!propAvailableDic.ContainsKey(prop.propName))
            {
                propAvailableDic.Add(prop.propName, true);
            }
            else
            {
                prop.gameObject.SetActive(propAvailableDic[prop.propName]);
            }
        }

        foreach (Interactive interactive in FindObjectsOfType<Interactive>())
        {
            if (!interactiveStateDic.ContainsKey(interactive.name))
            {
                interactiveStateDic.Add(interactive.name, interactive.isDone);
            }
            else
            {
                interactiveStateDic[interactive.name] = interactive.isDone;
            }
        }
    }

    private void OnAfterSceneLoadedEvent()
    {
        foreach (Prop prop in FindObjectsOfType<Prop>())
        {
            if (!propAvailableDic.ContainsKey(prop.propName))
            {
                propAvailableDic.Add(prop.propName, true);
            }
            else
            {
                prop.gameObject.SetActive(propAvailableDic[prop.propName]);
            }
        }
        foreach (Interactive interactive in FindObjectsOfType<Interactive>())
        {
            if (!interactiveStateDic.ContainsKey(interactive.name))
            {
                interactiveStateDic.Add(interactive.name, interactive.isDone);
            }
            else
            {
                interactive.isDone = interactiveStateDic[interactive.name];
            }
        }
    }

    private void OnUpdateUIEvent(PropDetails prop, int index)
    {
        if (prop != null)
        {
            propAvailableDic[prop.propName] = false;
        }
    }
}