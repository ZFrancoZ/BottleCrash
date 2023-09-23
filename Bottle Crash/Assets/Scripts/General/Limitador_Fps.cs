using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Limitador_Fps : MonoBehaviour
{

    public TextMeshProUGUI textMesh;
    [SerializeField] int fpsLimit;

    private void Awake()
    {
        Application.targetFrameRate = fpsLimit;
    }
    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        InvokeRepeating(nameof(Calcular), 0, 1f);
    }

    public void Calcular()
    {

        textMesh.text ="FPS:"+ Mathf.Ceil(1f / Time.deltaTime).ToString();
    }
}

