using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolTipBrainstorm : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textComponent;
    public static ToolTipBrainstorm ToolTipInstance;

    private void Awake()
    {
        if (ToolTipInstance != null && ToolTipInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            ToolTipInstance = this;
        }
    }
    void Start()
    {
        gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    public void ShowToolMessage(string message)
    {
        //Debug.Log("show tooltip");
        gameObject.SetActive(true);
        textComponent.text = message;
    }

    public void HideToolMessage(string message)
    {
        //Debug.Log("disable tooltip");
        gameObject.SetActive(false);
        textComponent.text = "";
    }
}
