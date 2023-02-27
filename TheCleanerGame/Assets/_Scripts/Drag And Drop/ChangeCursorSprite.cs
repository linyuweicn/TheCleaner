using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeCursorSprite : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    
   /* void OnMouseOver()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }*/

     public void OnPointerEnter(PointerEventData pointerEventData)
    {
       
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
       
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
