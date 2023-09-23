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
        switch(poder)
        {
            case 1:
                {
                    LeanTween.scale(gameObject, new Vector3(0.7f, 0.7f, 0.7f), 2).setEaseOutCubic().setLoopPingPong();
                }
                break;
            case 2:
                {
                    LeanTween.scale(gameObject, new Vector3(1.3f, 1.3f, 1.3f), 1).setEaseOutCubic().setLoopPingPong();
                }
                break;
            case 3:
                {
                    LeanTween.scale(gameObject, new Vector3(1.2f, 1.2f, 1.2f), 1).setEaseOutSine().setLoopPingPong();
                }
                break;
        }
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
