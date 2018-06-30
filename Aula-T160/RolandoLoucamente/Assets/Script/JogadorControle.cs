using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JogadorControle : MonoBehaviour {

    /// <summary>
    /// Referencia para a variavel rigidbody
    /// </summary>
    Rigidbody rb;

    [SerializeField]
    [Tooltip("A velocidade de deslocamento horizontal")]
    [Range(1, 20)]
    float velocidadeEsquiva;

    [Tooltip("A velocidade de deslocamento frontal")]
    [Range(1, 20)]
    [SerializeField]
    float velocidadeRolamento = 5.0f;

    [SerializeField]
    Vector3 posicaoInicial;
   
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
       /* posicaoInicial = transform.position;
        posicaoInicial.x = 2.0f;
        transform.position = posicaoInicial;*/
        
    }
	
	// Update is called once per frame
	void Update () {
        var direcaoHorizontal =
            Input.GetAxis("Horizontal");

        rb.AddForce(velocidadeEsquiva*direcaoHorizontal,
                    0,
                    velocidadeRolamento);
	}
}
