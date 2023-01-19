using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackButton : MonoBehaviour
{
    [SerializeField] CriticType criticType;
    [SerializeField] FeedbackUI feedbackUI;

    public bool mouseOver;

    private void Update()
    {
        if (mouseOver && Input.GetMouseButtonDown(0))
        {

        }
    }

    private void OnMouseEnter()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }

    public CriticType M_CriticType { get { return criticType; } }
}
