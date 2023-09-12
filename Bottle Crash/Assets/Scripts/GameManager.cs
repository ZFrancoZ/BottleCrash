using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager current;

    public int Nivel;
    public float ObjetosADestruir;
    public float ObjetosDestruidos;

    public GameObject canvas;

    public Slider BarraProgreso;
    public float Velocidad;
    private void Awake()
    {
        current = this;
    }
    private void Start()
    {
    
    }
    void Update()
    {
        if(BarraProgreso.value <= ObjetosDestruidos)
        {
            BarraProgreso.value += Velocidad * Time.deltaTime;
        }
    }
    
    public void Sumar_Destruido()
    {
        ObjetosDestruidos++;
        if(ObjetosDestruidos == ObjetosADestruir)
        {
            Invoke("Nivel_Completado", 0.4f);
        }
    }
    private void Nivel_Completado()
    {
        canvas.SetActive(true);
    }

}
