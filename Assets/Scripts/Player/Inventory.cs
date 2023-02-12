using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using TMPro;

public class Inventory : MonoBehaviour
{
    public ItemSlotUI[] uiSlots;
    public ItemSlot[] slots;

    public GameObject inventoryWindow;
    public Transform dropPosition;

    [Header("Selected Item")]
    private ItemSlot selectedItem;
    private int selectedItemIndex;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemStatNames;
    public TextMeshProUGUI selectedItemStatValues;
    public Image iconItem;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unEquipButton;
    public GameObject dropButton;

    private int curEquipIndex;

    // components
    public PlayerController controller;
    public PlayerNeeds needs;

    [Header("Events")]
    public UnityEvent onOpenInventory;
    public UnityEvent onCloseInventory;

    // singleton
    public static Inventory instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        inventoryWindow.SetActive(false);
        slots = new ItemSlot[uiSlots.Length];

        //inistialize the slots
        for (int x = 0; x < slots.Length; x++)
        {
            slots[x] = new ItemSlot();
            uiSlots[x].index = x;
            uiSlots[x].Clear();
        }

        ClearSelectedItemWindow();
    }

    public void OnInventoryButton(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        if (inventoryWindow.activeInHierarchy)
        {
            DesactiveInventory();
        }
        else
        {
            inventoryWindow.SetActive(true);
            onOpenInventory.Invoke();
            ClearSelectedItemWindow();
            controller.ToggleCursor(true);
        }
    }

    public void DesactiveInventory()
    {
        inventoryWindow.SetActive(false);
        onCloseInventory.Invoke();
        controller.ToggleCursor(false);
    }

    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    public void AddItem(ItemData item)
    {
        if (item.canStack)
        {
            ItemSlot slotToStackTo = GetItemStack(item);

            if(slotToStackTo != null)
            {
                slotToStackTo.quantity++;
                UpdateUI();
                return;
            }
        }

        ItemSlot emptySlot = GetEmptySlot();

        if(emptySlot != null)
        {
            emptySlot.item = item;
            emptySlot.quantity = 1;
            UpdateUI();
            return;
        }

        ThrowItem(item);
    }

    void ThrowItem(ItemData item)
    {
        Instantiate(item.prefabItem, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360f));
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
                uiSlots[i].Set(slots[i]);
            else
                uiSlots[i].Clear();
        }
    }

    ItemSlot GetItemStack(ItemData item)
    {
        for (int x = 0; x < slots.Length; x++)
        {
            if (slots[x].item == item && slots[x].quantity < item.maxStackAmount)
                return slots[x];
        }

        return null;
    }

    ItemSlot GetEmptySlot()
    {
        for (int x = 0; x < slots.Length; x++)
        {
            if (slots[x].item == null)
                return slots[x];
        }

        return null;
    }

    public void SelectItem(int index)
    {
        if (slots[index].item == null)
            return;

        selectedItem = slots[index];
        selectedItemIndex = index;

        iconItem.gameObject.SetActive(true);
        iconItem.sprite = selectedItem.item.iconItem;
        selectedItemName.text = selectedItem.item.nameItem;
        selectedItemDescription.text = selectedItem.item.descriptionItem;

        // set stat values and stat names
        selectedItemStatNames.text = string.Empty;
        selectedItemStatValues.text = string.Empty;

        for (int x = 0; x < selectedItem.item.consumables.Length; x++)
        {
            selectedItemStatNames.text += selectedItem.item.consumables[x].type.ToString() + "\n";
            selectedItemStatValues.text += " " + selectedItem.item.consumables[x].value.ToString() + "\n";
        }

        useButton.SetActive(selectedItem.item.typeItem == ItemType.Consumable);
        equipButton.SetActive(selectedItem.item.typeItem == ItemType.Equipable && !uiSlots[index].equipped);
        unEquipButton.SetActive(selectedItem.item.typeItem == ItemType.Equipable && uiSlots[index].equipped);
        dropButton.SetActive(true);

    }

    public void ClearSelectedItemWindow()
    {
        // clear the text elements
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedItemStatNames.text = string.Empty;
        selectedItemStatValues.text = string.Empty;
        iconItem.gameObject.SetActive(false);

        // disable buttons
        useButton.SetActive(false);
        equipButton.SetActive(false);
        unEquipButton.SetActive(false);
        dropButton.SetActive(false);
    }

    public void OnUseButton()
    {
        if(selectedItem.item.typeItem == ItemType.Consumable)
        {
            for (int x = 0; x < selectedItem.item.consumables.Length; x++)
            {
                switch (selectedItem.item.consumables[x].type)
                {
                    case ConsumableType.Hunger:
                        needs.Eat(selectedItem.item.consumables[x].value);
                        break;
                    case ConsumableType.Thirst:
                        needs.Drink(selectedItem.item.consumables[x].value);
                        break;
                    case ConsumableType.Health:
                        needs.Heal(selectedItem.item.consumables[x].value);
                        break;
                    case ConsumableType.Sleep:
                        needs.Sleep(selectedItem.item.consumables[x].value);
                        break;
                }
            }
        }

        RemoveSelectedItem();
    }

    public void OnEquipButton()
    {
        if (uiSlots[curEquipIndex].equipped)
            UnEquip(curEquipIndex);
        uiSlots[selectedItemIndex].equipped = true;
        curEquipIndex = selectedItemIndex;
        EquipManager.instance.EquipNew(selectedItem.item);

        UpdateUI();

        SelectItem(selectedItemIndex);
    }

    void UnEquip(int index)
    {
        uiSlots[index].equipped = false;
        EquipManager.instance.UnEquip();
        UpdateUI();

        if (selectedItemIndex == index)
            SelectItem(index);
    }

    public void OnUnEquipButton()
    {
        UnEquip(selectedItemIndex);
        EquipManager.instance.UnEquip();
    }

    public void OnDropButton()
    {
        ThrowItem(selectedItem.item);
        RemoveSelectedItem();
    }

    public void RemoveSelectedItem()
    {
        selectedItem.quantity--;
        if(selectedItem.quantity == 0)
        {
            if (uiSlots[selectedItemIndex].equipped == true)
                UnEquip(selectedItemIndex);

            selectedItem.item = null;
            ClearSelectedItemWindow();
        }

        UpdateUI();
    }

    public void RemoveItem(ItemData item)
    {

    }

    public bool HasItems(ItemData item, int quantity)
    {
        return false;
    }
}

public class ItemSlot
{
    public ItemData item;
    public int quantity;
}
