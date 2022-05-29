using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

// ReSharper disable All

public class ControleCamera : MonoBehaviour
{

    [Tooltip("O alvo a ser acompanhado pela camera")]
    public Transform alvo;

    [Tooltip("Offset da camera em relação ao alvo")]
    public Vector3 offset = new Vector3(0, 3, -6);
    
    
    // Start is called before the first frame update
    void Update()
    {
        Console.Out.WriteLine(alvo.position);
        if (alvo != null)
        {
            // Altera a posicao da camera
            transform.position = alvo.position + offset;
            
            // Altera a rotação da camera em relacao
            transform.LookAt(alvo);
        }
        
    }

    // // Update is called once per frame
    // void Update()
    // {
    //     
    // }
}