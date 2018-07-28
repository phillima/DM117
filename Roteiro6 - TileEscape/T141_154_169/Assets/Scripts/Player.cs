using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //Serialized
    [SerializeField]
    float playerRunningSpeed;
    [SerializeField]
    Vector2 playerJumpSpeed;

    //Const
    const string ANIM_RUNNING = "PlayerRunning";
    const string ANIM_CLIMB = "PlayerClimbing";
    const string ANIM_DEAD = "PlayerDead";
    const string ANIM_RUNNING_BEGIN = "PlayerRunningBegin";
    const string ANIM_PLAYER_BEGIN = "PlayerBegin";

    //Cached
    Rigidbody2D playerRB;
    Animator playerAnimator;
    CapsuleCollider2D playerCapsuleCollider;
    GameSession gameSession;

    //Members
    float playerXDir;
    float playerYDir;
    float playerJumpKey = -1f;
    float playerClimbSpeed = 5.0f;
    bool playerOnLadder = false;
    float playerGravity;
    bool isPlayerDead = false;
    int LAYER_ENEMY;
    int LAYER_PLAYER;

    //Inicializacao
    void Start() {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCapsuleCollider = GetComponent<CapsuleCollider2D>();
        playerGravity = playerRB.gravityScale;
        gameSession = FindObjectOfType<GameSession>();
        LAYER_ENEMY = LayerMask.NameToLayer("Enemy");
        LAYER_PLAYER = LayerMask.NameToLayer("Player");

        Physics2D.IgnoreLayerCollision(LAYER_ENEMY, LAYER_PLAYER, false);
    }

    //Obter entrada do usuario
    void Update() {
        playerXDir = Input.GetAxis("Horizontal");
        playerYDir = Input.GetAxis("Vertical");
        
        if (Input.GetButtonDown("Jump") && playerCapsuleCollider.
            IsTouchingLayers(LayerMask.GetMask("Foreground"))) {
                playerJumpKey = Time.time + 0.5f;
        }
    }

    //Usado para atualizacoes que envolvem fisica.
    void FixedUpdate() {
        if (isPlayerDead) {//Jogador esta morto, faz nada.
            return;
        }
        if (!GameSession.gameStart) {//Jogo nao comecou
            Run();//Pode apenas correr na tela Start
            return;
        } else {//Jogo comecou
            Run();//Pode correr
            if (Time.time < playerJumpKey) {//Para para nao perder input de pulo
                playerJumpKey = -1f;
                Jump();//Pular
            }
            Climb();//Subir a escada
        }
    }

    //Metodo para correr
    void Run() {
        playerRB.velocity = new Vector2(playerXDir * playerRunningSpeed, playerRB.velocity.y);
       if (playerXDir != 0) {
            FlipSprite();
            playerAnimator.SetBool(ANIM_RUNNING, true);
        } else {
            playerAnimator.SetBool(ANIM_RUNNING, false);
        }
    }

    //Metodo para pular
    void Jump() {
        playerJumpSpeed.x *= Mathf.Sign(playerRB.velocity.x);
        playerRB.velocity += playerJumpSpeed;
    }

    void Climb() {
        if (playerOnLadder) {
            Vector2 playerClimbVelocity = new Vector2(playerRB.velocity.x, 0.0f);
            if (playerYDir != 0) {
                playerClimbVelocity = new Vector2(0.0f, playerClimbSpeed * playerYDir);
                playerAnimator.speed = 1.0f;
            } else {
                playerAnimator.speed = 0.0f;
            }
            playerRB.velocity = playerClimbVelocity;
        }
    }

    //Metodo para inverter o sprite
    void FlipSprite() {
        transform.localScale = new Vector3(Mathf.Sign(playerRB.velocity.x),1,1);
    }

    //Jogador entrou na escada
    //Esta liberado subir/descer
    /*void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Ladder>()) {
            canClimb = true;
        } 
    }*/

    //Verificamos se o jogador esta dentro da escada
    void OnTriggerStay2D(Collider2D collision) {
        if (collision.GetComponent<Ladder>() && !isPlayerDead) {
            //Executaremos isso uma vez
            //Caso o jogador ainda nao tenha descido
            //e se nao estiver tocando o chao
            if ((!playerCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Foreground")) ||
                playerYDir != 0) && !playerOnLadder){
                playerAnimator.SetBool(ANIM_CLIMB, true);//Coloca a animacao da escada
                playerAnimator.speed = 0.0f;//Pausar a animacao 
                playerRB.gravityScale = 0.0f;//Ignora a gravidade para ele nao cair
                playerOnLadder = true; //Jogador esta na escada
            }
        }
    }

    //Quando o jogador sai da escada
    void OnTriggerExit2D(Collider2D collision) {
        if (collision.GetComponent<Ladder>()) {
            playerAnimator.SetBool(ANIM_CLIMB, false);//Retorna animacao idle
            playerRB.gravityScale = playerGravity;//Gravidade retorna
            playerAnimator.speed = 1.0f;//Animacao normalmente
            playerOnLadder = false;//Jogador nao se encontra na escada
        }
    }

    //Colidiu com o inimigo
    //Mata o jogador
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<Enemy>() && !isPlayerDead) {
            PlayerDead();
        } else if (isPlayerDead && 
                playerCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Foreground"))) {
            playerRB.velocity = Vector2.zero;//Zerar a velocidade apos morrer e tocar no chao
        }
    }

    //Matar o player
    void PlayerDead() {
        if(isPlayerDead) { return; }//Evitar de ser chamada outra vezes
        isPlayerDead = true;
        playerAnimator.SetTrigger(ANIM_DEAD);//Liga animacao dead
        playerRB.velocity = new Vector2(10.0f * Random.Range(-1,2), 15f);//Faz um pulo dramatico
        Physics2D.IgnoreLayerCollision(LAYER_ENEMY, LAYER_PLAYER);//Ignora a fisica 
        gameSession.PlayerDied();//Avisa o gamesession que o player morreu
    }

}
