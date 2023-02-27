using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ChangeSpriteMaterial : MonoBehaviour
{
    //public Renderer rend;
    private SpriteRenderer spriteRenderer;
    private Material originalMat;
    public Material material;
    private bool isShown;
    AudioManager audioManager;

    
    private Sprite OriginalSprite;
    public Sprite HoverSprite;
    public bool ChangeSpriteWhenHover;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMat = GetComponent<SpriteRenderer>().material;
        audioManager = FindObjectOfType<AudioManager>();
        OriginalSprite = GetComponent<SpriteRenderer>().sprite;
    }

    // The mesh goes red when the mouse is over it...
    void OnMouseEnter()
    {

        if (SceneTransitionButton.gameIsPaused)
        {
            //Debug.Log("game is paused and prevent changing color");
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return; // when the game is paused, prevent changing spite outline.
            }
        }

        spriteRenderer.material = material;
        //audioManager.PlayUiSound("ui_highlight");

        if (ChangeSpriteWhenHover && HoverSprite != null)
        {
            spriteRenderer.sprite = HoverSprite;
        }

    }


    public void OnMouseExit()
    {
        //rend.material.color = Color.white;
        spriteRenderer.material = originalMat;
        if (ChangeSpriteWhenHover && HoverSprite != null)
        {
            spriteRenderer.sprite = OriginalSprite;
        }

    }

    public void ChangMaterialForSeconds()
    {
        if (!isShown)
        {
            StartCoroutine(ChangeMaterial());
            isShown = true;
        }
        
    }

    IEnumerator ChangeMaterial()
    {

        spriteRenderer.material = material;
        yield return new WaitForSeconds(2f);
        spriteRenderer.material = originalMat;
    }

    public void DisableChange()
    {
        spriteRenderer.material = originalMat;
    }
}
