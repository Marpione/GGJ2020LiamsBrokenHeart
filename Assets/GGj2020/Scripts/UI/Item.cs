using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Item : Panel
{
    public ItemData ItemData;

    //Image iconImage;
    //Image IconImage { get { return (iconImage == null) ? iconImage = GetComponent<Image>() : iconImage; } }

    Button button;
    Button Button { get { return (button == null) ? button = GetComponent<Button>() : button; } }

    Image backgroundImage;
    Image BackgroundImage { get { return (backgroundImage == null) ? backgroundImage = GetComponent<Image>() : backgroundImage; } }

    Image outlineImage;
    Image OutlineImage { get { return (outlineImage == null) ? outlineImage = transform.GetChild(0).GetComponent<Image>() : outlineImage; } }

    public void ShowPanel(float delay)
    {

        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(delay)
            .AppendCallback(() => CanvasGroup.interactable = false)
            .Append(DOTween.To(() => CanvasGroup.alpha, x => CanvasGroup.alpha = x, 1, 1.5f))
            .AppendCallback(() => CanvasGroup.interactable = true).OnComplete(ShowPanel);
        
    }

    public void HidePanel(float delay)
    {
        DOTween.To(() => CanvasGroup.alpha, x => CanvasGroup.alpha = x, 0, 0.1f).SetDelay(delay).OnComplete(() => { Destroy(gameObject, 1); });

        ItemData = null;
    }

    public void SetItemData(ItemData data)
    {
        ItemData = data;
        OutlineImage.sprite = ItemData.ItemIcon;
        Button.onClick.AddListener(DoRelativeAction);
        Button.onClick.AddListener(UIManager.Instance.HideItemSelectionPanel);
    }


    public void DoRelativeAction()
    {
        switch (ItemData.ItemType)
        {
            case ItemType.Story:
                //Play Story here
                UIManager.Instance.PlayStory(ItemData.StoryBoard);
                GameManager.Instance.SetMood(ItemData.MoodPoint);
                BoxCollider boxCollider = GameObject.Find(ItemData.ConnectedInteractive).GetComponent<BoxCollider>();
                boxCollider.enabled = false;

                for (int i = 0; i < GameManager.Instance.PlayedMemories.Count; i++)
                {
                    if (!GameManager.Instance.PlayedMemories[i])
                    {
                        GameManager.Instance.PlayedMemories[i] = true;
                        break;
                    }

                    FindObjectOfType<DirectMemoryInteraction>().GetComponent<BoxCollider>().enabled = true;
                }
                break;
            case ItemType.Consume:
                //Change Mood Here
                GameManager.Instance.SetMood(ItemData.MoodPoint);
                ActionAnimationController.Instance.TriggerAction(ItemData.ActionTrigger);
                break;
            default:
                break;
        }
    }
}
