using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : ChangeSceneButton
{
    protected override void Start()
    {
        base.Start();
        button.onClick.AddListener(ClickMenuButton);
    }
    public void ClickMenuButton()
    {
        //Debug.LogError(11);
        //this.gameObject.SetActive(false);
        GameManager.Instance.SetCanvasActive(false);
    }
}
