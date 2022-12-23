using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimMaterial : MonoBehaviour
{
    //Variables
    [Header("Movimiento")]
    public float desplasarX = 0.1f;
    public float desplasarY = 0.1f;

    void Update()
    {
        float offsetX = Time.time * desplasarX / 100;
        float offsetY = Time.time * desplasarY / 100; 
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}
