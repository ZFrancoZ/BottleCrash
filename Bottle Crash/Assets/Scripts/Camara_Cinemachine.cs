using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camara_Cinemachine : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        if (virtualCamera != null)
        {
            // Desactiva la rotación en el eje Y
            virtualCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>().m_XDamping = 0;
        }
    }
}
