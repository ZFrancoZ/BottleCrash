using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objeto_Roto : MonoBehaviour
{
    private Collider coll;
    void Start()
    {
        coll = GetComponent<Collider>();
        Invoke("Desactivar_Collision", 1.5f);
    }

    private void Desactivar_Collision()
    {
        coll.enabled = false;
        Destroy(this);
    }
}
