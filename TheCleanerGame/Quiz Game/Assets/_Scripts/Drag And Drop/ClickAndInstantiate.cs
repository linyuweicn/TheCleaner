using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndInstantiate : MonoBehaviour
{
    public GameObject imgaeSelected;
    
    public GameObject location;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateAt()
    {
        GameObject froInstantiate = Instantiate(
            imgaeSelected,
            new Vector2(location.transform.position.x, location.transform.position.y),
            Quaternion.identity) as GameObject;

        froInstantiate.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
    }
}
