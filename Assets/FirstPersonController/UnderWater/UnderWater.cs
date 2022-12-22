using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderWater : MonoBehaviour
{
    public GameObject sistPartBurbujas, vidrioVFX;
    public AudioSource soundAmbient, soundUnderWater;
    public bool underWater;

    void Update()
    {
        if (underWater)
        {
            sistPartBurbujas.SetActive(true);
            RenderSettings.fog = true;
            RenderSettings.fogDensity = 0.2f;
            vidrioVFX.SetActive(true);
            soundAmbient.volume = 0.008f;
            soundUnderWater.volume = 1;
            vidrioVFX.GetComponent<AnimMaterial>().desplasarY = -1.0f;
        }
        else
        {
            sistPartBurbujas.SetActive(false);
            RenderSettings.fog = false;
            vidrioVFX.SetActive(false);
            soundAmbient.volume = 1;
            soundUnderWater.volume = 0;
        }
    }
}
