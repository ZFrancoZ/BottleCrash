using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camara_Cinemachine : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Transform [] Objetivo;

    private void Start()
    {
    }

    public void Cambiar_Objetivo(int objetivo)
    {
        Debug.Log(objetivo);
        virtualCamera.Follow = Objetivo[objetivo];
    }
}
