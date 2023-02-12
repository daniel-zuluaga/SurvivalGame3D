using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipTool : Equip
{
    public float attackRate;
    private bool attacking;
    public float attackDistance;
    public float healthWeapon;
    public float healthWeaponMax;

    [Header("Resource Gathering")]
    public bool doesGatherResources;

    [Header("Combat")]
    public bool doesDealDamage;
    public int damage;
    public int hitItem;

    [Header("Componets")]
    public Animator anim;
    public Camera cam;
    public ItemSlotUI itemSlotUI;

    private void Awake()
    {
        cam = Camera.main;
    }

    void Start()
    {
        healthWeapon = healthWeaponMax;
    }

    void Update()
    {
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        itemSlotUI.fillHealthItem.fillAmount = healthWeapon / healthWeaponMax;
        Inventory.instance.UpdateUI();
    }

    public override void OnAttackInput()
    {
        if (!attacking)
        {
            attacking = true;
            anim.SetTrigger("Attack");
            Invoke("OnCanAttack", attackRate);
        }
    }

    void OnCanAttack()
    {
        attacking = false;
    }

    public void OnHit()
    {
        if (this == null)
        {
            Debug.Log("Este objeto es nulo");
            return;
        }

        if (healthWeapon == 0)
        {
            Destroy(gameObject);
            Inventory.instance.RemoveSelectedItem();
            itemSlotUI.Clear();
            return;
        }
        else
        {
            healthWeapon -= hitItem;
            UpdateHealthBar();
            Debug.Log("Hit Detected");
        }
    }
}
