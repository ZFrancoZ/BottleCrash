using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Obstaculos : MonoBehaviour
{
    public GameObject[] Obstaculos;
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        int random = Random.Range(1, Obstaculos.Length);
        while (random == GameManager.current.NivelActual[1])
        {
            random = Random.Range(1, Obstaculos.Length);
        }
        Quaternion rotacionActual = Obstaculos[random].transform.rotation;
        Vector3 posicion = Obstaculos[random].transform.position;
        Instantiate(Obstaculos[random], posicion, rotacionActual);
        GameManager.current.NivelActual[1] = random;
    }
}
