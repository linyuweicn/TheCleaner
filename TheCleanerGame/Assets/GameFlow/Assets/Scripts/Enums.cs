using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PromptType
{
    Theme,
    Character,
    Setting,
    Narration
}
public enum FeedbackType
{
    None,
    Positive,
    Neutral,
    Negative
}

public enum CriticType
{
    Yiran,
    Benjamine,
    Team,
    Luca,
    Charlie,
    Olivia
}

public enum BrainstormState
{
    Menu,
    TransToMenu,
    Rank,
    TransToRank,
    Inactive
}

public enum ContainerState
{
    Revealed,
    Hidden,
    Moving
}

public enum RankPanelState
{
    Ranking,
    Feedback
}

public enum ButtonType
{
    Forward,
    Back
}