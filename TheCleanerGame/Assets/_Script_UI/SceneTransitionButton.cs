using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionButton : MonoBehaviour
{
    [SerializeField] Animator aimator; 
    void Start()
    {
       
    }

    
    void Update()
    {

        //Debug.Log("1");
    }

    public void ToNextScene()
    {
        StartCoroutine(LoadNextScene());
        
    }

    IEnumerator LoadNextScene( )
    {
        aimator.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
   
}
