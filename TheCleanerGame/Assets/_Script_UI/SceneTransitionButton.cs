using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionButton : MonoBehaviour
{
    [SerializeField] Animator aimator; 
    [SerializeField] GameObject PauseMene;
    public static bool gameIsPaused;
    void Start()
    {
       
    }

    
    void Update()
    {

        //Debug.Log("1");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();

        }

        else if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            
            ToNextScene();
            Debug.Log("to next scene");
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
        Debug.Log("quit");
    }

    
    public void ToStartPage()
    {
        SceneManager.LoadScene(0);

    }

    public void resume()
    {
        PauseMene.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    public void pauseGame()
    {
        PauseMene.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

}
