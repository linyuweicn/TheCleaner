using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChangeCardScale : MonoBehaviour
{
    public GameObject placeHolderButton;
    private GameObject placeholder;
    private Vector3 startPos;
    private Transform screenPosition;
    
    //see QuizManager correct(), wrong() fucntion
    public bool canCheckAnswer;

    [SerializeField] float waitTime;



    void Start()
    {
        //for card to return to original position
        startPos = transform.position;

        //get the screen's postiion
        screenPosition = GameObject.Find("QuestionPanel").transform;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseEnter()
    {
        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.3f);
        //transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }
    public void OnMouseExit()
    {
        transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.1f);
        //transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    public void DragCard()
    {
        //reset the canvas to camera overlay
        var screenPoint = (Vector3)Input.mousePosition;
        screenPoint.z = 10.0f; //distance of the plane from the camera
        transform.position = Camera.main.ScreenToWorldPoint(screenPoint);


        /*since the gridlayout will affect the dropped card position,I moved all cards to the TempCardPosition . 
            when releasing mouse, set all cards back to the original parent*/
        transform.SetParent(GameObject.Find("TempCardPosition").transform);

        //Debug.Log(Vector3.Distance(transform.position, screenPosition.position));

        
    }
    public void ReleaseCard()
    {
        

        if (Vector3.Distance(transform.position, screenPosition.position) < 90.03)
        {
            transform.position = screenPosition.position;
            canCheckAnswer = true;
            transform.DOScale(new Vector3(0f, 0f, 0f), 0.5f);
            StartCoroutine(ReturnOrigin());

        }
        else
        {
            canCheckAnswer = false;
            transform.SetParent(GameObject.Find("Cards").transform);
        }

    }

    private IEnumerator ReturnOrigin()
    {
        yield return new WaitForSeconds(waitTime);
        transform.SetParent(GameObject.Find("Cards").transform);
        transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.3f);
        canCheckAnswer = false;
    }

    public void BegineDrag()
    {
        //GameObject placeholder = GameObject.Instantiate(placeHolderButton, Vector3.zero, Quaternion.identity, GameObject.Find("Cards").transform) as GameObject;
    }

    public void EndDrag()
    {
        //Destroy(Instantiate(placeHolderButton, Vector3.zero, Quaternion.identity, GameObject.Find("Cards").transform), 0.1f);
        //Destroy(placeholder);
    }



}
