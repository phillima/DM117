using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JogadorComp : MonoBehaviour {

    public enum TipoMovimentoHozirontal 
    {
        Acelerometro,
        Touch
    }

    public TipoMovimentoHozirontal movimentoHorizontal =
            TipoMovimentoHozirontal.Touch;


    [SerializeField]
    [Tooltip("A velocidade qua a bola ira esquivar")]
    [Range(1, 10)]
    private float velocidadeEsquiva = 5.0f;

    [SerializeField]
    [Tooltip("Velocidade com qual a bola se desloca para a frente")]
    [Range(1, 10)]
    private float velocidadeRolamento = 5.0f;

    /// <summary>
    /// Variavel de detem o toque inicial
    /// </summary>
    private Vector2 touchInicio;

    [Header("Variaveis de controle do swipe")]
    [SerializeField]
    [Tooltip("Determinar distancia minima de swipe para " +
        "considerar swipe")]
    private float minDisSwipe = 2.0f;

    private float swipeMove = 2.0f;

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

        if (MenuPause.pausado)
            return;

        var velocidadeHorizontal = 
                Input.GetAxis("Horizontal") 
                * velocidadeEsquiva;

//#if UNITY_STANDALONE
        //Detectando se houve clique com o botao
        if (Input.GetMouseButton(0)) {
            velocidadeHorizontal = CalculaMovimento(
                    Input.mousePosition);
            TocarObjetos(Input.mousePosition);
        } 
            
//#elif UNITY_IOS || UNITY_ANDROID
        if(movimentoHorizontal == TipoMovimentoHozirontal.Acelerometro) {
            velocidadeHorizontal = Input.acceleration.x 
                        * velocidadeRolamento;
        }
        //Detectando se clique com o touch
        else if(movimentoHorizontal == TipoMovimentoHozirontal.Touch) {
            if (Input.touchCount > 0) {
                //Obtendo o primeiro touch
                Touch toque = Input.touches[0];
                velocidadeHorizontal = CalculaMovimento(
                            toque.position);
                SwipeTeleport(toque);
            }
        }
        //#endif

        var forcaMovimento = new Vector3(velocidadeHorizontal, 0,
            velocidadeRolamento);

        forcaMovimento *= (Time.deltaTime * 60);

        rb.AddForce(forcaMovimento);
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

    private void SwipeTeleport(Touch toque) {

        if(toque.phase == TouchPhase.Began) {
            touchInicio = toque.position;
        }else if (toque.phase == TouchPhase.Ended) {
            
            Vector2 toqueFim = toque.position;
            Vector3 direcaoMov = Vector3.zero;

            float dif = toqueFim.x - touchInicio.x;

            if (Mathf.Abs(dif) >= minDisSwipe) {

                //Determinar o sentido
                if(dif < 0) {
                    direcaoMov = Vector3.left;
                } else {
                    direcaoMov = Vector3.right;
                }

            } else {
                return;
            }

            //Variavel que ira salvar caso
            // o raio atinga algum objeto com collider
            RaycastHit hit;

            if(!rb.SweepTest(direcaoMov, out hit, swipeMove)) {
                rb.MovePosition(rb.position +
                    (direcaoMov * swipeMove));
            }
        }
    }

    /// <summary>
    /// Metodo para destruir obstaculo
    /// </summary>
    private static void TocarObjetos(Vector2 posicaoToque) {

        //Converter posicao da tela em um Ray
        Ray toqueRay = Camera.main.ScreenPointToRay(posicaoToque);

        //Objeto em potencial rota de colisao
        RaycastHit hit;

        if(Physics.Raycast(toqueRay, out hit)) {
            hit.transform.SendMessage("ObjetoTocado",
                SendMessageOptions.DontRequireReceiver);
        }
    }
}
