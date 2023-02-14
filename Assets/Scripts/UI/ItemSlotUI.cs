using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotUI : MonoBehaviour
{
    public Button button;
    public Image icon;
    public TextMeshProUGUI quantityText;
    private ItemSlot curSlot;
    public Outline outline;
    public GameObject selectedItemData;
    public GameObject ImgBGItem;
    public Image fillHealthItem;

    public int index;
    public bool equipped;

    void Awake()
    {
        outline = GetComponent<Outline>();
    }

    void OnEnable()
    {
        outline.enabled = equipped;
    }

    public void Set(ItemSlot slot)
    {
        curSlot = slot;
        icon.gameObject.SetActive(true);

        if(curSlot.item.typeItem == ItemType.Equipable)
        {
            ImgBGItem.SetActive(true);
        }
        else
        {
            ImgBGItem.SetActive(false);
        }

        icon.sprite = slot.item.iconItem;
        quantityText.text = slot.quantity > 1 ? slot.quantity.ToString() : string.Empty;

        if(outline != null)
        {
            outline.enabled = equipped;
        }
    }

    public void DesactiveBarHealth()
    {
        ImgBGItem.SetActive(false);
    }

    public void Clear()
    {
        curSlot = null;
        DesactiveBarHealth();

        icon.gameObject.SetActive(false);
        quantityText.text = string.Empty;
    }

    public void OnButtonClick()
    {
        Inventory.instance.SelectItem(index);
    }
}
