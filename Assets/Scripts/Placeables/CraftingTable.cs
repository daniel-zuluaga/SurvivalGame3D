using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : Buildings, IInteractable
{
    public CraftingWindow craftingWindow;
    public PlayerController playerController;

    private void Start()
    {
        craftingWindow = FindObjectOfType<CraftingWindow>(true);
        playerController = FindObjectOfType<PlayerController>();
    }

    public string GetInteractPrompt()
    {
        return "Craft";
    }

    public void OnInteract()
    {
        craftingWindow.gameObject.SetActive(true);
        playerController.ToggleCursor(true);
    }
}
