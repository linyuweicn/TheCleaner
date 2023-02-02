using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionButton : MonoBehaviour
{
    [SerializeField] Animator aimator; 
    [SerializeField] GameObject PauseMene;
    private bool hasPaused;
    [SerializeField] ConvoGlobalManager cgm;
    public static bool gameIsPaused = false;
    void Start()
    {
       
    }

    
    void Update()
    {

        //Debug.Log("1");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameIsPaused)
            {
                pauseGame();
            }
            else
            {
                resume();
            }
            

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

    public void ToSpecificScene() //according to dialogue options chosen
    {
        
        if (cgm)
            StartCoroutine(LoadSpecificScene(cgm.GetComponent<ConvoGlobalManager>().getNextScene()));
    }

    IEnumerator LoadSpecificScene(int buildIndex)
    {
        if (aimator)
        {
            aimator.SetTrigger("Start");

            yield return new WaitForSeconds(1f);

            SceneManager.LoadScene(buildIndex);
        }
        else
        {
            SceneManager.LoadScene(buildIndex);
        }
    }

    public void CloseGame()
    {
        Application.Quit();
        Debug.Log("quit");
    }

    
    public void ToStartPage()
    {
        StartCoroutine(LoadSpecificScene(0));

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
