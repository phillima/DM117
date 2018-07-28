using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //Serializaveis
    [SerializeField]
    float playerRunSpeed;
    [SerializeField]
    float playerJump;

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

    float playerJumpKey = -1f;

    // Use this for initialization
    void Start() {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCapCollider = 
            GetComponent<CapsuleCollider2D>();
    }

    //Usaremos esse metodo
    //para pegar inputs
	void Update () {
        playerXDir = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") &&
            playerCapCollider.IsTouchingLayers(
                LayerMask.GetMask("Foreground"))) {
            playerJumpKey = Time.time + 0.5f;
        }
	}

    //Usaremos o fixed para
    //atualizacoes com relacao a fisica
    void FixedUpdate() {
        Run();
        if(Time.time < playerJumpKey) {
            playerJumpKey = -1f;
            Jump();
        }
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

}
