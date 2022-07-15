using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
public class PitchContainer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PitchTypes type;
    [SerializeField] List<PitchItems> pitchBoxes;
    [SerializeField] TextMeshProUGUI fractionText;

    public static Dictionary<PitchTypes, PitchContainer> instances;
    public FlowTransition fTrans;
    int numCompletedPrompts;

    FlowTransitionManager fMang;
    PromptManager pMang;

    private void Awake()
    {
        if (instances == null)
        {
            instances = new Dictionary<PitchTypes, PitchContainer>();
        }
        instances.Add(type, this);
    }
    void Start()
    {
        fMang = FindObjectOfType<FlowTransitionManager>();
        fTrans = GetComponent<FlowTransition>();
        UpdateText();
        numCompletedPrompts = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hide()
    {
        foreach (PitchItems p in pitchBoxes)
        {
            p.gameObject.SetActive(false);
        }
    }

    public void Show()
    {
        foreach(PitchItems p in pitchBoxes)
        {
            p.gameObject.SetActive(true);
        }
    }

    public void UpdateText()
    {
        //UpdateIdeas();
        UpdateFractions();
    }

    public void UpdateFractions()
    {
        fractionText.text = numCompletedPrompts + " / " + Maximum;
    }

    public void UpdateIdeas()
    {
       for (int i = 0; i < Maximum; i++)
        {
             
        }
    }

    public int Maximum
    {
        get { return pitchBoxes.Count; }
    }

    public int NumCompletedPrompts
    {
        get { return numCompletedPrompts; }
    }

    public void AddCompletedPrompts(int amount)
    {
        numCompletedPrompts += amount;
    }
}
