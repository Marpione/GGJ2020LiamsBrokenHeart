using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class UIManager : Singleton<UIManager>
{
    public ItemSelectionPanel ItemSelectionPanel;

    public StoryboardPanel StoryboardPanel;

    public TypeTextPanel TypeTextPanel;



    public void ShowItemSelectionPanel(ItemPanelData data)
    {
        ItemSelectionPanel.ItemData = data;
        ItemSelectionPanel.ShowPanel();
    }

    public void HideItemSelectionPanel()
    {
        ItemSelectionPanel.HidePanel();
    }

    public void PlayStory(StoryboardData storyboardData)
    {
        StoryboardPanel.StoryboardData = storyboardData;
        StoryboardPanel.CreateStoryBoard();
    }

    public void ActivateText()
    {
        TypeTextPanel.ShowPanel();
    }

    public void DeactivateText()
    {
        TypeTextPanel.HidePanel();
    }
}
