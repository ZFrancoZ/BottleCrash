using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Movement : MonoBehaviour
{
    private Rigidbody Rb;
    public float Velocidad_Movimiento;
    [SerializeField] private float Velocidad_Colocación;
    [SerializeField] private GameObject Camara;

    private Vector2 touchInicio;
    private bool Toca = false;
    public bool SeSolto = false;
    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if(!SeSolto)
        {
            if (Input.touchCount > 0)
            {

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
                Rb.velocity = movimiento;
            }
            else
            {
                // Si no hay toques, establece la velocidad en cero en el eje X
                Toca = false;
                Rb.velocity = new Vector3(0, Rb.velocity.y, Rb.velocity.z);
            }
        }
        else
        {
            Vector3 movimiento = (Vector3.forward * Velocidad_Movimiento * Time.deltaTime);
            Rb.AddForce(movimiento);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Le agrega velocidad a la caida sobre la rampa
        if(other.tag == "Rampa")
        {
            Velocidad_Movimiento = 9000;
        }
        //Hace que la pelota aumente su velocidad al saltar por el propulsor
        if (other.tag == "Propulsor")
        {
            Debug.Log("Propulsor");
            Velocidad_Movimiento = 90000;
        }
        //Desactiva la camara para que no siga la pelota
        if(other.tag == "Fin" && Camara != null)
        {
            Camara.SetActive(false);
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
    public void Soltar()
    {
        SeSolto = true;
        Rb.useGravity = true;
    }
}
