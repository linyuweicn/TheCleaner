using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
public class PitchContainer : MonoBehaviour //handles behavior of pitch containers (the logo that you click to switch to rank state)
{
    #region variables
    [SerializeField] public PitchTypes pitchType;
    [SerializeField] TextMeshProUGUI labelText;
    [SerializeField] TextMeshProUGUI fractionText;
    [SerializeField] public Image logo;
    [SerializeField] public Vector3 transformedRankPosition;
    [SerializeField] public Color promptBoxColor;
    [SerializeField] public float timeToTransitionToRank;
    [SerializeField] public Vector3 localScaleAtRanking;
    [SerializeField] public Vector3 transformedUnfocusedPosition;

    [HideInInspector] public Vector3 originalPosition;
    [HideInInspector] public Vector3 originalScale;
    #endregion

    #region initialization
    private void Awake()
    {
        originalPosition = transform.localPosition;
        originalScale = transform.localScale;
    }
    void Start()
    {
        UpdateText();   
    }

    #endregion
    
    void Update()
    {
        
    }

    #region visual state changer
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void UpdateText()
    {
        //UpdateIdeas();
        UpdateFractions();
    }

    public void UpdateFractions()
    {
        PromptManager pMang = GeneralFlowStateManager.instance.promptManager;
        fractionText.text = pMang.GetNumVisited(pitchType) + " / " + pMang.GetMaximum(pitchType);
    }
    #endregion

    #region on click commands
    public void Selected()
    {
        if (GeneralFlowStateManager.instance.focusedContainer == null)
        {
            SetUpForRankState();
            Prompt nextPrompt = GeneralFlowStateManager.instance.promptManager.GetNextPrompt(pitchType);
            //Debug.Log("clicked " + nextPrompt.promptNo);
            GeneralFlowStateManager.instance.TransitionToRank(pitchType, nextPrompt.promptNo, this);
        } else
        {
            //Debug.Log("What is the last prompt: " + GeneralFlowStateManager.instance.currentPrompt.promptNo + " " + GeneralFlowStateManager.instance.currentPrompt.pitchType);
            GeneralFlowStateManager.instance.TransitionToDefault(this);
        }
    }
    public void SetUpForRankState()
    {
        fractionText.enabled = false;
        labelText.enabled = false;
    }

    public void SetUpForDefaultState()
    {
        fractionText.enabled = true;
        labelText.enabled = true;
    }

    #endregion
}
