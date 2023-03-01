using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingWindow : MonoBehaviour
{
    private void OnEnable()
    {
        Inventory.instanceInventory.onOpenInventory.AddListener(OnOpenInventory);   
    }

    private void OnDisable()
    {
        Inventory.instanceInventory.onOpenInventory.RemoveListener(OnOpenInventory);
    }

    public void OnOpenInventory()
    {
        gameObject.SetActive(false);
    }
}
