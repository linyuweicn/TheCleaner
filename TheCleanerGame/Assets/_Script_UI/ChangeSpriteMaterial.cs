using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ChangeSpriteMaterial : MonoBehaviour
{
    public Renderer rend;
    private Material originalMat;
    public Material material;
    private bool isShown;
    AudioManager audioManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalMat = GetComponent<Renderer>().material;
        audioManager = FindObjectOfType<AudioManager>();
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

        rend.material = material;
        //audioManager.PlayUiSound("ui_highlight");

    }


    public void OnMouseExit()
    {
        //rend.material.color = Color.white;
        rend.material = originalMat;
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

        rend.material = material;
        yield return new WaitForSeconds(2f);
        rend.material = originalMat;
    }

    public void DisableChange()
    {
        rend.material = originalMat;
    }
}
