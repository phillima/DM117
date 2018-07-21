using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    //Referencia para o rigidbody da bola
    Rigidbody2D ballRB;
    //Referncia para a plataforma (paddle)
    Paddle paddle;

    //Posicao inicial da bola
    Vector3 offset = new Vector3(0f,0.6f,0f);

	// Use this for initialization
	void Start () {
        ballRB = GetComponent<Rigidbody2D>();
        ballRB.isKinematic = true;
        paddle = FindObjectOfType<Paddle>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!LevelControl.hasGameStarted) {
            if (Input.GetMouseButtonDown(0)) {
                LaunchBall();
            } else {
                MoveWithPaddle();
            }
        }
	}
    //Metodo para mover a bola agarrado com a plataforma
    void MoveWithPaddle() {
        transform.position = paddle.transform.position
                                + offset;
    }

    //Metodo para lancar a bola
    void LaunchBall() {
        ballRB.isKinematic = false;
        ballRB.AddForce(Vector2.up * 15,ForceMode2D.Impulse);
        LevelControl.hasGameStarted = true;
    }
}
