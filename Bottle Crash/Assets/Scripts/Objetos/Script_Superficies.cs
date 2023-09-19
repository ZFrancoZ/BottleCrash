using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Superficies : MonoBehaviour
{
    [SerializeField] private Renderer rend;
    void Start()
    {
        rend.material = GameManager.current.MaterialSuperficie;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
