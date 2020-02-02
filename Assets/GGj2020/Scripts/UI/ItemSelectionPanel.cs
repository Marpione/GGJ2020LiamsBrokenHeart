using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using DG.Tweening;

public class ItemSelectionPanel : Panel
{
    public ItemPanelData ItemData;

    public GameObject ItemPrefab;
    public Transform ContentHolder;

    List<Item> createdItems = new List<Item>();

    Image backgroundImage;
    Image BackgroundImage { get { return (backgroundImage == null) ? backgroundImage = GetComponent<Image>() : backgroundImage; } }

    public override void ShowPanel()
    {
        base.ShowPanel();

        BackgroundImage.DOFade(0.4f, 0.7f).OnComplete(CreateItems);
    }

    public override void HidePanel()
    {
        BackgroundImage.DOFade(0, 1f).OnComplete(()=> {base.HidePanel(); }).SetDelay(0.6f);
        RemoveItems();
        ItemData = null;
    }

    [Button]
    public void CreateItems()
    {
        for (int i = 0; i < ItemData.itemData.Count; i++)
        {
            Item item = Instantiate(ItemPrefab, ContentHolder.transform).GetComponent<Item>();

            item.ShowPanel(0.4f * i);
            createdItems.Add(item);
            item.SetItemData(ItemData.itemData[i]);
        }
    }

    [Button]
    public void RemoveItemsImidiate()
    {
        for (int i = 0; i < createdItems.Count; i++)
        {
            if (createdItems[i] != null)
                Destroy(createdItems[i].gameObject);
        }

        createdItems.Clear();
    }

    public void RemoveItems()
    {
        for (int i = 0; i < createdItems.Count; i++)
        {
            if(createdItems[i] != null)
                createdItems[i].HidePanel(0.3f * i);
        }

        createdItems.Clear();
    }
    
}
