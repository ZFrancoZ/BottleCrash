using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borrar : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.FinPartida += MetodoQueSeEjecuta;
    }
    public void MetodoQueSeEjecuta()
    {
        if (this != null)
        {
            Invoke("Destruir", 0);
        }
    }
    private void Destruir()
    {
        Destroy(gameObject);
    }
}
