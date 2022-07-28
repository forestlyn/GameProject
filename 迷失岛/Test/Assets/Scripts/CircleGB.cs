using UnityEngine;

public class CircleGB : BaseGB
{
    [SerializeField]
    protected BaseGB baseGB;

    public Sprite greenSprite;
    public Sprite rightSprite;
    public bool isRight;
    public override void Initialize()
    {
        baseGB.Initialize();
    }
}