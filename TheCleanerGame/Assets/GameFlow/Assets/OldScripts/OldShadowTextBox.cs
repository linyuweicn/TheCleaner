using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OldShadowTextBox : MonoBehaviour
{
    #region variables
    [SerializeField] Image illuminated;
    int initialRanking;
    #endregion

    #region initialization
    void Start()
    {
        
    }

    public void Construct(int rank)
    {
        initialRanking = rank;
        if (rank == 0)
        {
            illuminated.enabled = true;
        } else
        {
            illuminated.enabled = false;
        }
    }

    #endregion
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
