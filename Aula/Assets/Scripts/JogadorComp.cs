using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JogadorComp : MonoBehaviour {

    [SerializeField]
    [Tooltip("A velocidade qua a bola ira esquivar")]
    [Range(1,10)]
    private float velocidadeEsquiva = 5.0f;

    [SerializeField]
    [Tooltip("Velocidade com qual a bola se desloca para a frente")]
    [Range(1,10)]
    private float velocidadeRolamento = 5.0f;

    /// <summary>
    /// Uma referencia para o corpo rigido
    /// </summary>
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        var velocidadeHorizontal = 
                Input.GetAxis("Horizontal") 
                * velocidadeEsquiva;

#if UNITY_STANDALONE
        //Detectando se houve clique com o botao
        if (Input.GetMouseButton(0)) 
            velocidadeHorizontal = CalculaMovimento(Input.mousePosition);
#elif UNITY_IOS || UNITY_ANDROID

        //Detectando se clique com o touch
        if(Input.touchCount > 0) {
            //Obtendo o primeiro touch
            Touch toque = Input.touches[0];
            velocidadeHorizontal = CalculaMovimento(toque.position);
        }
#endif
        rb.AddForce(velocidadeHorizontal, 0,
            velocidadeRolamento);

    }

    /// <summary>
    /// Metodo para calcular a velocidade 
    /// </summary>
    /// <param name="posScreenSpace">As coordenadas no Screen Space (pixel)</param>
    /// <returns></returns>
    private float CalculaMovimento(Vector2 posScreenSpace) {

        var pos = Camera.main.ScreenToViewportPoint(posScreenSpace);
        float direcaoX;

        if (pos.x < 0.5)
            direcaoX = -1;
        else 
            direcaoX = +1;

        return direcaoX * velocidadeEsquiva;

    }
}
