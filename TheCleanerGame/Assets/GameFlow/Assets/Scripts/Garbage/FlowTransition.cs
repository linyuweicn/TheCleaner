using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowTransition : MonoBehaviour
{
    // Start is called before the first frame update
    FlowTransitionManager fMang;
    Vector3 origPos;
    [SerializeField] Vector3 unFocusedPos;
    bool isMoving;
    Vector3 destination;
    float moveSpeed;
    [SerializeField] GameObject items;
    public PitchTypes type;
    void Start()
    {
        fMang = FindObjectOfType<FlowTransitionManager>();
        isMoving = false;
        origPos = transform.localPosition;
        destination = origPos;
        moveSpeed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (Vector3.Distance(transform.localPosition, destination) < 0.001f)
            {
                transform.localPosition = destination;
                StopMoving();

                if (!items.activeInHierarchy && transform.localPosition == origPos)
                {
                    items.SetActive(true);
                    PitchContainer.instances[type].Show();
                } else if (transform.localPosition == fMang.Dest)
                {
                    fMang.FinishFocus();
                }

            } else
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, moveSpeed * Time.deltaTime);
            }
        }
    }

    public void Transition()
    {
        if (fMang.FlowState == FlowTransitionManager.State.Default)
        {
            StartTransition();
            fMang.Focus(this, PitchContainer.instances[type].NumCompletedPrompts, 0);
        } else if (fMang.FlowState == FlowTransitionManager.State.Focused)
        {
            EndTransition();
        }
    }

    public void StartTransition()
    {
        fMang.FlowState = FlowTransitionManager.State.Focused;

        foreach (FlowTransition f in fMang.transitions)
        {
            f.OffScreen();
        }

        StopMoving();
        MoveTo(fMang.Dest);

        PitchContainer.instances[type].Hide();
    }

    public void StartTransition(int promptNo)
    {
        StartTransition();
        fMang.Focus(this, promptNo, 0);
    }

    public void EndTransition()
    {
        fMang.UnFocus();
        PitchContainer.instances[type].UpdateText();
    }

    public void MoveTo(Vector3 dest)
    {
        destination = dest;
        isMoving = true;
        moveSpeed = Vector3.Distance(dest, transform.localPosition) / fMang.TransTime;
        items.SetActive(false);
    }

    public void StopMoving()
    {
        isMoving = false;
        destination = transform.localPosition;
        moveSpeed = 0.0f;
    }

    public void OffScreen()
    {
        StopMoving();
        PitchContainer.instances[type].Hide();
        MoveTo(unFocusedPos);
    }

    public void ResetPos()
    {
        StopMoving();
        MoveTo(origPos);
    }
}
