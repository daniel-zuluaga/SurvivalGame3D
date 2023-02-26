using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingWindow : MonoBehaviour
{
    public CraftingRecipesUI[] craftRecipesUIs;

    public static CraftingWindow instanceCraftingWindow;

    private void Awake()
    {
        instanceCraftingWindow = this;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

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

    public void Craft(CraftingRecipes craftingRecipes)
    {
        for (int i = 0; i < craftingRecipes.costRecipes.Length; i++)
        {
            for (int x = 0; x < craftingRecipes.costRecipes[i].quantity; x++)
            {
                Inventory.instanceInventory.RemoveItem(craftingRecipes.costRecipes[i].item);
            }
        }

        Inventory.instanceInventory.AddItem(craftingRecipes.itemToCraft);

        for (int x = 0; x < craftRecipesUIs.Length; x++)
        {
            craftRecipesUIs[x].UpdateCanCraft();
        }
    }
}
