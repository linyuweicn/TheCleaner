using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class YiranConversation : MonoBehaviour
{
    private int convSequence;
    public NPCConversation introConversation;
    public NPCConversation OfficeConversation1;
    Animator YiranAnim;

    private void Start()
    {
        ConversationManager.Instance.StartConversation(introConversation);
        YiranAnim = GetComponent<Animator>();

    }

    public void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            convSequence++;

            switch (convSequence)
            {
                case 1:
                    ConversationManager.Instance.StartConversation(OfficeConversation1);
                    Debug.Log("C1");
                    break;

                case 2:
                    Debug.Log("C2");
                    break;
            }
        }
    }

    public void YiranDisappear( )
    {
        YiranAnim.SetTrigger("WalkOffice");
    }

    public void InstantiateYiran()
    {

    }
}
