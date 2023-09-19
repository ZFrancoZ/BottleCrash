using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transportador_Script : MonoBehaviour
{
    [SerializeField] private bool PuedeTransportar;
    [SerializeField] private Transform Transporte;

    private void OnTriggerEnter(Collider other)
    {
        if(PuedeTransportar)
        {
            if (other.CompareTag("Pelota"))
            {
                LeanTween.scale(other.gameObject, new Vector3(0, 0, 0), 0.3f).setEaseOutSine().setOnComplete(()=>
                {
                    other.transform.position = Transporte.transform.position;
                    LeanTween.scale(other.gameObject, new Vector3(0.476217f, 0.476217f, 0.476217f), 1f).setEaseOutSine().setOnComplete(() =>
                    {
                        other.gameObject.GetComponent<Ball_Movement>().Seguir_Camino();
                    });
                });
            }
        }
    }
}
