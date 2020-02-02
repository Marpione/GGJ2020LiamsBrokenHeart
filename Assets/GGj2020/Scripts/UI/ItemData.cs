using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum ItemType { Story, Consume}

public class ItemData : ScriptableObject
{
    public ItemType ItemType;

    public Sprite ItemIcon;


    public string ConnectedInteractive;

    [ShowIf("isStory")]
    public StoryboardData StoryBoard;

    [HideIf("isStory")]
    public string ActionTrigger;

    public float MoodPoint;



    bool isStory { get { return ItemType == ItemType.Story; } }
}
