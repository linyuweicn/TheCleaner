using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetection : MonoBehaviour
{
    static string ObjectId = "object1";
    string Object1Panel = DragDropManager.GetObjectPanel(ObjectId);

    public static float PlatformV;
    public static float GenreV;
    public static float ContentV;
    public static float ProductionV;

    public GameObject objects;


    //You can check the value of the Panel1Object variable by using a switch command like this:
    void Start()
    {

        /*PlatformV = objects.GetComponent<ObjectSettings>().Platform;
        GenreV = objects.GetComponent<ObjectSettings>().Genre;
        ContentV = objects.GetComponent<ObjectSettings>().Content;
        ProductionV = objects.GetComponent<ObjectSettings>().Porduction;*/
        
    }

    public void OnDropSuccess()
    {
        if (gameObject.CompareTag( "Platform"))
        {
            PlatformV = objects.GetComponent<ObjectSettings>().Platform;
            Debug.Log(PlatformV);

        }else if (gameObject.CompareTag("Genre"))
        {
            GenreV = objects.GetComponent<ObjectSettings>().Genre;
            Debug.Log(GenreV);

        }else if (gameObject.CompareTag("Content"))
        {
            ContentV = objects.GetComponent<ObjectSettings>().Content;
            Debug.Log(ContentV);

        }else if (gameObject.CompareTag("Production"))
        {
            ProductionV = objects.GetComponent<ObjectSettings>().Porduction;
            Debug.Log(ProductionV);
        }

    }

}
