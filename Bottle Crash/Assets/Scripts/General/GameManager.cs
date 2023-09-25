using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager current;

    public Camara_Cinemachine Camara;
    public int Nivel;
    public float ObjetosADestruir;
    public float ObjetosDestruidos;

    public Material MaterialBotella;
    public Material MaterialSuperficie;
    public Material_Random MatRandom;
    
    public static event Action FinPartida;

    public GameObject Canvas_Nivel;

    public Ball_Movement Pelota;

    public float VelocidadBarraProgreso;
    public Image BarraProgresoNivel;

    private void Awake()
    {
        current = this;
        Material();
    }
    void Update()
    {
        Barra_Progreso();
    }
    public void Barra_Progreso()
    {
        float progreso = ObjetosDestruidos / ObjetosADestruir;
        if (BarraProgresoNivel.fillAmount < progreso)
        {
            BarraProgresoNivel.fillAmount += VelocidadBarraProgreso * Time.deltaTime;
        }
    }
    public void Material()
    {
        MaterialBotella = MatRandom.Cambiar_Material_Botella();
        MaterialSuperficie = MatRandom.Cambiar_Material_Superficie();
        BarraProgresoNivel.color = MaterialBotella.color;
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
        BarraProgresoNivel.fillAmount = 0;
        Canvas_Nivel.SetActive(true);
        FinPartida.Invoke();
        Material();
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
