using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Controlador_UI : MonoBehaviour
{
    public static Controlador_UI current;
    public TMP_Text TxtNivel;
    public TMP_Text TxtRacha;
    public int Racha;

    private void Awake()
    {
        current = this;
    }
    void Start()
    {
        Cambiar_Numero_Nivel();
    }

    public void Cambiar_Numero_Nivel()
    {
        TxtNivel.text = "Nivel " + GameManager.current.Nivel.ToString();
        LeanTween.scale(TxtNivel.gameObject, new Vector3(1.5f, 1.5f, 1), 0.5f).setEaseInOutBack().setLoopPingPong(1);
    }

    public void Sumar_Racha(int racha)
    {
        if(racha == 0)
        {
            Racha = 0;
            LeanTween.scale(TxtRacha.gameObject, new Vector3(0, 0, 0), 0.2f).setEaseInOutBack().setOnComplete(()=>
            {
                TxtRacha.text = "";
                TxtRacha.gameObject.transform.localScale = new Vector3(1, 1, 1);
            });
        }
        else
        {
            Racha += racha;
            TxtRacha.text = "X" + Racha.ToString();
            if (TxtRacha.gameObject.transform.localScale.x < 2.5f &&
                TxtRacha.gameObject.transform.localScale.y < 2.5f)
            {
                TxtRacha.gameObject.transform.localScale = new Vector3(TxtRacha.transform.localScale.x +0.05f, TxtRacha.transform.localScale.y + 0.05f,1);
            }
            Color colorAleatorio = new Color(Random.value, Random.value, Random.value);

            // Usar LeanTween para animar el color gradualmente
            LeanTween.value(TxtRacha.gameObject, TxtRacha.color, colorAleatorio, 0.2f)
                .setOnUpdate((Color updatedColor) =>
                {
                // Actualizar el color en cada fotograma
                TxtRacha.color = updatedColor;
                })
                .setEase(LeanTweenType.easeOutSine);
        }
    }
}
