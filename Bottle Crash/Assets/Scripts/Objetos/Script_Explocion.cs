using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Explocion : MonoBehaviour
{
    void Start()
    {
        LeanTween.scale(gameObject, new Vector3(24, 24, 24), 0.5f).setEaseOutSine().setOnComplete(() =>
        {
            LeanTween.scale(gameObject, new Vector3(0, 0, 0), 1f).setEaseOutSine().setOnComplete(() =>
            {
                Destroy(gameObject);
            });
        });
    }
    
}
