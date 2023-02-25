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
        if (healthWeapon == 0)
        {
            Destroy(gameObject);
            Inventory.instanceInventory.RemoveSelectedItem();
            itemSlotUI.Clear();
            return;
        }
        healthWeapon -= hitItem;
        Debug.Log("Hit Detected");

        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, attackDistance))
        {
            if (doesGatherResources && hit.collider.GetComponent<Resource>())
            {
                hit.collider.GetComponent<Resource>().Gather(hit.point, hit.normal);
            }

            if(doesDealDamage && hit.collider.GetComponent<IDamagable>() != null)
            {
                hit.collider.GetComponent<IDamagable>().TakePhysicalDamage(damage);
            }
        }
    }
}
