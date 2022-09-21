using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class CensorshipUI : MonoBehaviour
{
    #region variables
    [SerializeField] List<ImageID> imageList;
    [SerializeField] TextMeshProUGUI criticText;
    [SerializeField] TextMeshProUGUI feedbackText;

    Image activeImage = null;
    Dictionary<string, Image> imageDictionary;
    #endregion

    #region private classes
    [Serializable] struct ImageID
    {
        public string name;
        public Image image;
    }
    #endregion
    void Awake()
    {
        imageDictionary = new Dictionary<string, Image>();
        foreach (ImageID id in imageList)
        {
            imageDictionary.Add(id.name, id.image);
        }
    }
    void Start()
    {
        Hide();
    }

    
    void Update()
    {
        
    }

    public void Show()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        GeneralFlowStateManager.instance.answerManager.SetAreAnswersClickable(false);
        GeneralFlowStateManager.instance.promptButton.Hide();
    }
    public void Hide()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        GeneralFlowStateManager.instance.answerManager.SetAreAnswersClickable(true);
        GeneralFlowStateManager.instance.promptButton.Show();
    }
    public void SetCensorshipUI(Answer answer)
    {
        SetCensorshipUI(answer.imageText, answer.criticName, answer.feedback);
    }
    public void SetCensorshipUI(string imageName, string criticName, string feedback)
    {
        SetImage(imageName);
        SetCriticText(criticName);
        SetFeedback(feedback);
    }

    void SetImage(string imageName)
    {
        if (activeImage != null)
        {
            activeImage.gameObject.SetActive(false);
        }

        if (imageDictionary.ContainsKey(imageName))
        {
            imageDictionary[imageName].gameObject.SetActive(true);
            activeImage = imageDictionary[imageName];
        } else
        {
            Debug.LogError(imageName + " is not stored in Images under censorshipUI");
        }

    }

    void SetCriticText(string criticName)
    {
        criticText.text = criticName;
    }

    void SetFeedback(string feedback)
    {
        feedbackText.text = feedback;
    }

}
