using UnityEngine;

public class CursorSettings : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    private Vector2 cursorHotspot;

    private void Start()
    {
        cursorHotspot = new Vector2(cursorTexture.width*.5f, cursorTexture.height*.5f);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }
}
