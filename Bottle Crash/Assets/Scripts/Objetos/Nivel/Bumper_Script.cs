using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper_Script : MonoBehaviour
{
    [SerializeField] private GameObject Objeto;
    [SerializeField] private Ball_Movement Ball;
    private void Start()
    {
        Ball = GameManager.current.Pelota;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Pelota"))
        {
            Ball.Cambiar_Color();
            LeanTween.scale(Objeto, new Vector3(2.542865f, 2.542865f, 1.020462f), 0.1f).setEaseOutSine().setLoopPingPong(1);
        }
    }
}
