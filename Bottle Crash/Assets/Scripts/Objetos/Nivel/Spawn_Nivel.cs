using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Nivel : MonoBehaviour
{
    public GameObject[] Objetivos;
    // Start is called before the first frame update
    void Start()
    {
        Spawn();   
    }

    public void Spawn()
    {
        /*int random = Random.Range(1, Objetivos.Length);
        while (random == GameManager.current.NivelActual[0])
        {
            random = Random.Range(1, Objetivos.Length);
        }
        Instantiate(Objetivos[random], transform.position, Quaternion.identity);
        GameManager.current.NivelActual[0] = random;*/
    }
}
