using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void QuitGame()
    {
        EventHandler.CallEndNewGameEvent(NewGameManager.Instance.gameWeek);
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void ContinueGame()
    {
        TransitionManager.Instance.Transition("Menu", "NewH1");
    }

    public void GoBackToMenu()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        TransitionManager.Instance.Transition(currentSceneName, "Menu");
    }

    public void StartGameWeek(int week)
    {
        EventHandler.CallStartNewGameEvent(week);
    }
}