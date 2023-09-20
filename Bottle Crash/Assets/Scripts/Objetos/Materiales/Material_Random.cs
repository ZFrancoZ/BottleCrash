using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material_Random : MonoBehaviour
{
    public Material[] MaterialesBotellas;
    public Material[] Materiales_Superficies;
    public int random;
    public Material Cambiar_Material_Botella()
    {
        random = Random.Range(0, MaterialesBotellas.Length);
        return MaterialesBotellas[random];
    }
    public Material Cambiar_Material_Superficie()
    {
        return Materiales_Superficies[random];
    }

}
