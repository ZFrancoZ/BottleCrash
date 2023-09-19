using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poderes : MonoBehaviour
{
    public int poder;
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scale(gameObject, new Vector3 (1.2f,1.2f,1.2f), 1).setEaseOutSine().setLoopPingPong();
    }
    public void Reaparecer()
    {
        switch(poder)
        {
            case 1:
                {
                    LeanTween.color(gameObject, Color.magenta, 1).setEaseOutSine();
                }
                break;
            case 2:
                {
                    LeanTween.color(gameObject, Color.blue, 1).setEaseOutSine();
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
