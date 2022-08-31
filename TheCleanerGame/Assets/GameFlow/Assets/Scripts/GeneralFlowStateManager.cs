using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GeneralFlowStateManager : MonoBehaviour
{
    #region variables
    public static GeneralFlowStateManager instance;

    public PromptManager promptManager;
    public AnswerManager answerManager;
    public ScoreManager scoreManager;

    [SerializeField] GameObject promptObjects;
    [SerializeField] Image promptBox;
    [SerializeField] TextMeshProUGUI promptText;
    [SerializeField] PromptNextButton promptButton;
    [SerializeField] GameObject scoreObjects;

    [HideInInspector] public PitchContainer focusedContainer;
    [HideInInspector] public Prompt currentPrompt;
    bool inTransitionState;

    #endregion
    private void Awake()
    {
        instance = this;
        currentPrompt = null;
    }
    private void Start()
    {
        promptObjects.SetActive(false);
        currentPrompt = null;
    }

    public bool SwitchToPromptState(PitchTypes type, int promptNo)
    {
        if (type == PitchTypes.None || promptNo < 0)
        {
            currentPrompt = null;
            promptText.text = "";
            return true;
        }

        Prompt p = promptManager.GetPrompt(type, promptNo);
        if (p != null)
        {
            currentPrompt = p;
            promptText.text = p.text;
            return true;
        } else
        {
            return false;
        }
    }

    public bool SwitchToPromptState(Prompt prompt)
    {
        return SwitchToPromptState(prompt.pitchType, prompt.promptNo);
    }

    public void TransitionToRank(PitchTypes type, int promptNo, PitchContainer container)
    {
        if (!inTransitionState && SwitchToPromptState(type, promptNo))
        {
            inTransitionState = true;
            focusedContainer = container;
            scoreObjects.SetActive(false);

            StartCoroutine(MovingToRankingState(container));
        }
    }

    IEnumerator MovingToRankingState(PitchContainer container)
    {
        var containers = FindObjectsOfType<PitchContainer>();
        foreach (PitchContainer c in containers)
        {
            c.SetUpForRankState();
        }

        float elapsed = 0.0f;

        while (elapsed <= container.timeToTransitionToRank)
        {
            container.transform.localPosition = Vector3.Lerp(container.originalPosition, container.transformedRankPosition, elapsed / container.timeToTransitionToRank);
            container.transform.localScale = Vector3.Lerp(container.originalScale, container.localScaleAtRanking, elapsed / container.timeToTransitionToRank);
            
            foreach (PitchContainer c in containers)
            {
                if (c != container)
                {
                    c.transform.localPosition = Vector3.Lerp(c.originalPosition, c.transformedUnfocusedPosition, elapsed / c.timeToTransitionToRank);
                }
            }
            
            elapsed += Time.deltaTime;
            yield return null;
        }

        container.transform.localScale = container.localScaleAtRanking;
        container.transform.localPosition = container.transformedRankPosition;

        promptObjects.SetActive(true);
        promptBox.color = container.promptBoxColor;

        if (currentPrompt.visited)
        {
            promptButton.Show();
        }

        answerManager.GenerateAnswers(currentPrompt);

        inTransitionState = false;
    }

    public void TransitionToDefault(PitchContainer container)
    {
        if (!inTransitionState && SwitchToPromptState(PitchTypes.None, -1))
        {
            inTransitionState = true;
            focusedContainer = null;

            promptButton.Hide();
            promptObjects.SetActive(false);

            answerManager.DeSpawnAnswers();
            StartCoroutine(MovingToDefaultState(container));
        }
    }

    IEnumerator MovingToDefaultState(PitchContainer container)
    {
        var containers = FindObjectsOfType<PitchContainer>();
        float elapsed = 0.0f;
        
        while (elapsed <= container.timeToTransitionToRank)
        {
            container.transform.localPosition = Vector3.Lerp(container.transformedRankPosition, container.originalPosition, elapsed / container.timeToTransitionToRank);
            container.transform.localScale = Vector3.Lerp(container.localScaleAtRanking, container.originalScale, elapsed / container.timeToTransitionToRank);

            foreach (PitchContainer c in containers)
            {
                if (c != container)
                {
                    c.transform.localPosition = Vector3.Lerp(c.transformedUnfocusedPosition, c.originalPosition, elapsed / c.timeToTransitionToRank);
                }
            }

            elapsed += Time.deltaTime;

            yield return null;
        }
        container.transform.localScale = container.originalScale;

        foreach (PitchContainer c in containers)
        {
            c.transform.localPosition = c.originalPosition;
            c.SetUpForDefaultState();
        }

        scoreObjects.SetActive(true);

        inTransitionState = false;
    }

    public void VisitPrompt()
    {
        if (!currentPrompt.visited)
        {
            currentPrompt.visited = true;
            
            promptButton.Show();
            focusedContainer.UpdateText();
        }
    }
}
