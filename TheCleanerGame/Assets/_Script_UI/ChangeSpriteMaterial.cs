using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        //rend.material.color = Color.red;
        rend.material = material;
        audioManager.PlayUiSound("ui_highlight");
    }

    // ...the red fades out to cyan as the mouse is held over...
    void OnMouseOver()
    {
        //rend.material.color -= new Color(0.1F, 0, 0) * Time.deltaTime;
    }

    // ...and the mesh finally turns white when the mouse moves away.
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
