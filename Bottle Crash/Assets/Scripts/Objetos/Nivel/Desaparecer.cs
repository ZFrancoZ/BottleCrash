using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desaparecer : MonoBehaviour
{
    private Rigidbody Rb;
    public bool BotellasEnMovimiento;
    public float explosionRadius = 5.0f;
    public float explosionForce = 1000.0f;
    public float upwardsModifier = 0.0f;
    void Start()
    {
        Controlador_UI.current.Sumar_Racha(1);
        /*Rb = GetComponent<Rigidbody>();
        // Encuentra todos los objetos dentro del radio de la explosión
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hit in colliders)
        {
            // Intenta obtener un componente Rigidbody del objeto
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Aplica una fuerza explosiva al Rigidbody
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier);
            }
        }*/
        Invoke("Destruir", 0.5f);
    }
    private void Destruir()
    {
        if (BotellasEnMovimiento)
        {
            Controlador_Botellas.current.BotellasMoviendose--;
        }
    }
}
