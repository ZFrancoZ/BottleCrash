using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento_Rampa : MonoBehaviour
{
    [SerializeField] private float Distancia;
    [SerializeField] private float Velocidad;
    private void Start()
    {
        LeanTween.moveX(gameObject, Distancia, Velocidad).setEaseInSine().setLoopPingPong();
    }
}
