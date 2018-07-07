using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JogadorControle : MonoBehaviour {

    public enum TipoMovimentoHorizontal {
        Acelerometro,
        Touch
    }

    public TipoMovimentoHorizontal movimentoHorizontal =
        TipoMovimentoHorizontal.Touch;

    /// <summary>
    /// Referencia para a variavel rigidbody
    /// </summary>
    Rigidbody jogadorRB;

    [SerializeField]
    [Tooltip("A velocidade de deslocamento horizontal")]
    [Range(1, 20)]
    float velocidadeEsquiva;

    [Tooltip("A velocidade de deslocamento frontal")]
    [Range(1, 20)]
    [SerializeField]
    float velocidadeRolamento = 5.0f;

    
    Vector3 posicaoInicial;

    [Header("Membros responsaveis pelo swipe")]
    [Tooltip("Variavel para determinar a distancia minima de swipe.")]
    [SerializeField]
    float minDisSwipe = 2.0f;

    [SerializeField]
    [Tooltip("Distancia percorrida pela bola")]
    float swipeMove = 3.0f;

    Vector3 touchInicio;

	// Use this for initialization
	void Start () {
        jogadorRB = GetComponent<Rigidbody>();
        /* posicaoInicial = transform.position;
         posicaoInicial.x = 2.0f;
         transform.position = posicaoInicial;*/
    }
	
	// Update is called once per frame
	void Update () {
        if (MenuPause.isPausado) {
            return;
        }
        //var direcaoHorizontal =
        //    Input.GetAxis("Horizontal");
        var direcaoHorizontal = 0.0f;

        //Esse metodo funciona para clique com botao esquerdo
        //ou toque na tela
//#if UNITY_STANDALONE
        if (Input.GetMouseButton(0)) {
            var pos = Camera.main.ScreenToViewportPoint
                (Input.mousePosition);
            if(pos.x < 0.5) {
                direcaoHorizontal = -1;
            } else {
                direcaoHorizontal = 1;
            }
            //Verifica se objetos foram tocados
            TocarObjeto(Input.mousePosition);
        }
        //#elif UNITY_IOS || UNITY_ANDROID
        if (movimentoHorizontal == TipoMovimentoHorizontal.Acelerometro) {
            //Tratar o movimento via acelerometro
            //Mover a bola via acelerometro
            direcaoHorizontal = Input.acceleration.x;
        } else {
            //Exclusivo para touch
            if (Input.touchCount > 0) {
                //Obtendo o primeiro touch
                Touch toque = Input.touches[0];
                //Obtendo o valor em View Port
                var pos = Camera.main.ScreenToViewportPoint(toque.position);

                if (pos.x < 0.5) {
                    direcaoHorizontal = -1;
                } else {
                    direcaoHorizontal = 1;
                }
                //Verifica se houve swipe
                SwipeTeleport(toque);
            }
        }
        //#endif

        //Usando o time.DeltaTime

        float constante = 20;
        Vector3 velocidade = new Vector3(velocidadeEsquiva * 
            direcaoHorizontal,
            0, velocidadeRolamento);
        jogadorRB.AddForce(velocidade * Time.deltaTime * constante);
	}
    /// <summary>
    /// Metodo para tratar o swipe
    /// </summary>
    /// <param name="touch"></param>
    void SwipeTeleport(Touch touch) {

        //Verifica se esse eh o ponto onde o swipe comecou
        if(touch.phase == TouchPhase.Began) {
            touchInicio = touch.position;
        } 
        //Verifica se esse eh o final do touch (final do swipe)
        else if(touch.phase == TouchPhase.Ended){

            Vector3 touchFim = touch.position;
            Vector3 direcao;

            //Calcular a diferenca no eixo X
            float dif = touchFim.x - touchInicio.x;

            //Determinar o modulo 
            if(Mathf.Abs(dif) >= minDisSwipe) {

                //Determinar a direcao do swipe
                if(dif < 0) {
                    direcao = Vector3.left;
                } else {
                    direcao = Vector3.right;
                }

            } else {
                return;
            }

            RaycastHit hit;

            if(!jogadorRB.SweepTest(direcao,out hit, swipeMove)) {
                jogadorRB.MovePosition(jogadorRB.position + 
                    (direcao * swipeMove));
            }
        }
    }

    /// <summary>
    /// Metodo para identificar se objetos foram tocados
    /// </summary>
    /// <param name="posicaoClique">O clique ocorrido nesse frame</param>
    static void TocarObjeto(Vector3 posicaoClique) {
        //Converter o clique na tela em um Ray
        Ray cliqueRay = Camera.main.ScreenPointToRay(posicaoClique);
        //Objeto em rota de colisao
        RaycastHit hit;
        if (Physics.Raycast(cliqueRay,out hit)) {
            hit.transform.SendMessage("ObjetoTocado",
                SendMessageOptions.DontRequireReceiver);
        }
    }
}
