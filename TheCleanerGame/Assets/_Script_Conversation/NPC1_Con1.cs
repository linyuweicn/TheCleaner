using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.UI;

public class NPC1_Con1 : MonoBehaviour
{
    // this script should be intergrated with NPCConWithWhiteBoard.


    
    public NPCConversation BuyLicenseOrBetraylConversation;
    public NPCConversation RefuseLicenseOrCoverConversation;
    private BoxCollider2D CharaCollider;
    ConvoGlobalManager LicenseResult;
    private bool canturnoff;


    [SerializeField] BoxCollider2D[] boxCollider2D;


    void Start()
    {
        CharaCollider = gameObject.GetComponent<BoxCollider2D>();
        LicenseResult = FindObjectOfType<ConvoGlobalManager>();
        //BetraylResult = FindObjectOfType<ConvoGlobalManager>();

    }

    // Update is called once per frame
    void Update()
    {
       if (ConversationManager.Instance != null ) 
        {
            if (ConversationManager.Instance.IsConversationActive)
            {

                for (int i = 0; i < boxCollider2D.Length; i++)
                {
                    boxCollider2D[i].enabled = false; // do not set collider to unenabled for those gameObjects. It will conflict with GenrealObjects Scripst.
                    
                }


            }
            else // if conv is not active
            {
                for (int i = 0; i < boxCollider2D.Length; i++)
                {
                    boxCollider2D[i].enabled = true; // do not set collider to unenabled for those gameObjects. It will conflict with GenrealObjects Scripst.

                }
            }
        }

        if (canturnoff)
        {
            CharaCollider.enabled = false;
        }
    }

    public void OnMouseDown()
    {
        StartConv();

    }


    public void StartConv()
    {

        if (LicenseResult.BuyLicense || ConvoGlobalManager.Betrayl)
        {
            ConversationManager.Instance.StartConversation(BuyLicenseOrBetraylConversation);

            
        }
        else
        {
            ConversationManager.Instance.StartConversation(RefuseLicenseOrCoverConversation);
            
        }
        canturnoff = true;
    }

    }
    

