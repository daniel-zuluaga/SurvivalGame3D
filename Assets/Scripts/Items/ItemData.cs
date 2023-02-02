using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Resource,
    Equipable,
    Consumable
}

public enum ConsumableType
{
    Hunger,
    Thirst,
    Health,
    Sleep
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string nameItem;
    public string descriptionItem;
    public ItemType typeItem;
    public Sprite iconItem;
    public GameObject prefabItem;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;
}

[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}
