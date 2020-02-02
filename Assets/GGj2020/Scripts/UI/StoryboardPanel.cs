using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

public class StoryboardPanel : Panel
{
    public StoryboardData StoryboardData;
    public GameObject StoryPrefab;


    Button button;
    Button Button { get { return (button == null) ? button = GetComponent<Button>() : button; } }

    Image backgroundImage;
    Image BackgroundImage { get { return (backgroundImage == null) ? backgroundImage = GetComponent<Image>() : backgroundImage; } }

    List<Image> createdImages = new List<Image>();


    private void Awake()
    {
        ShowPanel();
    }

    public void CreateStoryBoard()
    {
        Button.interactable = false;
        ShowPanel();
        BackgroundImage.DOFade(1f, 2f);

        for (int i = 0; i < StoryboardData.storyFrames.Count; i++)
        {
            Image storyImage = Instantiate(StoryPrefab, transform).GetComponent<Image>();
            storyImage.sprite = StoryboardData.storyFrames[i].StoryboardSprite;
            storyImage.SetNativeSize();
            storyImage.GetComponent<RectTransform>().anchoredPosition = StoryboardData.storyFrames[i].StoryPostion;
            createdImages.Add(storyImage);
        }

        for (int i = 0; i < createdImages.Count; i++)
        {
            createdImages[i].DOFade(1f, 2f);
        }

        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(3).AppendCallback(() => { Button.interactable = true;} );
    }


    public void FinishStory()
    {
        for (int i = 0; i < createdImages.Count; i++)
        {
            if(createdImages[i] != null)
                createdImages[i].DOFade(0f, 0.5f).OnComplete(()=> Destroy(createdImages[i].gameObject));
        }

        BackgroundImage.DOFade(0, 2f).OnComplete(()=>
        {
            HidePanel();

            for (int i = 0; i < createdImages.Count; i++)
            {
                if (createdImages[i] != null)
                    Destroy(createdImages[i].gameObject);
            }

            createdImages.Clear();
        });

       
        
    }


}
