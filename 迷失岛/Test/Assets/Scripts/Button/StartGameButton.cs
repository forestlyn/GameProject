using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : ChangeSceneButton
{
    protected override void Start()
    {
        base.Start();
        button.onClick.AddListener(ClickStartGameButton);
    }
    private void ClickStartGameButton()
    {
        if (GameManager.Instance)
            GameManager.Instance.SetCanvasActive(true);
    }
}
