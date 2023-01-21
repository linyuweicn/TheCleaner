using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PromptType
{
    Theme,
    Character,
    Setting,
    Narration,
    Protagonist,
    Antagonist,
    Art
}
public enum FeedbackType
{
    Null,
    Critical,
    Neutral,
    Summary,
    Note
}

public enum CriticType
{
    Yiran,
    Benjamine,
    Team,
    Luca,
    Charlie
    
}

public enum BrainstormState
{
    Inactive,
    Menu,
    Rank,
    Decision
}

public enum FeedbackState
{
    Null,
    Neutral,
    Critical,
    Summary
}

public enum NeutralFeedbackState
{
    Inactive,
    Triggered,
    Active
}

public enum ButtonType
{
    Forward,
    Back,
    CloseFeedback
}