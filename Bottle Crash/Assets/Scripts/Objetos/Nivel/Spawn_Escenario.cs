using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Escenario : MonoBehaviour
{
    public GameObject[] Escenario;
    void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        int nivel = GameManager.current.Nivel;
        int indiceEscenario = (nivel - 1) % Escenario.Length + 1; // Calcula el índice y suma 1
        Quaternion rotacionActual = Escenario[indiceEscenario - 1].transform.rotation; // Resta 1 para acceder al elemento correcto
        Vector3 posicion = Escenario[indiceEscenario - 1].transform.position; // Resta 1 para acceder al elemento correcto
        Instantiate(Escenario[indiceEscenario - 1], posicion, rotacionActual); // Resta 1 para acceder al elemento correcto
    }
}
