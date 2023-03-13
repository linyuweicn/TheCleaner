using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class NonAnimatedObjects : MonoBehaviour
{
    [SerializeField] NPCConversation PosterDescription;
    private bool hasClicked = false;
    private BoxCollider2D Postercollider;

    private void Start()
    {
        Postercollider = GetComponent<BoxCollider2D>();
    }
    private void OnMouseDown()
    {
        if (!hasClicked)
        {
            ConversationManager.Instance.StartConversation(PosterDescription);
            hasClicked = true;
            Postercollider.enabled = false;
        }
        else
        {
            Postercollider.enabled = false;
        }
        
    }
}
