using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Test : MonoBehaviour
{

    public TextMeshProUGUI textMesh;
    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        InvokeRepeating(nameof(Calcular), 0, 1f);
    }

    public void Calcular()
    {

        textMesh.text = (1f / Time.deltaTime).ToString();
    }
}

