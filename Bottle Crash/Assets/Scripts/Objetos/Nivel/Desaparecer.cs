using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desaparecer : MonoBehaviour
{
    public bool BotellasEnMovimiento;
    void Start()
    {
        Invoke("Destruir", 0.5f);
    }
    private void Destruir()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0),0.5f).setEaseOutSine().setOnComplete(() =>
            {
                if (BotellasEnMovimiento)
                {
                    Controlador_Botellas.current.BotellasMoviendose--;
                }
                Destroy(gameObject);
            });
    }
}
