using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camara_Cinemachine : MonoBehaviour
{
    public CinemachineVirtualCamera CamaraVirtual;
    public Transform[] Objetivo;
    [SerializeField] private Rigidbody RbPelota;
    [SerializeField] private bool Detener; // Indica si la cámara debe seguir moviendose
    [SerializeField] private float VelocidadZ; // Velocidad actual de la cámara
    [SerializeField] private float VelocidadY;
    [SerializeField] private float TiempoParaDetenerse;
    [SerializeField] private float FactorDesaceleracion;
    public bool EfectoVelocidad;
    public bool EfectoTemblor;
    [SerializeField] private float TiempoEfectoVelocidad;

    [SerializeField] private float DuracionTemblor; // Duración en segundos del temblor
    public float AplitudTemblor; // Intensidad del temblor
    public float FrecuenciaTemblor; // Velocidad del temblor
    [SerializeField] private CinemachineBasicMultiChannelPerlin noise;

    private void Start()
    {
        noise = CamaraVirtual.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    public void Cambiar_Objetivo(int objetivo)
    {
        CamaraVirtual.Follow = Objetivo[objetivo];
    }
    public void Movimiento_Lento()
    {
        VelocidadZ = RbPelota.velocity.z;
        VelocidadY = RbPelota.velocity.y;
        CamaraVirtual.Follow = null;
        Detener = true;

    }
    private void Update()
    {
        Seguimiento_Camara();
        Efecto_Velocidad();
        Efecto_Temblor();
    }
    void Seguimiento_Camara()
    {
        if (Detener && CamaraVirtual.Follow == null)
        {
            // Calcular la velocidad de desaceleración basada en el tiempo restante para detenerse
            float velocidadDesaceleracion = VelocidadZ / TiempoParaDetenerse;
            float velocidadDesaceleracionY = VelocidadY / TiempoParaDetenerse;

            // Aplicar la desaceleración utilizando el factor de desaceleración en el eje Z
            VelocidadZ -= velocidadDesaceleracion * Time.deltaTime * FactorDesaceleracion;
            VelocidadY -= velocidadDesaceleracionY * Time.deltaTime * FactorDesaceleracion;

            // Calcular la nueva posición en el eje Z
            float nuevaPosicionZ = transform.position.z + VelocidadZ * Time.deltaTime;
            // Calcular la nueva posición en el eje Y
            float nuevaPosicionY = transform.position.y + VelocidadY * Time.deltaTime;

            // Calcular la nueva posición en el eje X interpolando hacia 0
            float nuevaPosicionX = Mathf.Lerp(transform.position.x, 0, Time.deltaTime * FactorDesaceleracion);

            // Actualizar la posición en los ejes X y Z
            transform.position = new Vector3(nuevaPosicionX, nuevaPosicionY, nuevaPosicionZ);

            //TiempoTranscurrido += Time.deltaTime;

            // Si la velocidad es menor o igual a cero, detener el movimiento
            if (VelocidadZ <= 0)
            {
                Detener = false;
                Debug.Log("Objeto se ha detenido.");
            }
        }
    }
    void Efecto_Velocidad()
    {
        if (EfectoVelocidad)
        {
            if (CamaraVirtual.m_Lens.FieldOfView < 120)
            {
                CamaraVirtual.m_Lens.FieldOfView += TiempoEfectoVelocidad * Time.deltaTime;
            }
            else
            {
                EfectoVelocidad = false;
            }
        }
        else
        {
            if (CamaraVirtual.m_Lens.FieldOfView > 105)
            {
                CamaraVirtual.m_Lens.FieldOfView -= TiempoEfectoVelocidad * Time.deltaTime;
            }
        }
    }
    void Efecto_Temblor()
    {
        if (EfectoTemblor)
        {
            // Calcula el factor de reducción en función del tiempo transcurrido
            float t = Mathf.Clamp01(1.0f - (DuracionTemblor / 2.0f)); // Ajusta el divisor para controlar la velocidad de reducción

            // Aplica el temblor con amplitud y frecuencia reducidas
            noise.m_AmplitudeGain = Mathf.Lerp(AplitudTemblor, 0f, t);
            noise.m_FrequencyGain = Mathf.Lerp(FrecuenciaTemblor, 0f, t);

            // Reduce la duración restante del temblor
            DuracionTemblor -= Time.deltaTime;

            // Si la duración ha terminado, detiene el temblor
            if (DuracionTemblor <= 0)
            {
                EfectoTemblor = false;
                noise.m_AmplitudeGain = 0f;
                noise.m_FrequencyGain = 0f;
                DuracionTemblor = 0.5f;
            }
        }
    }
}
