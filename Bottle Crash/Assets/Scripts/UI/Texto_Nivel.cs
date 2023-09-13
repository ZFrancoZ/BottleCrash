using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Texto_Nivel : MonoBehaviour
{
    [SerializeField] private TMP_Text TxtNivel; 
    // Start is called before the first frame update
    void Start()
    {
        Cambiar_Texto();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Cambiar_Texto()
    {
        TxtNivel.text ="Nivel " + GameManager.current.Nivel.ToString();
        LeanTween.scale(gameObject,new Vector3(1.5f,1.5f,1), 1).setEaseOutSine().setLoopPingPong(1);
    }
}
