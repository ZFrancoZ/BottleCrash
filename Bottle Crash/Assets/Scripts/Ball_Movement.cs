using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Movement : MonoBehaviour
{
    private Rigidbody Rb;
    public float VelocidadRotacion;
    public float Velocidad_Movimiento;
    private int direccionRotacion = 1;
    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 movimiento =(Vector3.forward * Velocidad_Movimiento * Time.deltaTime);
        Rb.AddForce(movimiento);
    }
}
