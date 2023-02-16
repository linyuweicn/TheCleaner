using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneControl : MonoBehaviour
{
    public GameObject PhoneParent;
    void Start()
    {
        //GameObject.Find("TimeParent").GetComponent<CanvasGroup>().alpha = 0;
        PhoneParent.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*    public void Hover()
        {
            GameObject.Find("TimeParent").GetComponent<CanvasGroup>().alpha = 1;
        }

        public void StopHover()
        {
            GameObject.Find("TimeParent").GetComponent<CanvasGroup>().alpha = 0;
        }*/

    private void OnMouseDown()
    {
        PhoneParent.SetActive(false);
        Debug.Log("closePhone");
    }
}
