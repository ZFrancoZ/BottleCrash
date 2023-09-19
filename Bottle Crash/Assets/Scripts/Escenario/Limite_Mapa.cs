using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limite_Mapa : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Pelota"))
        {
            LeanTween.color(gameObject, Color.red, 0.25f).setEaseOutSine().setLoopPingPong(2);

        }
    }
}
