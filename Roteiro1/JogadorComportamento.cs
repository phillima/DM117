using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JogadorComportamento : MonoBehaviour {
    /// <summary>
    /// Uma referencia para o componente RigidBody
    /// </summary>
    private Rigidbody rb;

    [Tooltip("A velocidade que a bola irá esquivar para os lados")]
    [Range(0,10)]
    public float velocidadeEsquiva = 5.0f;

    [Tooltip("Velocidade com qual a bola irá se deslocar para a frente")]
    [Range(0, 10)]
    public float velocidadeRolamento = 5.0f;

	// Use this for initialization
	void Start () {
        //Obter acesso ao componente RigidBody associado a esse GO
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        //Verificar para qual lado o jogador deseja esquivar
        var velocidadeHorizontal
            = Input.GetAxis("Horizontal") * velocidadeEsquiva;
        //Aplicar uma força para que a bola se desloque
        rb.AddForce(velocidadeHorizontal, 0, velocidadeRolamento);
    }
}
