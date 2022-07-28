using UnityEngine;

public class BaseGB : MonoBehaviour
{
    [SerializeField]
    protected SpriteRenderer sr;

    public Sprite originSprite;

    protected virtual void Awake()
    {
        sr = this.GetComponent<SpriteRenderer>();
    }

    public virtual void Initialize()
    {
        SetSprite(originSprite);
    }

    public void SetSprite(Sprite res)
    {
        if (res != null)
            sr.sprite = res;
    }
}