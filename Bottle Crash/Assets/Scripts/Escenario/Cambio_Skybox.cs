using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cambio_Skybox : MonoBehaviour
{
    [SerializeField] private Material [] Skybox;

    private void Start()
    {
        Cambio();
        GameManager.FinPartida += Cambio;
    }
    public void Cambio()
    {
        int random = Random.Range(0, Skybox.Length);
        RenderSettings.skybox = Skybox[random];
    }
}
