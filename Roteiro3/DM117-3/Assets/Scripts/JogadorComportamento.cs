using System;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
// using Vector3 = System.Numerics.Vector3;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

[RequireComponent(typeof(Rigidbody))]
public class JogadorComportamento : MonoBehaviour
{
    public enum TipoMovHorizontal
    {
        Acelerometro,
        Touch
    }
    
    public TipoMovHorizontal movHorizontal = TipoMovHorizontal.Acelerometro;
    /// <summary>
    /// Referência para o componente RigidBody
    /// </summary>
    private Rigidbody _rb;
    
    // ReSharper disable once StringLiteralTypo
    [Tooltip("Velocidade da reação da bola ao desviar dos obstaculos")]
    [Range(0, 10)]
    public float velocidadeEsquiva = 0.5f;
    
    [Tooltip("Velocidade do movimento da bola para frente")]
    [Range(0, 10)]
    public float velocidadeRolamento = 0.5f;
    // Start is called before the first frame update

    [Header("Atributos Responsaveis pelo swipe")]
    [Tooltip("Determina qual a distancia que o dedo do jogador" +
             "deve se deslocar pela tela para ser considerado um swipe")]
    public float minDisSwipe = 2.0f;
    
    [Tooltip("Distancia que a bola ira percorrer atraves do swipe")]
    public float swipeMove = 2.0f;
    
    /// <summary>
    /// Ponto inicial onde o swipe ocorre
    /// </summary>
    private Vector2 _touchInicio;
    
    void Start()
    {
        // Obter acesso ao componente RigidBody associado a este GO
        _rb = GetComponent<Rigidbody>(); 
    }

    void Update()
    {
        if (MenuPause.Pause)
        {
            return;
        }

        // Verificar para qual lado o jogador deseja esquivar
        var velocidadeHorizontal = Input.GetAxis("Horizontal") * velocidadeEsquiva;

#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBPLAYER
        // Detectando se ouve clique com o botão direito ou toque na tela
        if (Input.GetMouseButton(0))
        {
            // Aplicar a velocidade horizontal
            velocidadeHorizontal = CalculaMovimento(Input.mousePosition);
            TocarObjetos(Input.mousePosition);
        }
// Verifica se estamos no mobile se n estivermos o codigo fica cinza
#elif UNITY_IOS || UNITY_ANDROID

        if (movHorizontal == tipoMovHorizontal.Acelerometro)
        {
            // Move a bola na direção do acelerometro
            velocidadeHorizontal = Input.acceleration.x * velocidadeRolamento;
        }
        else
        {
            // Movimento por touch detectando exclusivamente por touch
            if (Input.touchCount > 0)
            {
                // Obtem o primeiro touch na tela dentro do frame
                Touch touch = Input.touches[0];
                
                // Usando por touch
                velocidadeHorizontal = CalculaMovimento(touch.position);
                
                // usando movimento por swipe 
                // lembrando que podemos ter o jogo pelo swipe e pelo touch ao mesmo tempo
                swipeTeleport(touch);
                
                // Verifica se o touch atingiu algum objeto
                tocarObjetos(touch.position);
            }
        }

#endif
        var focaMovimento = new Vector3(velocidadeHorizontal, 0, velocidadeRolamento);
        
        // Time.delta nos retorna o tempo gasto no frame anterior 
        // algo em torno de 1/60fps
        // Usamos esse valor para garantir que a nossa bola se desloque com a mesma velocidade 
        // nao importa o hardware.
        focaMovimento *= (Time.deltaTime * 60);
        
        // Aplicar uma força para que a bola se desloque 
        _rb.AddForce(focaMovimento);
    }

    // Update is called once per frame

    /// <summary>
    /// Metodo para calcular para onde o jogador se deslocara na horizontal
    /// </summary>
    /// <param name="screenSpaceCoord">As coordenadas no Screen Space</param>
    /// <param name="screenSpaceCood"></param>
    /// <returns></returns>
    private float CalculaMovimento(Vector2 screenSpaceCood)
    {
        var pos = Camera.main.ScreenToViewportPoint(screenSpaceCood);
        float directionX = 0;
        
        directionX = pos.x > 0.5f ? 1 : -1;
        return directionX * velocidadeEsquiva;
    }

    private void swipeTeleport(Touch touch)
    {
        // Verificar se esse e o ponto que o swipe comecou
        if (touch.phase == TouchPhase.Began)
            _touchInicio = touch.position;
        
        // Verificar se o swipe acabou
        else if (touch.phase == TouchPhase.Ended)
        {
            Vector2 touchFim = touch.position;
            Vector3 direcaoMov;
            
            // Faz a diferença entre o ponto final e inicial do swipe
            float dif = touchFim.x - _touchInicio.x;
            
            // Verifica se o swipe percorreu uma distancia suficiente
            // Para ser reconhecido como swipe
            if (Math.Abs(dif) >= minDisSwipe) 
                direcaoMov = dif > 0 ? Vector3.right : Vector3.left;
            else 
                return;

            // Raycast eh outra forma de detectar colisão 
            RaycastHit hit;
            
            // Vamos verificar se o swipe não vai causar colisao
            if (!_rb.SweepTest(direcaoMov, out hit, swipeMove))
                _rb.MovePosition(_rb.position +(direcaoMov * swipeMove));
        }
    }

    /// <summary>
    /// Metodo para verificar se o objeto foi tocado
    /// </summary>
    /// <param name="touch">Objeto que foi tocado nesse frame</param>
    private static void TocarObjetos(Vector3 pos)
    {
        // convertemos a posicao do touch (screen space) para um Ray
        Ray touchRay = Camera.main.ScreenPointToRay(pos);

        // Objeto que ira salvar as informações de um possivel objeto q foi tocado
        RaycastHit hit;

        if (Physics.Raycast(touchRay, out hit))
            hit.transform.SendMessage("ObjetoTocado", SendMessageOptions.DontRequireReceiver);
    }
}