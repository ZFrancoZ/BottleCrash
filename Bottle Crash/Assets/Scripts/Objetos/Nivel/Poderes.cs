using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poderes : MonoBehaviour
{
    public int poder;
    private Color color;
    // Start is called before the first frame update
    void Start()
    {
        color = GetComponent<Renderer>().material.color;
        LeanTween.scale(gameObject, new Vector3 (1.2f,1.2f,1.2f), 1).setEaseOutSine().setLoopPingPong();
    }
    public void Reaparecer()
    {
        LeanTween.color(gameObject, color, 1).setEaseOutSine();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
