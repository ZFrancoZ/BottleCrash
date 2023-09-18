using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material_Random : MonoBehaviour
{
    public Material[] Materiales;

    public Material Cambiar_Material()
    {
        int random = Random.Range(0, Materiales.Length);
        return Materiales[random];
    }
}
