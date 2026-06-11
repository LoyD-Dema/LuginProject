using UnityEngine;

public class SetCursor : MonoBehaviour
{
    [SerializeField] Texture2D cursorTexture;
    [Range(0.0f, 1.0f)]
    [SerializeField] float xOffset;
    [Range(0.0f, 1.0f)]
    [SerializeField] float yOffset;

    private void Awake()
    {

        Vector2 offset = new Vector2(cursorTexture.width * xOffset, cursorTexture.height * yOffset); 
        Cursor.SetCursor(cursorTexture, offset, CursorMode.Auto);
    }
}
