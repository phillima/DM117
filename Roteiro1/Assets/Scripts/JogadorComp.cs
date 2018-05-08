using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe que define o comportamento do jogador
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class JogadorComp : MonoBehaviour {

    /// <summary>
    /// Referencia para o corpo rigido da bola
    /// </summary>
    private Rigidbody rb;

    [SerializeField]
    [Tooltip("Controla a velocidade de rolamento")]
    [Range(1,20)]
    private float velocidadeRolamento = 5.0f;

    [SerializeField]
    [Tooltip("Controla a velocidade de esquiva")]
    [Range(1, 20)]
    private float velocidadeEsquiva = 5.0f;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
  	}

    void Update() {
        float velocidadeHorizontal = Input.GetAxis("Horizontal") * velocidadeEsquiva;
        rb.AddForce(velocidadeHorizontal, 0, velocidadeRolamento);
    }


    
}
