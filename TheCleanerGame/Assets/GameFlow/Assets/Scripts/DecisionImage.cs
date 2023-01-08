using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecisionImage : MonoBehaviour
{
    public Image MyImage { get; set; }
    Animator animator;

    private void Awake()
    {
        MyImage = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }
    
    public void Hide()
    {
        MyImage.enabled = false;
        MyImage.color = new Color(MyImage.color.r, MyImage.color.g, MyImage.color.b, 0);
    }


    public void ShowNoAnimation(Sprite sprite)
    {
        MyImage.enabled = true;
        MyImage.sprite = sprite;
        animator.SetTrigger("Stay");
    }
    public void ShowAnimation(Sprite sprite)
    {
        MyImage.enabled = true;
        MyImage.sprite = sprite;
        animator.SetTrigger("FadeIn");
    }


}
