using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public BallDetails ballDetails;
    public bool isMatch;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetUpBall(BallDetails ball)
    {
        ballDetails = ball;
        if (isMatch)
            SetRight();
        else
            SetWrong();
    }

    public void SetRight()
    {
        spriteRenderer.sprite = ballDetails.rightSprite;
    }
    public void SetWrong()
    {
        spriteRenderer.sprite = ballDetails.wrongSprite;
    }

}
