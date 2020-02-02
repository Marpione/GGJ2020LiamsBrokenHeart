using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TypeTextPanel : Panel
{

    public GameObject TextPrefab;
    public Transform Content;
    float typeingTimeStart;
    public float waitDuration;

    string typedText;

    List<Text> currentTexts = new List<Text>();

    bool canType;

    public override void ShowPanel()
    {
        base.ShowPanel();
        canType = true;
    }

    public override void HidePanel()
    {
        base.HidePanel();
        canType = false;
    }

    private void OnGUI()
    {
        if (!canType)
            return;

        TypeText();
        RemoveText();
    }

    public void TypeText()
    {
        Event e = Event.current;
        if (e == null)
            return;

        if(Input.anyKeyDown)
        {
            if (e.isKey)
            {
                if (Time.time > typeingTimeStart)
                {
                    typeingTimeStart = Time.time;
                    Text currentText = Instantiate(TextPrefab, Content).GetComponent<Text>();
                    string currentChar = e.keyCode.ToString();

                    if (string.Equals(currentChar, "Space"))
                        currentChar = " ";

                    currentText.text += currentChar;
                    typedText += e.keyCode.ToString();

                    currentTexts.Add(currentText);
                }
            }
        }
        
    }
    bool lastListDeleted;
    public void RemoveText()
    {
        //if (!lastListDeleted)
        //    return;

        if(Time.time > typeingTimeStart + waitDuration)
        {
            for (int i = 0; i < currentTexts.Count; i++)
            {
                Text animatingText = currentTexts[i];
                animatingText.DOFade(0, 1f).OnComplete(() => { Destroy(animatingText.gameObject); GameManager.Instance.SetMood(0.3f); });
                currentTexts.Remove(animatingText);
            }
        }
       
    }
}
