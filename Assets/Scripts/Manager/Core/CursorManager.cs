using UnityEngine;
using UnityEngine.AddressableAssets;
using static Define;

public class CursorManager
{
    Texture2D basicCursor;
    Texture2D handCursor;

    CursorType cursorType = CursorType.None;

    public void Init()
    {
        basicCursor = Addressables.LoadAssetAsync<Texture2D>("BasicCursor").WaitForCompletion();
        handCursor = Addressables.LoadAssetAsync<Texture2D>("HandCursor").WaitForCompletion();

        SetBasicCursor();
    }

    public void SetBasicCursor()
    {
        if (cursorType != CursorType.Basic)
        {
            Cursor.SetCursor(basicCursor, new Vector2(basicCursor.width / 3, 0), CursorMode.Auto);
            cursorType = CursorType.Basic;
        }
    }

    public void SetHandCursor()
    {
        if (cursorType != CursorType.Hand)
        {
            Cursor.SetCursor(handCursor, new Vector2(handCursor.width / 3, 0), CursorMode.Auto);
            cursorType = CursorType.Hand;
        }
    }
}
