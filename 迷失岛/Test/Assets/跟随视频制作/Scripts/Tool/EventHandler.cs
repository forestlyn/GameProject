using System;

public static class EventHandler
{
    public static event Action<PropDetails, int> UpdateUIEvent;

    public static void CallUpdateUI(PropDetails prop, int index)
    {
        UpdateUIEvent?.Invoke(prop, index);
    }

    public static event Action BeforeSceneUnloadEvent;

    public static void CallBeforeSceneUnloadEvent()
    {
        BeforeSceneUnloadEvent?.Invoke();
    }

    public static event Action AfterSceneLoadedEvent;

    public static void CallAfterSceneLoadedEvent()
    {
        AfterSceneLoadedEvent?.Invoke();
    }

    public static event Action<PropDetails, bool> PropSelectedEvent;

    public static void CallPropSelectedEvent(PropDetails propDetails, bool isSelected)
    {
        PropSelectedEvent?.Invoke(propDetails, isSelected);
    }

    public static event Action<PropName> PropUsedEvent;

    public static void CallPropUsedEvent(PropName propName)
    {
        PropUsedEvent?.Invoke(propName);
    }

    public static event Action<int> ChangeItemEvent;

    public static void CallChangeItemEvent(int propName)
    {
        ChangeItemEvent?.Invoke(propName);
    }

    public static event Action<bool, string> ShowDialogueEvent;

    public static void CallShowDialogueEvent(bool open, string dialogue)
    {
        ShowDialogueEvent?.Invoke(open, dialogue);
    }

    public static event Action<string> GamePassedEvent;

    public static void CallGamePassedEvent(string gameName)
    {
        GamePassedEvent?.Invoke(gameName);
    }

    public static event Action<int> StartNewGameEvent;

    public static void CallStartNewGameEvent(int week)
    {
        StartNewGameEvent?.Invoke(week);
    }

    public static event Action<int> EndNewGameEvent;

    public static void CallEndNewGameEvent(int week)
    {
        EndNewGameEvent?.Invoke(week);
    }
}