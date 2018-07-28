using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //Serializaveis
    [SerializeField]
    float playerRunSpeed;
    [SerializeField]
    float playerJump;
    [SerializeField]
    float playerClimbSpeed;

    //constantes
    const string ANIM_RUNNING = "PlayerRunning";

    //Referencias
    //Referencia para corpo rigido
    Rigidbody2D playerRB;
    //Referencia para o animator
    Animator playerAnimator;
    //Referencia para o collider
    CapsuleCollider2D playerCapCollider;
    
    //Variaves de controle
    float playerXDir;
    float playerYDir;
    bool playerOnLadder = false;
    float playerJumpKey = -1f;
    float playerGravity;
    bool playerDead = false;

    // Use this for initialization
    void Start() {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCapCollider = 
            GetComponent<CapsuleCollider2D>();
        playerGravity = playerRB.gravityScale;
    }

    //Usaremos esse metodo
    //para pegar inputs
	void Update () {
        playerXDir = Input.GetAxis("Horizontal");
        playerYDir = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") &&
            playerCapCollider.IsTouchingLayers(
                LayerMask.GetMask("Foreground"))) {
            playerJumpKey = Time.time + 0.5f;
        }
	}

    //Usaremos o fixed para
    //atualizacoes com relacao a fisica
    void FixedUpdate() {
        if (playerDead) {
            return;
        }
        Run();
        if(Time.time < playerJumpKey) {
            playerJumpKey = -1f;
            Jump();
        }
        Climb();
    }

    void Run() {
        playerRB.velocity =
            new Vector2(
                playerXDir * playerRunSpeed,
                playerRB.velocity.y);
        if(playerXDir != 0) {
            FlipSprite();
            playerAnimator.SetBool(ANIM_RUNNING,
                true);
        } else {
            playerAnimator.SetBool(ANIM_RUNNING, 
                false);
        }
    }

    void FlipSprite() {
        transform.localScale =
            new Vector3(
                Mathf.Sign(playerRB.velocity.x)
                ,1, 
                1);
    }

    void Jump() {
        playerRB.velocity =
            new Vector2(playerRB.velocity.x,
            playerJump);
    }
    //Metodo para subir/descer a escada
    void Climb() {
        //Verifica se player esta na escada
        if (playerOnLadder) {
            if(playerYDir != 0) {
                playerRB.velocity =
                    new Vector2(
                    playerRB.velocity.x,
                    playerYDir * playerClimbSpeed);
                playerAnimator.speed = 1.0f;
            } else {
                playerAnimator.speed = 0.0f;
                playerRB.velocity =
                    new Vector2(
                        playerRB.velocity.x,
                        0.0f);
            }
        }
    }
    //Vamos usar esse metodo
    //para iniciar o processo de climb
    void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("Ladder")) {
            //Verifica se houve movimento vertical
            if ((!playerCapCollider.IsTouchingLayers(
                    LayerMask.GetMask("Foreground")) ||
                    playerYDir != 0) && !playerOnLadder) {
                playerAnimator.
                    SetBool("PlayerClimb", true);
                playerAnimator.speed = 0f;
                playerRB.gravityScale = 0;
                playerOnLadder = true;
            }
        } 
    }
    //Metodo para tratar a saida da escada
    void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Ladder")) {
            playerRB.gravityScale = playerGravity;
            playerAnimator.SetBool("PlayerClimb",
                false);
            playerAnimator.speed = 1.0f;
            playerOnLadder = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<Enemy>()) {
            playerDead = true;
            playerAnimator.SetTrigger("PlayerDead");
            playerRB.velocity =
                new Vector2(-5.0f, 12f);//Pulo Dramatico
            Physics2D.IgnoreLayerCollision(
                LayerMask.NameToLayer("Player"),
                LayerMask.NameToLayer("Enemy"));
        }
    }



}
