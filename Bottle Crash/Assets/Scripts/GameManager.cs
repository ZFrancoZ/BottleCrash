using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager current;

    public int[] NivelActual; // 0 = Objetos a destruir , 1 = Obstaculos
    public Camara_Cinemachine Camara;
    public int Nivel;
    public float ObjetosADestruir;
    public float ObjetosDestruidos;

    public static event Action FinPartida;

    public GameObject Canvas_Nivel;

    public Ball_Movement Pelota;

    public Slider BarraProgreso;
    public float VelocidadBarraProgreso;

    private void Awake()
    {
        current = this;
    }
    void Update()
    {
        if(BarraProgreso.value <= ObjetosDestruidos)
        {
            BarraProgreso.value += VelocidadBarraProgreso * Time.deltaTime;
        }
    }
    
    public void Sumar_Destruido()
    {
        ObjetosDestruidos++;
        if(ObjetosDestruidos == ObjetosADestruir)
        {
            Pelota.PuedeTirar = false;
            Nivel++;
            Invoke("Nivel_Completado", 2f);
        }
    }
    private void Nivel_Completado()
    {
        ObjetosADestruir = 0;
        ObjetosDestruidos = 0;
        Canvas_Nivel.SetActive(true);
        FinPartida.Invoke();
        Camara.Cambiar_Objetivo(1);
    }

    public void Habilitar_Pelota(float tiempo)
    {
        StartCoroutine(HabilitarPelota(tiempo));
    }
    public IEnumerator HabilitarPelota(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        Pelota.PuedeTirar = true;
    }
}
