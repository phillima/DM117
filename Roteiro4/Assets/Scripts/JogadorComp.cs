using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Classe que define o comportamento do jogador
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class JogadorComp : MonoBehaviour {

    public enum TipoMovimentoHorizontal {
        Acelerometro,
        Touch
    }

    [SerializeField]
    [Tooltip("Define o tipo de movimento horizontal")]
    private TipoMovimentoHorizontal TipoMovimento = TipoMovimentoHorizontal.Touch;

    /// <summary>
    /// Referencia para o corpo rigido da bola
    /// </summary>
    private Rigidbody rb;

    [SerializeField]
    [Tooltip("Controla a velocidade de rolamento")]
    [Range(1, 20)]
    private float velocidadeRolamento = 5.0f;

    [SerializeField]
    [Tooltip("Controla a velocidade de esquiva")]
    [Range(1, 20)]
    private float velocidadeEsquiva = 5.0f;


    [Header("Atributos responsaveis pelo swipe")]
    [SerializeField]
    [Tooltip("Determina a distancia minima que o dedo do jogador deve percorrer para ser considerado um swipe")]
    private float minDisSwipe = 2.0f;

    [SerializeField]
    [Tooltip("Distacia percorrida pela bola depois do swipe")]
    private float swipeMove = 2.0f;

    /// <summary>
    /// Ponto inicial do touch
    /// </summary>
    private Vector2 pontoInicial;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        
        //O jogo esta pausado. Nao atualize
        if (MenuPauseComp.pausado)
            return;
        //Detectando uso das teclas A e D
        float velocidadeHorizontal = Input.GetAxis("Horizontal") * velocidadeEsquiva;

//#if UNITY_STANDALONE
        //Verifica se houve clique com o mouse na tela atraves do botao direito
        if (Input.GetMouseButton(0)) {
            velocidadeHorizontal = CalculaMovimento(Input.mousePosition);
            TocarObjeto(Input.mousePosition);
        }
//#elif UNITY_ANDROID || UNITY_IOS
        if(TipoMovimento == TipoMovimentoHorizontal.Acelerometro) {
            velocidadeHorizontal = Input.acceleration.x * velocidadeRolamento;
        } else {
            if (Input.touchCount > 0) {
                //Desejamos apenas o primeiro touch na tela
                Touch touch = Input.touches[0];
                velocidadeHorizontal = CalculaMovimento(touch.position);

                SwipeTeleporte(touch);

                TocarObjeto(touch.position);
            }
        }
//#endif

        var forcaMovimento = new Vector3(velocidadeHorizontal, 0, velocidadeRolamento);

        //Time.deltaTime retorna o tempo gasto no frame anterior
        //Assim sabemos o quanto nosso jogo está atrasado. Isso permite um game loop suave
        forcaMovimento *= (Time.deltaTime * 60);

        rb.AddForce(forcaMovimento);
    }

    private float CalculaMovimento(Vector2 screenSpacePos) {
        var posPortView = Camera.main.ScreenToViewportPoint(screenSpacePos);
        float direcaoX;
        //Toque no lado direito
        if (posPortView.x > 0.5) {
            direcaoX = 1;
        } else {//Toque no lado esquerdao
            direcaoX = -1;
        }
        return direcaoX * velocidadeEsquiva;

    }

    /// <summary>
    /// Funcao que trata o swipe
    /// </summary>
    private void SwipeTeleporte(Touch toque) {

        //Obtem o ponto inicial do swipe
        if (toque.phase == TouchPhase.Began) {
            pontoInicial = toque.position;
        }
        //Verifica se o swipe chegou ao fim (se o jogador soltou o dedo)
        else if (toque.phase == TouchPhase.Ended) {

            Vector2 pontoFinal = toque.position;
            Vector3 direcaoMove;
            //Calcula a diferenca
            float diferencaX = pontoFinal.x - pontoInicial.x;

            //Verifica se a distancia percorrida eh o suficiente 
            //para ser considerada um swipe
            if (Mathf.Abs(diferencaX) >= minDisSwipe) {
                
                //Verifica a direcao do swipe
                if (diferencaX < 0)
                    direcaoMove = Vector3.left;
                else
                    direcaoMove = Vector3.right;

            } else //Distancia insuficiente para ser considerado um swipe
                return;

            //Mas antes de executarmos o swipe, precisamos verificar 
            //se a bola nao ira colidir com algum obstaculo
            //Fazemos isso usando o Raycast
            RaycastHit hit;

            if (!rb.SweepTest(direcaoMove, out hit, swipeMove)) {
                rb.MovePosition(rb.position + (direcaoMove * swipeMove));
            }
        }
    }
    /// <summary>
    /// Metodo para identificar se objetos foram tocados 
    /// </summary>
    /// <param name="pos">A poscicao tocada/clicada nada tela</param>
    private static void TocarObjeto(Vector3 pos) {
        Ray posicaoTelaRay = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;

        if (Physics.Raycast(posicaoTelaRay, out hit)) {
            hit.transform.SendMessage("ObjetoTocado", SendMessageOptions.DontRequireReceiver);
        }
    }
}

