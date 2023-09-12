using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objeto_Nuevo : MonoBehaviour
{
    public GameObject Botella_Rota;
    private void Start()
    {
        GameManager.current.ObjetosADestruir++;
        GameManager.current.BarraProgreso.maxValue = GameManager.current.ObjetosADestruir;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Pelota")
        {
            Destruir();
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Suelo")
        {
            Destruir();
        }
    }
    private void Destruir()
    {
        Instantiate(Botella_Rota, transform.position, Quaternion.identity);
        GameManager.current.Sumar_Destruido();
        Destroy(this.gameObject);
    }
}
