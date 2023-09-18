using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objeto_Roto : MonoBehaviour
{
    private Collider coll;
    [SerializeField] private Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = GameManager.current.MaterialBotella;
        coll = GetComponent<Collider>();
        Invoke("Desactivar_Collision", 1.5f);
    }

    private void Desactivar_Collision()
    {
        coll.enabled = false;
        Destroy(this);
    }
}
