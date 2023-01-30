using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjects : MonoBehaviour, IInteractable
{
    public ItemData itemData;

    public string GetInteractPrompt()
    {
        return string.Format("Pickup {0}", itemData.nameItem);
    }

    public void OnInteract()
    {
        Destroy(gameObject);
    }
}
