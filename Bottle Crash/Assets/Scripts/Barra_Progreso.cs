using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barra_Progreso : MonoBehaviour
{

    public Image BarraProgreso;

    public float ProgresoActual;

    public float ProgresoMaximo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(BarraProgreso.fillAmount != ProgresoActual / ProgresoMaximo)
        {
            BarraProgreso.fillAmount += 0.1f;
        }
    }
}
