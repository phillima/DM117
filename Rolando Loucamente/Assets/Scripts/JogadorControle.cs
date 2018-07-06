using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JogadorControle : MonoBehaviour {
    /// <summary>
    /// Rigidbody pertencente ao jogador
    /// </summary>
    Rigidbody jogadorRB;

    [Tooltip("Velocidade de Esquiva do jogador")]
    [SerializeField]
    [Range(1,10)]
    float velocidadeEsquiva = 5.0f;

    [Tooltip("Velocidade de Rolamento do Jogador")]
    [SerializeField]
    [Range(1, 10)]
    float velocidadeRolamento = 5.0f;

    [Tooltip("Torque eixo X")]
    [SerializeField]
    [Range(1, 150)]
    float torqueX = 1.0f;

    /// <summary>
    /// Direcao do movimento horizontal. Valor entre -1 e 1.
    /// </summary>
    float direcaoHorizontal;

    //Area de variaveis do Swipe

    [Header("Variaveis responsáveis pelo swipe")]
    [Tooltip("Distancia minima para ser considerado um swipe")]
    [SerializeField]
    float minDisSwipe = 2.0f;
    [Tooltip("Distancia que o jogador sera deslocado pelo swipe")]
    [SerializeField]
    float swipeMove = 2.0f;

    /// <summary>
    /// Ponto incial do touch
    /// </summary>
    Vector2 touchInicio;

	// Use this for initialization
	void Start () {
        jogadorRB = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        //Obter a direcao. 
        //0 se nenhuma tecla for pressionada
        //-1 se manter A pressionado
        //1 se manter D pressionado
        //direcaoHorizontal = Input.GetAxis("Horizontal");
#if UNITY_STANDALONE
        //Detectando clique com o mouse/touch
        if (Input.GetMouseButton(0)) {
            //Obter as coordenadas do clique/touch em View Port (valor entre 0 e 1)
            CalculaDirecao(Input.mousePosition);
        }
#elif UNITY_ANDROID || UNITY_IOS
        //Detectando exclusivamente via touch
        if(Input.touchCount > 0) {
            Touch touch = Input.touches[0];
            //Obter as coordenadas do clique/touch em View Port (valor entre 0 e 1)
            CalculaDirecao(touch.position);
            SwipeTeleport(touch);
        }
#endif
    }

    //Todos os calculos que envolvem fisica devem ser feitos no fixed update
    void FixedUpdate() {
        jogadorRB.AddForce(velocidadeEsquiva * direcaoHorizontal, 0, velocidadeRolamento);
        jogadorRB.AddTorque(transform.right * torqueX);
    }

    /// <summary>
    /// Devolve a direcao do movimento horizontal (-1 esquerda, 1 direita)
    /// </summary>
    /// <param name="posicaoPixels">Coordenada em pixels do clique/toque</param>
    void CalculaDirecao(Vector3 posicaoPixels) {
        var pos = Camera.main.ScreenToViewportPoint(posicaoPixels);
        if (pos.x > 0.5)
            direcaoHorizontal = 1;
        else
            direcaoHorizontal = -1;
    }

    /// <summary>
    /// Metodo para detectar se o swipe ocorreu
    /// </summary>
    /// <param name="touch"></param>
    void SwipeTeleport(Touch touch) {

        //Verifica se eh o primeiro touch
        if (touch.phase == TouchPhase.Began)
            touchInicio = touch.position;
        //Verifica se eh o segundo touch
        else if(touch.phase == TouchPhase.Ended) {
            Vector2 touchFim = touch.position;
            Vector3 direcaoSwipe;

            //Diferenca entre ponto final e inicia
            float difX = touchFim.x - touchInicio.x;

            //Verifica se o swipe percorreu uma distância 
            //mínima para ser considerado swipe
            if (Mathf.Abs(difX) >= minDisSwipe) {
                if (difX < 0)
                    direcaoSwipe = Vector3.left;
                else
                    direcaoSwipe = Vector3.right;
            }else
                return;

            //Usar Raycast para detectar possivel colisão
            RaycastHit hit;

            if(!jogadorRB.SweepTest(direcaoSwipe,out hit, swipeMove)) {
                jogadorRB.MovePosition(jogadorRB.position + (direcaoSwipe * swipeMove));
            }

        }
    }
}
