using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuildingRecipeUI : MonoBehaviour
{
    public BuildingRecipe recipeBuilding;
    public Image backgroundImage;
    public Image icon;
    public TextMeshProUGUI buildingNameText;
    public Image[] resourceCostBuilding;

    public Color canBuildColor;
    public Color cannotBuildColor;
    private bool canBuild;

    private void OnEnable()
    {
        UpdateCanCraft();
    }

    private void Start()
    {
        UpdateDataBuilding();
    }

    private void UpdateDataBuilding()
    {
        icon.sprite = recipeBuilding.icon;
        buildingNameText.text = recipeBuilding.displayName;

        for (int x = 0; x < resourceCostBuilding.Length; x++)
        {
            if(x < recipeBuilding.resourceCosts.Length)
            {
                resourceCostBuilding[x].gameObject.SetActive(true);

                resourceCostBuilding[x].sprite = recipeBuilding.resourceCosts[x].item.iconItem;
                resourceCostBuilding[x].transform.GetComponentInChildren<TextMeshProUGUI>().text =
                    recipeBuilding.resourceCosts[x].quantity.ToString();
            }
            else
            {
                resourceCostBuilding[x].gameObject.SetActive(false);
            }
        }
    }

    void UpdateCanCraft() 
    {
        canBuild = true;

        for (int x = 0; x < recipeBuilding.resourceCosts.Length; x++)
        {
            var hasItemsBuilding = Inventory.instanceInventory.HasItems(recipeBuilding.resourceCosts[x].item, recipeBuilding.resourceCosts[x].quantity);
            if(!hasItemsBuilding)
            {
                canBuild = false;
                break;
            }
        }

        backgroundImage.color = canBuild ? canBuildColor : cannotBuildColor;
    }
}
