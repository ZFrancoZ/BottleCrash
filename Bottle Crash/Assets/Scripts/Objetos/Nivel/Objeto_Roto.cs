using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Objeto_Roto : MonoBehaviour
{
    private Collider coll;
    [SerializeField] private Renderer rend;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        rend.material = GameManager.current.MaterialBotella;
        coll = GetComponent<Collider>();
        //Invoke("Desactivar_Collision", 0.5f);
    }
    public float shrinkSpeed = 1f;

    private void Update()
    {
        // Reducir la escala del objeto en cada fotograma
        Vector3 newScale = transform.localScale - new Vector3(shrinkSpeed * Time.deltaTime, shrinkSpeed * Time.deltaTime, shrinkSpeed * Time.deltaTime);

        // Asegurarse de que no se vuelva negativo
        newScale = Vector3.Max(newScale, Vector3.zero);

        // Asignar la nueva escala al objeto
        transform.localScale = newScale;

        // Si la escala es casi cero, destruir el objeto
        if (transform.localScale.magnitude < 0.01f)
        {
            Destroy(gameObject);
        }
    }
    private void Desactivar_Collision()
    {
        coll.enabled = false;
    }
}
