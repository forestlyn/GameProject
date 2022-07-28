using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : BaseButton
{
    [SerializeField]
    public string sceneName;

    protected override void Start()
    {
        button.onClick.AddListener(ChangeScene);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}