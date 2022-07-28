using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private static CursorManager instance;

    [SerializeField]
    public Texture2D normal, holding;

    public static CursorManager Instance
    {
        get => instance;
    }

    private void Start()
    {
        if (instance)
            Destroy(this);
        else
            instance = this;
    }

    private Vector2 hotspot = Vector2.zero; //鼠标的左上角
    private CursorMode mode = CursorMode.Auto; //允许这种光标呈现作为硬件支持平台上的光标或者软件

    private void Update()
    {
        UpdateCursor();
    }

    public void UpdateCursor()
    {
        switch (GameManager.Instance.Mouse.State)
        {
            case MouseState.Default:
                Cursor.SetCursor(normal, hotspot, mode);
                break;
            case MouseState.Holding:
                Cursor.SetCursor(holding, hotspot, mode);
                break;
        }
    }
}