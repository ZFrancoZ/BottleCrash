using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objeto_Nuevo : MonoBehaviour
{
    public GameObject Botella_Rota;
    private bool YaEntro;
    [SerializeField] private Renderer rend;
    private void Start()
    {
        rend.material = GameManager.current.MaterialBotella;
        GameManager.current.ObjetosADestruir++;
        GameManager.current.BarraProgreso.maxValue = GameManager.current.ObjetosADestruir;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Suelo") || other.CompareTag("Limite") || other.CompareTag("Pelota"))
        {
            Destruir();
        }
    }
    private void Destruir()
    {
        if(!YaEntro)
        {
            YaEntro = true;
            GameManager.current.Sumar_Destruido();
            GameObject objetoInstanciado = Instantiate(Botella_Rota, transform.position, Quaternion.identity);
            objetoInstanciado.transform.localScale = transform.localScale;
            Destroy(this.gameObject);
        }
    }
}
