using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public ItemPanelData itemPanelData;


    public virtual void Interact()
    {
        UIManager.Instance.ShowItemSelectionPanel(itemPanelData);
    }
}
