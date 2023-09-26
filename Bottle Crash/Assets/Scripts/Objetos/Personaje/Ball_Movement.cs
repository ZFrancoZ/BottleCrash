using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Movement : MonoBehaviour
{
    private Collider Coll;
    private Rigidbody rb;
    public float Velocidad_Movimiento;
    public float AlturaSalto;
    public int CantidadDeTiros;
    [SerializeField] private float Velocidad_Minima;
    [SerializeField] private float Velocidad_Colocación;
    private bool camaraActivada = true;
    [SerializeField] private Camara_Cinemachine CamaraCM;
    [SerializeField] private Transform Pos_Inicial;

    [SerializeField] private GameObject Canvas_Menu;

    [SerializeField] private TrailRenderer Trail;

    //private Vector2 touchInicio;
    private float touchInicio;
    [SerializeField] private bool Toca = false;
    public bool PuedeTirar;
    public bool EstaLaPelota;
    [SerializeField] private bool Paso_Rampa;
    public bool EfectoVelocidad;
    public bool Explota;
    public bool Desaparecio;
    [SerializeField] private GameObject Explocion;
    [SerializeField] private float TiempoEfecto;
    [SerializeField] private bool Teletransportador;
    [SerializeField] private Vector3 VelGuardada;
    [SerializeField] private Vector3 VelAngularGuardada;
    [SerializeField] GameObject Particulas;
    [SerializeField] private bool PrimerToqueSuelo;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Coll = GetComponent<Collider>();
        Aparecer_Pelota();
    }

    private void FixedUpdate()
    {
        if (PuedeTirar)
        {
            if (Input.touchCount > 0)
            {
                Canvas_Menu.SetActive(false);
                Touch touch = Input.GetTouch(0);

                if (!Toca)
                {
                    Toca = true;
                    touchInicio = touch.position.x; // Guarda solo la posición en el eje X
                }

                float deltaPosX = touch.position.x - touchInicio;

                // Define la distancia máxima permitida antes de que el objeto se mueva en el eje X
                float distanciaMaximaPermitidaX = 50.0f; // Ajusta este valor según tus necesidades

                if (Mathf.Abs(deltaPosX) <= distanciaMaximaPermitidaX)
                {
                    // No aplicamos velocidad en el eje X si no nos hemos movido más allá de la distancia máxima permitida
                    rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
                }
                else
                {
                    // Calcula la velocidad en función de la distancia horizontal (deltaPosX)
                    float velocidadHorizontal = deltaPosX * Velocidad_Colocación * Time.deltaTime;

                    // Aplica la velocidad al objeto solo en el eje X
                    rb.velocity = new Vector3(velocidadHorizontal, rb.velocity.y, rb.velocity.z);
                }
            }
            else
            {
                if (Toca)
                {
                    Toca = false;
                    Soltar();
                }
            }

        }
        else
        {

            Vector3 movimiento = (Vector3.forward * Velocidad_Movimiento * Time.deltaTime);
            if (rb.velocity.magnitude <= Velocidad_Minima && Paso_Rampa && !Teletransportador)
            {
                Invoke("Desaparecer_Pelota", 0.2f);
                //Desaparecer_Pelota();
            }
            else
            {
                rb.AddForce(movimiento);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        //Le agrega velocidad a la caida sobre la rampa
        if (other.CompareTag("Rampa") /*&& SeSolto*/)
        {
            Velocidad_Movimiento = 11000;
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
            CamaraCM.EfectoVelocidad = true;
            Velocidad_Movimiento = other.GetComponent<Movimiento_Rampa>().Impulso;
        }
        //Desactiva la camara para que no siga la pelota
        if (other.CompareTag("Fin Camara"))
        {
            if (camaraActivada)
            {
                CamaraCM.Movimiento_Lento();
                camaraActivada = false;
            }
        }
        if (other.CompareTag("Limite") || other.CompareTag("Limite_Final"))
        {
            Desaparecer_Pelota();
        }
        if(other.CompareTag("Poder"))
        {
            Poderes power = other.GetComponent<Poderes>();
            int poder = power.poder;
            LeanTween.color(power.gameObject, new Color (0,0,0,0), 0.6f ).setEaseOutSine().setOnComplete(()=>
            {
                power.Reaparecer();
            });
            switch (poder)
            {
                case 1:
                    {
                        StartCoroutine(Color_Rastro(1, Color.magenta));
                        LeanTween.scale(gameObject, new Vector3(0.15f, 0.15f, 0.15f), 0.2f).setEaseOutSine();
                    }
                    break;
                case 2:
                    {
                        StartCoroutine(Color_Rastro(1, Color.blue));
                        LeanTween.scale(gameObject, new Vector3(1, 1, 1), 0.2f).setEaseOutSine();
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
            Instantiate(Explocion, transform.position, Quaternion.Euler (-90,0,0));
            CamaraCM.AplitudTemblor = 10;
            CamaraCM.EfectoTemblor = true;
            Explota = false;
            Coll.isTrigger = true;
            Desaparecer_Pelota();
        }
        if (collision.collider.CompareTag("Botella_Grande"))
        {
            CamaraCM.AplitudTemblor = 4;
            CamaraCM.EfectoTemblor = true;
        }
        if(collision.collider.CompareTag("Suelo"))
        {
            if(PrimerToqueSuelo)
            {
                Particulas.transform.position = this.gameObject.transform.position;
                Particulas.GetComponent<ParticleSystem>().Play();
            }
            else
            {
                PrimerToqueSuelo = true;
            }
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
            Paso_Rampa = true;
        }

        if (other.CompareTag("Propulsor"))
        {
            Velocidad_Movimiento = 0;
        }
    }
    public void Seguir_Camino()
    {
        if(GameManager.current.ObjetosDestruidos < GameManager.current.ObjetosADestruir)
        {
            rb.velocity = VelGuardada;
            rb.angularVelocity = VelAngularGuardada;
        }
        Teletransportador = false;
    }
    public void Desaparecer_Pelota()
    {
        if(!Desaparecio)
        {
            Desaparecio = true;
            Debug.Log("Desaparecer");
            Trail.enabled = false;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.5f).setEaseOutSine().setOnComplete(() =>
            {
                transform.position = new Vector3(0, 4.046f, -6.23f);
                if (Controlador_Botellas.current.BotellasMoviendose < 1)
                {
                    if(GameManager.current.ObjetosADestruir > GameManager.current.ObjetosDestruidos)
                    {
                        Aparecer_Pelota();
                    }
                }
            });
        }
    }
    public void Aparecer_Pelota()
    {
        if (GameManager.current.ObjetosADestruir > GameManager.current.ObjetosDestruidos || GameManager.current.ObjetosADestruir == 0)
        {
            if (!EstaLaPelota)
            {
                Debug.Log("Aparecer");
                EstaLaPelota = true;
                CamaraCM.Cambiar_Objetivo(0);
                camaraActivada = true;
                Coll.isTrigger = false;
                Paso_Rampa = false;
                Controlador_UI.current.Sumar_Racha(0);
                Controlador_Botellas.current.tiempoDeError = 0;
                LeanTween.scale(gameObject, new Vector3(0.5f, 0.5f, 0.5f), 1f).setEaseOutSine().setOnComplete(() =>
                {
                    Canvas_Menu.SetActive(true);
                    PuedeTirar = true;
                    Desaparecio = false;
                    PrimerToqueSuelo = false;
                });
            }
        }
    }
    public void Soltar()
    {
        CantidadDeTiros++;
        PuedeTirar = false;
        if (!GameManager.current.HizoPrimerTiro)
        {
            GameManager.current.HizoPrimerTiro = true;
        }
        EstaLaPelota = false;
        Trail.Clear();
        Trail.enabled = true;
        rb.useGravity = true;
    }
}
