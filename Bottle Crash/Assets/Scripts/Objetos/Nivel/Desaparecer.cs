using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desaparecer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destruir", 0.5f);
    }
    private void Destruir()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0),0.5f).setEaseOutSine().setOnComplete(() =>
            {
                Destroy(gameObject);
            });
    }
}
