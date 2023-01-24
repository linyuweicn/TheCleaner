using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WeekUpdate : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Text;
    public bool isWeekNumber;

    private int WeekNumber;
    // Start is called before the first frame update
    void Start()
    {
        if (isWeekNumber)
        {
            WeekNumber = SceneManager.GetActiveScene().buildIndex;
            Text.text = "Week 0" + WeekNumber;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
