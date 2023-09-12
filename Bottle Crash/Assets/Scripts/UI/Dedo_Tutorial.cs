using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dedo_Tutorial : MonoBehaviour
{
    public Image Imagen;

    public Sprite[] SpriteArray;
    public float Velocidad;

    private int IndexSprite;
    Coroutine Anim;
    bool Termino;

    IEnumerator Play_Animacion()
    {
        yield return new WaitForSeconds(Velocidad);
        if(IndexSprite >= SpriteArray.Length)
        {
            IndexSprite = 0;
        }
        Imagen.sprite = SpriteArray[IndexSprite];
        IndexSprite += 1;
        if(Termino == false)
        {
            StartCoroutine(Play_Animacion());
        }
    }
    private void Start()
    {
        StartCoroutine(Play_Animacion());
        MoverDerecha();
    }
    public void MoverDerecha()
    {
        LeanTween.moveX(gameObject, 0.2f, 2f).setOnComplete(MoverIzquierda);
    }
    private void MoverIzquierda()
    {
        LeanTween.moveX(gameObject, -0.2f, 2f).setOnComplete(MoverDerecha);
    }
}
