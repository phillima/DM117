using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JogadorComportamento : MonoBehaviour {

    public enum TipoMovimentoHorizontal
    {
        Acelerometro,
        Touch
    }

    public TipoMovimentoHorizontal movimentoHorizontal =
        TipoMovimentoHorizontal.Acelerometro;
    
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

    [Header("Atributos responsaveis pelo swipe")]
    [Tooltip("Determina qual a distancia que o dedo do jogador " +
        "deve deslocar pela tela para ser " +
        "considerado um swipe")]
    public float minDisSwipe = 2.0f;
    
    [Tooltip("Distancia que a bola ira percorrer atraves do swipe")]
    public float swipeMove = 2.0f;

    /// <summary>
    /// Ponto inicial onde o swipe ocorreu
    /// </summary>
    private Vector2 toqueInicio; 
	
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

#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBPLAYER
        //Detectando se houve clique com o botao direito (opcao 0) ou toque na tela
        if (Input.GetMouseButton(0)) {
            velocidadeHorizontal = CalculaMovimento(Input.mousePosition);
        }
        //Verifica se estamos no iOS ou Android. Se não estivermos o codigo estara cinza
//#elif UNITY_IOS || UNITY_ANDROID

        if (movimentoHorizontal == TipoMovimentoHorizontal.Acelerometro)
            // Move a bola baseada na direcao do acelerometro
            velocidadeHorizontal = Input.acceleration.x * velocidadeRolamento;
        else {//Movimento por touch
            //Detectando exclusivamente via touch
            if (Input.touchCount > 0)
            {
                //Obtendo o primeiro touch na tela dentro do frame
                Touch toque = Input.touches[0];

                //Usando por touch
                velocidadeHorizontal = CalculaMovimento(toque.position);
                //Usando movimento por swipe
                //Lembrando que podemos ter o jogo pelo swipe e touch ao mesmo tempo
                SwipeTeleport(toque);

                //Verifica se esse toque atingiu algum objeto
                TocarObjetos(toque);
            }
        }
        
#endif
        //Aplicar uma força para que a bola se desloque
        rb.AddForce(velocidadeHorizontal, 0, velocidadeRolamento);
    }

    /// <summary>
    /// Metodo para realizar o swipe
    /// </summary>
    /// <param name="toque">O toque ocorrido na tela</param>
    private void SwipeTeleport(Touch toque)
    {
        //Verifica se esse eh o ponto onde o swipe comecou
        if (toque.phase == TouchPhase.Began)
            toqueInicio = toque.position;

        //Verifica se o swipe acabou
        else if(toque.phase == TouchPhase.Ended) {

            Vector2 toqueFim = toque.position;
            Vector3 direcaoMov;

            //Faz a diferenca entre o ponto final e inicial do swipe
            float dif = toqueFim.x - toqueInicio.x;

            //Verifica se o swipe percorreu uma distancia suficiente para
            //ser reconhecido como swipe
            if (dif >= Math.Abs(minDisSwipe))
            {
                //Determina a direcao do swipe
                if (dif < 0)
                    direcaoMov = Vector3.left;
                else
                    direcaoMov = Vector3.right;
            }
            else
                return;
            //Raycast eh outra forma de detectar colisao
            RaycastHit hit;

            //Vamos verificar se o swipe nao vai causar colisao
            if (!rb.SweepTest(direcaoMov, out hit,swipeMove))
                rb.MovePosition(rb.position + (direcaoMov * swipeMove));
        }
    }

    /// <summary>
    /// Metodo para calcular para onde o jogador se deslocara na horizontal
    /// </summary>
    /// <param name="screenSpaceCoord">As coordenadas no Screen Space</param>
    /// <returns></returns>
    private float CalculaMovimento(Vector2 screenSpaceCoord)
    {
        var pos = Camera.main.ScreenToViewportPoint(screenSpaceCoord);

        float direcaoX = 0;

        if (pos.x < 0.5)
            direcaoX = -1;
        else
            direcaoX = 1;
        return direcaoX * velocidadeEsquiva;
    }

    /// <summary>
    /// Metodo para identificar se objetos foram tocados
    /// </summary>
    /// <param name="toque">O toque ocorrido nesse frame</param>
    private static void TocarObjetos(Touch toque) {

        //Convertemos a posicao do toque (Screen Space) para um Ray
        Ray toqueRay = Camera.main.ScreenPointToRay(toque.position);

        //Objeto que ira salvar informacoes de um possivel objeto que foi tocado
        RaycastHit hit;

        if (Physics.Raycast(toqueRay, out hit)) 
            hit.transform.SendMessage("ObjetoTocado",
                SendMessageOptions.DontRequireReceiver);
    }
}
