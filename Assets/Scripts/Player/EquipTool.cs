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
    public int healthWeaponMax;

    [Header("Resource Gathering")]
    public bool doesGatherResources;

    [Header("Combat")]
    public bool doesDealDamage;
    public int damage;
    public int hitItem;

    [Header("Componets")]
    public Animator anim;
    public Camera cam;
    public Image fillAmountHealth;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        healthWeapon = healthWeaponMax;
    }

    private void Update()
    {
        fillAmountHealth.fillAmount = healthWeapon / healthWeaponMax;
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
        attacking = true;
    }

    public void OnHit()
    {
        Debug.Log("Hit Detected");
        healthWeapon -= hitItem;

        if(healthWeapon == 0)
        {
            Destroy(gameObject);
        }
    }
}
