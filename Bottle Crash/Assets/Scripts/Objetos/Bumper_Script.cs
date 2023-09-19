using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper_Script : MonoBehaviour
{
    [SerializeField] private GameObject Objeto;
    private void Start()
    {
    }
    private void OnCollisionEnter(Collision collision)
    {
        Ball_Movement ball = collision.gameObject.GetComponent<Ball_Movement>();
        ball.Cambiar_Color();
        LeanTween.scale(Objeto, new Vector3(2.542865f, 2.542865f, 1.020462f), 0.1f).setEaseOutSine().setLoopPingPong(1);
    }
}
