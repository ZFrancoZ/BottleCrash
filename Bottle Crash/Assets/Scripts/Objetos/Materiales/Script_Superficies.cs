using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Superficies : MonoBehaviour
{
    [SerializeField] private Renderer rend;
    void Start()
    {
        Cambiar_Material();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Cambiar_Material()
    {
        rend.material = GameManager.current.MaterialSuperficie;
    }
}
