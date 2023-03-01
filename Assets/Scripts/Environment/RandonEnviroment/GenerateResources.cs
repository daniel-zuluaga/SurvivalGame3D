using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateResources : MonoBehaviour
{
    public GameObject[] positionResources;
    public float rateGenerateResources;
    public GameObject[] resourcesPrefab;

    void Start()
    {
        for (int i = 0; i < positionResources.Length; i++)
        {
            int indexResources = Random.Range(0, resourcesPrefab.Length);
            Instantiate(resourcesPrefab[indexResources], positionResources[i].transform.position, resourcesPrefab[indexResources].transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < positionResources.Length; i++)
        {
            if (positionResources[i] == null)
            {
                return;
            }
            else
            {
                GenerateResourcesRandom();
            }
        }
    }

    IEnumerator GenerateResourcesRandom()
    {
        yield return new WaitForSeconds(rateGenerateResources);
        for (int i = 0; i < positionResources.Length; i++)
        {
            int indexResources = Random.Range(0, resourcesPrefab.Length);
            GameObject resourcesObj = Instantiate(resourcesPrefab[indexResources], positionResources[i].transform.position, resourcesPrefab[indexResources].transform.rotation);
            positionResources[i].GetComponent<PositionGenerate>().resourcesPrefab = resourcesObj;
        }
    }
}
