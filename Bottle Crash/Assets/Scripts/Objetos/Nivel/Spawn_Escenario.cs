using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Escenario : MonoBehaviour
{
    public GameObject[] Escenario;
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        /*int random = Random.Range(1, Obstaculos.Length);
        while (random == GameManager.current.NivelActual[1])
        {
            random = Random.Range(1, Obstaculos.Length);
        }*/
        int nivel = GameManager.current.Nivel;
        Quaternion rotacionActual = Escenario[nivel].transform.rotation;
        Vector3 posicion = Escenario[nivel].transform.position;
        Instantiate(Escenario[nivel], posicion, rotacionActual);
        //GameManager.current.NivelActual[1] = random;
    }
}
