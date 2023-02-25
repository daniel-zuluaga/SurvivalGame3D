using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public ItemData[] itemToGive;
    public int quantityPerHit;
    public int capacity;
    public GameObject hitParticle;

    public void Gather(Vector3 hitPoint, Vector3 hitNormal)
    {
        for (int i = 0; i < quantityPerHit; i++)
        {
            if (capacity <= 0)
                break;

            capacity -= 1;
            int indexItem = Random.Range(0, itemToGive.Length);
            Inventory.instanceInventory.AddItem(itemToGive[indexItem]);
        }

        Destroy(Instantiate(hitParticle, hitPoint, Quaternion.LookRotation(hitNormal, Vector3.up)), 1f);

        if(capacity <= 0)
        {
            Destroy(gameObject);
        }
    }
}
