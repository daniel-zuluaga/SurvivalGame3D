using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Resource,
    Equipable,
    Consumable
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
}
