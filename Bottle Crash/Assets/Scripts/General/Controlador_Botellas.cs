using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador_Botellas : MonoBehaviour
{
    public static Controlador_Botellas current;
    public int BotellasMoviendose;
    public float tiempoTranscurrido = 0f;
    private float tiempoEspera = 5f;
    [SerializeField] private Ball_Movement Pelota;
    private void Awake()
    {
        current = this;
    }
    void Update()
    {
        if(GameManager.current.HizoPrimerTiro)
        {
            if(Pelota.Desaparecio)
            {
                if(BotellasMoviendose > 0)
            {
                    tiempoTranscurrido += Time.deltaTime; // Sumar el tiempo transcurrido en cada frame

                    if (tiempoTranscurrido >= tiempoEspera)
                    {
                        Debug.Log("A");
                        Aparecer_Desaparecer();
                        // Han pasado 0.5 segundos, cambia el valor de BotellasMoviendose a false
                        tiempoTranscurrido = 0;
                    }
                }
            else
                {
                    if (tiempoTranscurrido > 0)
                    {
                        Debug.Log("B");
                        //Invoke("Aparecer_Pelota", 1);
                        Aparecer_Desaparecer();
                        tiempoTranscurrido = 0;
                    }
                }
            }
        }
    }

    void Aparecer_Desaparecer()
    {
        if (GameManager.current.ObjetosADestruir > GameManager.current.ObjetosDestruidos)
        {
            //if (Pelota.Desaparecio)
            //{
            Debug.Log("C");
                Pelota.Aparecer_Pelota();
            //}
        }
    }
}
