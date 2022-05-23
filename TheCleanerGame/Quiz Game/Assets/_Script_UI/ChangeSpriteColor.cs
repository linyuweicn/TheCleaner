using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChangeSpriteColor : MonoBehaviour
{
    [SerializeField] Color colorTo;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HoverChangeColor()
    {
        gameObject.transform.GetComponent<MeshRenderer>().material.DOColor(colorTo, 0.1f);
    }
}
