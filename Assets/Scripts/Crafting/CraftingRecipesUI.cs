using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingRecipesUI : MonoBehaviour
{
    public CraftingRecipes craftingRecipes;
    public Image backgroundImage;
    public Image icon;
    public TextMeshProUGUI itemName;
    public Image[] resourcesCosts;

    public Color canCraftColor;
    public Color cannotCraftColor;
    private bool canCraft;

    private void OnEnable()
    {
        UpdateCanCraft();
    }

    public void UpdateCanCraft()
    {
        canCraft = true;

        for (int x = 0; x < craftingRecipes.costRecipes.Length; x++)
        {
            var hasItemsRecipes = Inventory.instanceInventory.HasItems(craftingRecipes.costRecipes[x].item, craftingRecipes.costRecipes[x].quantity);

            if (!hasItemsRecipes)
            {
                canCraft = false;
                break;
            }
        }

        backgroundImage.color = canCraft ? canCraftColor : cannotCraftColor;
    }

    private void Start()
    {
        icon.sprite = craftingRecipes.itemToCraft.iconItem;
        itemName.text = craftingRecipes.itemToCraft.nameItem;

        for (int x = 0; x < resourcesCosts.Length; x++)
        {
            if (x < craftingRecipes.costRecipes.Length)
            {
                resourcesCosts[x].gameObject.SetActive(true);
                resourcesCosts[x].sprite = craftingRecipes.costRecipes[x].item.iconItem;
                resourcesCosts[x].transform.GetComponentInChildren<TextMeshProUGUI>().text = craftingRecipes.costRecipes[x].quantity.ToString();
            }
            else
            {
                resourcesCosts[x].gameObject.SetActive(false);
            }
        }
    }

    public void OnClickButtonCraftRecipes()
    {
        if (canCraft)
        {
            CraftingWindow.instanceCraftingWindow.Craft(craftingRecipes);
        }
    }
}
