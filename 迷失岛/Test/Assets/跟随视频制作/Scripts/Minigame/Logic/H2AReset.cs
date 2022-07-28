using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class H2AReset : Interactive
{
    private Transform gear;

    private void Awake()
    {
        gear = transform.GetChild(0);
    }

    public override void OnEmptyClickedAction()
    {
        gear.DOPunchRotation(Vector3.forward * 180, 1, 1, 0);
        GameController.Instance.ResetGame();
    }
}
