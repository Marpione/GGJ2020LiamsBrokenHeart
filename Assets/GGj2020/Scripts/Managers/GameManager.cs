using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GameManager : Singleton<GameManager>
{
    public StoryboardData StartingStoryData;

    public List<bool> PlayedMemories;

    [PropertyRange(0, 100)]
    public float Mood;

    private void Start()
    {
        StartStory();
    }

    [Button]
    public void StartStory()
    {
        UIManager.Instance.PlayStory(StartingStoryData);
    }

    public void SetMood(float moodPoint)
    {
        Mood += moodPoint;
        if (Mood < 0)
            Mood = 0;
        if (Mood > 100)
            Mood = 100;

        if (Mood > 65)
        {
            ActionAnimationController.Instance.SetAction("Crying", false);
            UIManager.Instance.DeactivateText();
            MoodManager.Instance.NormalMood();
        }
        else if (Mood <= 64 && Mood > 40)
        {
            MoodManager.Instance.SemiDarkMood();
            ActionAnimationController.Instance.SetAction("Crying", false);
            UIManager.Instance.DeactivateText();
        }
        else if(Mood <= 63 && Mood > 15)
        {
            MoodManager.Instance.DarkMood();
            ActionAnimationController.Instance.SetAction("Crying", false);
            UIManager.Instance.DeactivateText();
        }
        else if(Mood <= 14)
        {
            ActionAnimationController.Instance.SetAction("Crying", true);
            ActionAnimationController.Instance.TriggerAction("Cry");
            UIManager.Instance.ActivateText();
        }
    }
}
