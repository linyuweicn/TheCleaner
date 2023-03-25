using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipContent : MonoBehaviour
{
    [SerializeField] string message;

    private void OnMouseOver()
    {
        ToolTipBrainstorm.ToolTipInstance.ShowToolMessage(message);
    }

    private void OnMouseExit()
    {
        ToolTipBrainstorm.ToolTipInstance.HideToolMessage(message);

    }
}
