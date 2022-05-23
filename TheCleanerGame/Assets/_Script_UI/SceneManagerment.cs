using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerment : MonoBehaviour
{
    //public GameObject GameOverPanel;

    private void Start()
    {
        //GameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(0);
            ResetNumbers();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(1);
            ResetNumbers();

        }
        /*else if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameOverPanel.SetActive(false);
            ResetNumbers();

        }*/
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene(2);
            ResetNumbers();

        }else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

       /* if (Score.PlayerSatisValue <= 0 || Score.SafetyValue <= 0 || Score.MoneyValue <= 0)
        {
            GameOverPanel.SetActive(true);

            
        }*/
    }

    void ResetNumbers()
    {

        Score.PlayerSatisValue = 50;
        Score.MoneyValue = 50;
        Score.SafetyValue = 50;
    }
}
