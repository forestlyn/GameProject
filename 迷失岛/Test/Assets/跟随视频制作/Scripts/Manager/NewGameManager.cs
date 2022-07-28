using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class NewGameManager : MySingleton<NewGameManager>
{
    private Dictionary<string, bool> miniGameStateDic = new Dictionary<string, bool>();
    public int gameWeek;
    private GameController currentGame;

    private void Start()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
    }

    private void OnEnable()
    {
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        EventHandler.GamePassedEvent += OnGamePassedEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
        EventHandler.EndNewGameEvent += OnEndNewGameEvent;
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        EventHandler.GamePassedEvent -= OnGamePassedEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
        EventHandler.EndNewGameEvent += OnEndNewGameEvent;
    }

    private void OnEndNewGameEvent(int week)
    {
        SaveManager.Instance.Save(miniGameStateDic, "miniGameStateDic" + week);
    }

    private void OnStartNewGameEvent(int week)
    {
        this.gameWeek = week;
        SaveManager.Instance.Load(out miniGameStateDic, "miniGameStateDic" + week);
    }

    private void OnAfterSceneLoadedEvent()
    {
        foreach (MiniGame miniGame in FindObjectsOfType<MiniGame>())
        {
            if (miniGameStateDic.TryGetValue(miniGame.gameName, out bool isPassed))
            {
                miniGame.isPassed = isPassed;
                miniGame.UpdateMiniGameState();
            }
        }
        currentGame = FindObjectOfType<GameController>();
        currentGame?.SetGameWeekData(gameWeek);
    }

    private void OnGamePassedEvent(string gameName)
    {
        miniGameStateDic[gameName] = true;
    }
}