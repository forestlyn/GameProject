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

    private Vector2 hotspot = Vector2.zero; //�������Ͻ�
    private CursorMode mode = CursorMode.Auto; //�������ֹ�������ΪӲ��֧��ƽ̨�ϵĹ��������

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