using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    public Texture2D cursorTexture;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    public void CursorCrosshair()
    {
        hotSpot = new Vector2(cursorTexture.width / 2, cursorTexture.height/2);
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
}
