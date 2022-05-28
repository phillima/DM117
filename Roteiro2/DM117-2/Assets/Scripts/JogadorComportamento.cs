using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

[RequireComponent(typeof(Rigidbody))]
public class JogadorComportamento : MonoBehaviour
{
    /// <summary>
    /// Referência para o componente RigidBody
    /// </summary>
    private Rigidbody rb;
    
    // ReSharper disable once StringLiteralTypo
    [Tooltip("Velocidade da reação da bola ao desviar dos obstaculos")]
    [Range(0, 10)]
    public float velocidadeEsquiva = 0.5f;
    
    [Tooltip("Velocidade do movimento da bola para frente")]
    [Range(0, 10)]
    public float velocidadeRolamento = 0.5f;
    // Start is called before the first frame update
    
    void Start()
    {
        // Obter acesso ao componente RigidBody associado a este GO
        rb = GetComponent<Rigidbody>(); 
    }
    // Update is called once per frame
    void Update()
    {
        // Verificar para qual lado o jogador deseja esquivar
        var velocidadeHorizontal = Input.GetAxis("Horizontal") * velocidadeEsquiva;

        // print("teste {}" + velocidadeHorizontal);
        
        // Aplicar uma força para que a bola se desloque 
        rb.AddForce(velocidadeHorizontal, 0, velocidadeRolamento);
    }
}