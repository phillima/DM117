using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControle : MonoBehaviour {

    [SerializeField]
    [Tooltip("Referencia para o jogodor")]
    Transform jogador;

    /// <summary>
    /// Offset de distancia entre a camera e o jogador.
    /// </summary>
    Vector3 offset;

	// Use this for initialization
	void Start () {
        //Calculo do offset
        offset = jogador.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (jogador) {
            transform.position = jogador.position - offset;
            transform.LookAt(jogador);
        }
	}
}
