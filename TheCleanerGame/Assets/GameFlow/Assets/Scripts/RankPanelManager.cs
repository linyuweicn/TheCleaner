using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.UI;

public class RankPanelManager : MonoBehaviour
{
    #region variables
    [SerializeField] TextMeshProUGUI promptText;

    [SerializeField] Vector3Int firstAnswerPos, secondAnswerPos, thirdAnswerPos;
    [SerializeField] int heightDiff;
    #endregion
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region
    public void UpdatePromptText()
    {
        promptText.text = BrainstormGeneralManager.Instance.FocusedContainer.Prompt.Text;
    }
    #endregion
}
