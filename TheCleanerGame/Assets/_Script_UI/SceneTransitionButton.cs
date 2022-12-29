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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(0);
        }

    }

    public void ToNextScene()
    {
        StartCoroutine(LoadNextScene());
        
    }

    IEnumerator LoadNextScene( )
    {
        if (aimator)
        {
            aimator.SetTrigger("Start");

            yield return new WaitForSeconds(1f);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
            
    }

    public void CloseGame()
    {
        Application.Quit();
    }
   
}
