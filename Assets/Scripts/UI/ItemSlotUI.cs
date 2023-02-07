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
    public GameObject fillHealthItem;

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
            fillHealthItem.SetActive(true);
        }
        else
        {
            fillHealthItem.SetActive(false);
        }

        icon.sprite = slot.item.iconItem;
        quantityText.text = slot.quantity > 1 ? slot.quantity.ToString() : string.Empty;

        if(outline != null)
        {
            outline.enabled = equipped;
        }
    }

    public void Clear()
    {
        curSlot = null;
        fillHealthItem.SetActive(false);

        icon.gameObject.SetActive(false);
        quantityText.text = string.Empty;
    }

    public void OnButtonClick()
    {
        Inventory.instance.SelectItem(index);
    }
}
