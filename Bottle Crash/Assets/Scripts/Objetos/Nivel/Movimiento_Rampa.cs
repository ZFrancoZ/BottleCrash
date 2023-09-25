using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento_Rampa : MonoBehaviour
{
    [SerializeField] private bool Mover;
    [SerializeField] private float Distancia;
    [SerializeField] private float Velocidad;
    private Ball_Movement Pelota;
    private void Start()
    {
        if(Mover)
        {
            LeanTween.moveX(gameObject, Distancia, Velocidad).setEaseInOutSine().setLoopPingPong();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Pelota"))
        {
            if(Pelota == null)
            {
                Pelota = other.GetComponent<Ball_Movement>();
            }
            Pelota.Cambiar_Color();
        }
    }
}
