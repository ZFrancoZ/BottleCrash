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
    [SerializeField] private GameObject Camara;
    private bool camaraActivada = true;
    [SerializeField] private Transform Pos_Inicial;

    [SerializeField] private GameObject Canvas_Menu;

    private Vector2 touchInicio;
    private bool Toca = false;
    public bool SeSolto = false;
    public bool PuedeTirar;
    [SerializeField] private bool Paso_Limites;
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
                ;
                Coll.isTrigger = false;
                //Coll.enabled = true;
                Canvas_Menu.SetActive(true);
                PuedeTirar = true;
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
                rb.AddForce(movimiento);
                if (rb.velocity.x <= Velocidad_Minima)
                {
                    //   Reiniciar_Pelota();
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Le agrega velocidad a la caida sobre la rampa
        if(other.tag == "Rampa" && SeSolto)
        {
            Velocidad_Movimiento = 11000;
        }
        //Hace que la pelota aumente su velocidad al saltar por el propulsor
        if (other.tag == "Propulsor")
        {
            Debug.Log("Propulsor");
            Velocidad_Movimiento = 90000;
        }
        //Desactiva la camara para que no siga la pelota
        if (other.CompareTag("Fin Camara"))
        {
            if (camaraActivada)
            {
                Debug.Log("Desactivar");
                Camara.SetActive(false);
                camaraActivada = false;
            }
            else
            {
                Debug.Log("Activar");
                Camara.SetActive(true);
                camaraActivada = true;
            }
        }
        if (other.tag == "Limite")
        {
            Reiniciar_Pelota();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //Hace que no se aplique ninguna fuerza sobre terreno liso
        if (other.tag == "Rampa")
        {
            Velocidad_Movimiento = 0;
        }

        if (other.tag == "Propulsor")
        {
            Velocidad_Movimiento = 0;
        }
    }

    public void Reiniciar_Pelota()
    {
        //Coll.enabled = false;
        Coll.isTrigger = true;
        rb.useGravity = false;
        SeSolto = false;
        Paso_Limites = true;
        //Camara.SetActive(true);
    }
    public void Soltar()
    {
        PuedeTirar = false;
        SeSolto = true;
        rb.useGravity = true;
    }
}
