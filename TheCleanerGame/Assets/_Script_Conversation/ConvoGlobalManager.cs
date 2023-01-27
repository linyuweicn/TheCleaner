using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConvoGlobalManager : MonoBehaviour
{
    [SerializeField] int nextScene;

    // Start is called before the first frame update
    void Start()
    {
        nextScene = SceneManager.GetActiveScene().buildIndex + 1; //default
    }

    public void setNextScene(int scene)
    {
        nextScene = scene;
    }

    public int getNextScene()
    {
        return nextScene;
    }
}
