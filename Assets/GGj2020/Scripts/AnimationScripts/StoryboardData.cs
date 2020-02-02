using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoryFrame
{
    public Sprite StoryboardSprite;
    public Vector2 StoryPostion;
}

public class StoryboardData : ScriptableObject
{
    public List<StoryFrame> storyFrames;
}
