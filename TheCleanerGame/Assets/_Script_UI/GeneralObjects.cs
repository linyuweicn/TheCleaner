using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralObjects : MonoBehaviour
{
    [SerializeField] GameObject enableObjects;
    [SerializeField] BoxCollider2D [] boxCollider2D;

    private bool isCalaendarClicked;
    void Start()
    {
        enableObjects.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CloseObject()
    {
        enableObjects.SetActive(false);
        isCalaendarClicked = false;

        for (int i = 0; i < boxCollider2D.Length; i++)
        {
            boxCollider2D[i].enabled = true;
        }
    }

    private void OnMouseDown()
    {
     
        enableObjects.SetActive(true);

        for (int i = 0; i< boxCollider2D.Length; i++)
        {
            boxCollider2D[i].enabled = false;
        }

    }
}
