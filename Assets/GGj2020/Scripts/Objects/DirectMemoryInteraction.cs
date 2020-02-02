using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectMemoryInteraction : InteractiveObject
{
    public List<StoryboardData> stories;

    public override void Interact()
    {
        UIManager.Instance.PlayStory(stories[Random.Range(0, stories.Count)]);
    }
}
