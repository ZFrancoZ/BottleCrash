using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camara_Cinemachine : MonoBehaviour
{
    public CinemachineVirtualCamera CamaraVirtual;
    public Transform[] Objetivo;
    [SerializeField] private Rigidbody RbPelota;
    [SerializeField] private bool Detener; // Indica si la c�mara debe seguir moviendose
    [SerializeField] private float VelocidadZ; // Velocidad actual de la c�mara
    [SerializeField] private float VelocidadY;
    [SerializeField] private float TiempoParaDetenerse;
    [SerializeField] private float FactorDesaceleracion;
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
        /*if (Detener)
        {
            // Calcular la velocidad de desaceleraci�n basada en el tiempo restante para detenerse
            float velocidadDesaceleracion = VelocidadInicial / tiempoParaDetenerse;

            // Aplicar la desaceleraci�n utilizando el factor de desaceleraci�n
            VelocidadInicial -= velocidadDesaceleracion * Time.deltaTime * factorDesaceleracion;

            // Calcular la nueva posici�n en el eje Z
            float nuevaPosicionZ = transform.position.z + VelocidadInicial * Time.deltaTime;

            // Actualizar la posici�n en el eje Z
            transform.position = new Vector3(transform.position.x, transform.position.y, nuevaPosicionZ);

            tiempoTranscurrido += Time.deltaTime;

            // Si la velocidad es menor o igual a cero, detener el movimiento
            if (VelocidadInicial <= 0)
            {
                Detener = false;
                Debug.Log("Objeto se ha detenido.");
            }
        }*/
        /*if (Detener && CamaraVirtual.Follow == null)
        {
            // Calcular la velocidad de desaceleraci�n basada en el tiempo restante para detenerse
            float velocidadDesaceleracion = VelocidadInicial / TiempoParaDetenerse;

            // Aplicar la desaceleraci�n utilizando el factor de desaceleraci�n en el eje Z
            VelocidadInicial -= velocidadDesaceleracion * Time.deltaTime * FactorDesaceleracion;

            // Calcular la nueva posici�n en el eje Z
            float nuevaPosicionZ = transform.position.z + VelocidadInicial * Time.deltaTime;

            // Calcular la nueva posici�n en el eje X interpolando hacia 0
            float nuevaPosicionX = Mathf.Lerp(transform.position.x, 0, Time.deltaTime * FactorDesaceleracion);

            // Actualizar la posici�n en los ejes X y Z
            transform.position = new Vector3(nuevaPosicionX, transform.position.y, nuevaPosicionZ);

            //TiempoTranscurrido += Time.deltaTime;

            // Si la velocidad es menor o igual a cero, detener el movimiento
            if (VelocidadInicial <= 0)
            {
                Detener = false;
                Debug.Log("Objeto se ha detenido.");
            }
        }*/
        if (Detener && CamaraVirtual.Follow == null)
        {
            // Calcular la velocidad de desaceleraci�n basada en el tiempo restante para detenerse
            float velocidadDesaceleracion = VelocidadZ / TiempoParaDetenerse;
            float velocidadDesaceleracionY = VelocidadY / TiempoParaDetenerse;

            // Aplicar la desaceleraci�n utilizando el factor de desaceleraci�n en el eje Z
            VelocidadZ -= velocidadDesaceleracion * Time.deltaTime * FactorDesaceleracion;
            VelocidadY -= velocidadDesaceleracionY * Time.deltaTime * FactorDesaceleracion;

            // Calcular la nueva posici�n en el eje Z
            float nuevaPosicionZ = transform.position.z + VelocidadZ * Time.deltaTime;
            // Calcular la nueva posici�n en el eje Y
            float nuevaPosicionY = transform.position.y + VelocidadY * Time.deltaTime;

            // Calcular la nueva posici�n en el eje X interpolando hacia 0
            float nuevaPosicionX = Mathf.Lerp(transform.position.x, 0, Time.deltaTime * FactorDesaceleracion);

            // Actualizar la posici�n en los ejes X y Z
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
}
