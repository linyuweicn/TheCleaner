using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChangeSpriteColor : MonoBehaviour
{
    public Renderer rend;
    public float FadingTime;
    public Color32 color;
 

    void Start()
    {
        rend = GetComponent<Renderer>();
        FadingTime = 0.2f;


    }

    // The mesh goes red when the mouse is over it...
    void OnMouseEnter()
    {
        rend.material.color = color;
    }

    // ...the red fades out to cyan as the mouse is held over...
    void OnMouseOver()
    {
        rend.material.color -= new Color(FadingTime, 0, 0) * Time.deltaTime;
    }

    // ...and the mesh finally turns white when the mouse moves away.
    void OnMouseExit()
    {
        rend.material.color = Color.white;
    }

 
}
