using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objeto_Nuevo : MonoBehaviour
{
    public GameObject Botella_Rota;
    private bool YaEntro;
    [SerializeField] private Renderer rend;
    [SerializeField] private Rigidbody Rb;
    private bool BotellasEnMovimiento;
    private void Start()
    {
        rend.material = GameManager.current.MaterialBotella;
        GameManager.current.ObjetosADestruir++;
        Rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Suelo") || other.CompareTag("Limite") || other.CompareTag("Pelota"))
        {
            Destruir();
        }
    }
    private void FixedUpdate()
    {
        if (Rb.velocity.magnitude > 0.1f)
        {
            if (!BotellasEnMovimiento)
            {
                Controlador_Botellas.current.BotellasMoviendose++;
                BotellasEnMovimiento = true;
            }
        }
        else
        {
            if (BotellasEnMovimiento)
            {
                Controlador_Botellas.current.BotellasMoviendose--;
                BotellasEnMovimiento = false;
            }
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
            objetoInstanciado.GetComponent<Desaparecer>().BotellasEnMovimiento = BotellasEnMovimiento;
            Destroy(this.gameObject);
        }
    }
}
