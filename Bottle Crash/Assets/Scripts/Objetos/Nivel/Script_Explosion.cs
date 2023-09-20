using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Explosion : MonoBehaviour
{
    void Start()
    {
        LeanTween.scale(gameObject, new Vector3(15, 15, 10), 0.5f).setEaseOutSine().setOnComplete(() =>
        {
            LeanTween.scale(gameObject, new Vector3(0, 0, 0), 1f).setEaseOutSine().setOnComplete(() =>
            {
                Destroy(gameObject);
            });
        });
    }
    
}
