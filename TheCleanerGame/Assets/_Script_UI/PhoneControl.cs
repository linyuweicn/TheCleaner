using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneControl : MonoBehaviour
{
    //public GameObject PhoneParent;
    private bool hasOpened;
    private bool hasClickedTab;
    private AudioManager audioManager;
    void Start()
    {
        //GameObject.Find("TimeParent").GetComponent<CanvasGroup>().alpha = 0;
        transform.parent.gameObject.SetActive(false);
        audioManager = FindObjectOfType<AudioManager>();
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
        transform.parent.gameObject.SetActive(false);
        Debug.Log("closePhone");
    }

    public void EnablePhone()
    {
        if (!hasOpened && hasClickedTab)
        {
            transform.parent.gameObject.SetActive(true);
            hasOpened = true;
            audioManager.PlayUiSound("phone vibrate");
        }
    }

    public void ClickTab()
    {
        hasClickedTab = true;
    }
}
