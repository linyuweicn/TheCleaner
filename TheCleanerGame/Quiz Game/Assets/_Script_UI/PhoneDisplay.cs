using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("TimeParent").GetComponent<CanvasGroup>().alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hover()
    {
        GameObject.Find("TimeParent").GetComponent<CanvasGroup>().alpha = 1;
    }

    public void StopHover()
    {
        GameObject.Find("TimeParent").GetComponent<CanvasGroup>().alpha = 0;
    }
}
