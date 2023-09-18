using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Movement : MonoBehaviour
{
    private Collider Coll;
    private Rigidbody rb;
    public float Velocidad_Movimiento;
    [SerializeField] private float Velocidad_Minima;
    [SerializeField] private float Velocidad_Colocación;
    private bool camaraActivada = true;
    [SerializeField] private Camara_Cinemachine CamaraCM;
    [SerializeField] private Transform Pos_Inicial;

    [SerializeField] private GameObject Canvas_Menu;

    [SerializeField] private TrailRenderer Trail;

    private Vector2 touchInicio;
    private bool Toca = false;
    public bool SeSolto = false;
    public bool PuedeTirar;
    [SerializeField] private bool Paso_Rampa;
    public bool Paso_Limites;
    [SerializeField] private float Velocidad_Reinicio;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Coll = GetComponent<Collider>();
    }
    private void FixedUpdate()
    {
        if(Paso_Limites)
        {
            if(Vector3.Distance(transform.position, Pos_Inicial.position) <0.3)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                Paso_Limites = false;
                Coll.isTrigger = false;
                Paso_Rampa = false;
                Canvas_Menu.SetActive(true);
                if(GameManager.current.ObjetosADestruir > GameManager.current.ObjetosDestruidos)
                {
                    PuedeTirar = true;
                }
            }
            else
            {
                float step = Velocidad_Reinicio * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, Pos_Inicial.position, step);
            }
        }
        if(!SeSolto && PuedeTirar)
        {
            if (Input.touchCount > 0)
            {
                Canvas_Menu.SetActive(false);
                Touch touch = Input.GetTouch(0);
                if (!Toca)
                {
                    Toca = true;
                    touchInicio = touch.position;
                }

                Vector2 deltaPos = touch.position - touchInicio;

                // Calcula la velocidad en función de la distancia horizontal (deltaPosition.x)
                float velocidadHorizontal = deltaPos.x * Velocidad_Colocación * Time.deltaTime;

                // Aplica la velocidad al objeto en el eje X
                Vector3 movimiento = new Vector3(velocidadHorizontal, 0, 0);
                rb.velocity = movimiento;
            }
            else
            {
                // Si no hay toques, establece la velocidad en cero en el eje X
                if(Toca)
                {
                    Toca = false;
                    Soltar();
                }
            }
        }
        else
        {
            if(!Paso_Limites)
            {
                Vector3 movimiento = (Vector3.forward * Velocidad_Movimiento * Time.deltaTime);
                //rb.AddForce(movimiento);
                if (rb.velocity.magnitude <= Velocidad_Minima && Paso_Rampa)
                {
                    StartCoroutine(Espera(1.5f));
                }
                else
                {
                    rb.AddForce(movimiento);
                }
            }
        }
    }
    IEnumerator Espera(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        Paso_Limites = true;
        Reiniciar_Pelota();
    }
    private void OnTriggerEnter(Collider other)
    {
        //Le agrega velocidad a la caida sobre la rampa
        if(other.CompareTag("Rampa") && SeSolto)
        {
            Velocidad_Movimiento = 11000;
        }
        //Hace que la pelota aumente su velocidad al saltar por el propulsor
        if (other.CompareTag("Propulsor"))
        {
            Debug.Log("Propulsor");
            Velocidad_Movimiento = 90000;
        }
        //Desactiva la camara para que no siga la pelota

        if (other.CompareTag("Fin Camara"))
        {
            if (camaraActivada && !Paso_Limites)
            {
                Debug.Log("Desactivar");
                //Camara.SetActive(false);
                CamaraCM.Movimiento_Lento();
                camaraActivada = false;
                //CamaraCM.Cambiar_Objetivo(2);
            }
            else
            {
                Debug.Log("Activar");
                //StartCoroutine(Activar_Seguimiento(0.4f));
                //Camara.SetActive(true);
                CamaraCM.Cambiar_Objetivo(0);
                camaraActivada = true;
            }
        }
        if (other.CompareTag("Limite"))
        {
            Reiniciar_Pelota();
        }
    }
    public void Cambiar_Color()
    {
        StartCoroutine(Color_Rastro(1));
    }
    public IEnumerator Color_Rastro(float tiempo)
    {

        Trail.startColor = Color.green;
        yield return new WaitForSeconds(tiempo);
        Trail.startColor = Color.white;
    }
    private void OnTriggerExit(Collider other)
    {
        //Hace que no se aplique ninguna fuerza sobre terreno liso
        if (other.CompareTag("Rampa"))
        {
            Velocidad_Movimiento = 0;
            if(!Paso_Limites)
            {
                Paso_Rampa = true;
            }
        }

        if (other.CompareTag("Propulsor"))
        {
            Velocidad_Movimiento = 0;
        }
    }

    public void Reiniciar_Pelota()
    {
        Trail.enabled = false;
        //Coll.enabled = false;
        Coll.isTrigger = true;
        rb.useGravity = false;
        SeSolto = false;
        Paso_Limites = true;
        //Camara.SetActive(true);
    }
    public void Soltar()
    {
        Trail.Clear();
        Trail.enabled = true;
        PuedeTirar = false;
        SeSolto = true;
        rb.useGravity = true;
    }
}
