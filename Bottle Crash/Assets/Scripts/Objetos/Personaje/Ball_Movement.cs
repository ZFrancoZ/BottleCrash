using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Movement : MonoBehaviour
{
    private Collider Coll;
    private Rigidbody rb;
    public float Velocidad_Movimiento;
    public float AlturaSalto;
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
    public bool EfectoVelocidad;
    public bool Explota;
    [SerializeField] private GameObject Explocion;
    [SerializeField] private float TiempoEfecto;
    [SerializeField] private float Velocidad_Reinicio;
    [SerializeField] private bool Teletransportador;
    [SerializeField] private Vector3 VelGuardada;
    [SerializeField] private Vector3 VelAngularGuardada;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Coll = GetComponent<Collider>();
    }
    private void Update()
    {
        if (EfectoVelocidad)
        {
            if (CamaraCM.CamaraVirtual.m_Lens.FieldOfView < 120)
            {
                CamaraCM.CamaraVirtual.m_Lens.FieldOfView += TiempoEfecto * Time.deltaTime;
            }
            else
            {
                EfectoVelocidad = false;
            }
        }
        else
        {
            if (CamaraCM.CamaraVirtual.m_Lens.FieldOfView > 105)
            {
                CamaraCM.CamaraVirtual.m_Lens.FieldOfView -= TiempoEfecto * Time.deltaTime;
            }
        }
    }
    private void FixedUpdate()
    {
        if (!SeSolto && PuedeTirar)
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
                if (Toca)
                {
                    Toca = false;
                    Soltar();
                }
            }
        }
        else
        {
            if (!Paso_Limites)
            {
                Vector3 movimiento = (Vector3.forward * Velocidad_Movimiento * Time.deltaTime);
                //rb.AddForce(movimiento);
                if (rb.velocity.magnitude <= Velocidad_Minima && Paso_Rampa && !Teletransportador)
                {
                    Paso_Limites = true;
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
        Reiniciar_Pelota();
    }
    private void OnTriggerEnter(Collider other)
    {

        //Le agrega velocidad a la caida sobre la rampa
        if (other.CompareTag("Rampa") && SeSolto)
        {
            Velocidad_Movimiento = 11000;
        }
        //Le agrega velocidad a la caida sobre la rampa
        if (other.CompareTag("Trampolin"))
        {
            rb.velocity = new Vector3(rb.velocity.x, AlturaSalto, rb.velocity.z);
        }
        if(other.CompareTag("Transportador"))
        {
            Teletransportador = true;
            VelGuardada = rb.velocity;
            VelAngularGuardada = rb.angularVelocity;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        //Hace que la pelota aumente su velocidad al saltar por el propulsor
        if (other.CompareTag("Propulsor"))
        {
            EfectoVelocidad = true;
            Debug.Log("Propulsor");
            Velocidad_Movimiento = 90000;
        }
        //Desactiva la camara para que no siga la pelota

        if (other.CompareTag("Fin Camara"))
        {
            if (camaraActivada && !Paso_Limites)
            {
                Debug.Log("Desactivar");
                CamaraCM.Movimiento_Lento();
                camaraActivada = false;
            }
        }
        if (other.CompareTag("Limite"))
        {
            Reiniciar_Pelota();
        }
        if(other.CompareTag("Poder"))
        {
            Poderes power = other.GetComponent<Poderes>();
            int poder = power.poder;
            LeanTween.color(power.gameObject, Color.clear, 0.5f ).setEaseOutSine().setOnComplete(()=>
            {
                power.Reaparecer();
            });
            switch (poder)
            {
                case 1:
                    {
                        StartCoroutine(Color_Rastro(1, Color.magenta));
                        LeanTween.scale(gameObject, new Vector3(0.2f, 0.2f, 0.2f), 0.4f).setEaseOutSine();
                    }
                    break;
                case 2:
                    {
                        StartCoroutine(Color_Rastro(1, Color.blue));
                        LeanTween.scale(gameObject, new Vector3(1, 1, 1), 0.4f).setEaseOutSine();
                    }
                    break;
                case 3:
                    {
                        Explota = true;
                        StartCoroutine(Color_Rastro(1, Color.yellow));
                    }
                    break;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(Explota)
        {
            Instantiate(Explocion, transform.position, Quaternion.identity);
            Reiniciar_Pelota();
        }
    }
    public void Cambiar_Color()
    {
        StartCoroutine(Color_Rastro(1, Color.green));
    }
    public IEnumerator Color_Rastro(float tiempo,Color color)
    {

        Trail.startColor = color;
        yield return new WaitForSeconds(tiempo);
        Trail.startColor = Color.white;
    }
    public IEnumerator Color_Rastro_Poder(float tiempo, Color color)
    {

        Trail.startColor = color;
        yield return new WaitForSeconds(tiempo);
        Trail.startColor = Color.white;
    }
    private void OnTriggerExit(Collider other)
    {
        //Hace que no se aplique ninguna fuerza sobre terreno liso
        if (other.CompareTag("Rampa"))
        {
            Velocidad_Movimiento = 0;
            if (!Paso_Limites)
            {
                Paso_Rampa = true;
            }
        }

        if (other.CompareTag("Propulsor"))
        {
            Velocidad_Movimiento = 0;
        }
    }
    public void Seguir_Camino()
    {
        rb.velocity = VelGuardada;
        rb.angularVelocity = VelAngularGuardada;
    }
    public void Reiniciar_Pelota()
    {
        Explota = false;
        Trail.enabled = false;
        Coll.isTrigger = true;
        rb.useGravity = false;
        SeSolto = false;
        Paso_Limites = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.5f).setEaseOutSine().setOnComplete(() =>
        {
            transform.position = new Vector3(0, 4.046f, -6.23f);
            Invoke("Reponer_Pelota", (0f));
        });
    }
    public void Reponer_Pelota()
    {
        CamaraCM.Cambiar_Objetivo(0);
        camaraActivada = true;
        LeanTween.scale(gameObject, new Vector3(0.476217f, 0.476217f, 0.476217f), 1.5f).setEaseOutSine().setOnComplete(() =>
        {
            Paso_Limites = false;
            Coll.isTrigger = false;
            Paso_Rampa = false;
            Canvas_Menu.SetActive(true);
            if (GameManager.current.ObjetosADestruir > GameManager.current.ObjetosDestruidos)
            {
                PuedeTirar = true;
            }
        });
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
