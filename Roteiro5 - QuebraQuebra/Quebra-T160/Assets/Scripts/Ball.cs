using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    //Referencia para o rigidbody da bola
    Rigidbody2D ballRB;
    //Referncia para a plataforma (paddle)
    Paddle paddle;
    //Referencia para a cruz
    CrossHair crossHair;
    //Referencia para o audio source
    AudioSource ballAudioSource;

    //Posicao inicial da bola
    Vector3 offset = new Vector3(0f,0.6f,0f);

	// Use this for initialization
	void Start () {
        ballAudioSource = GetComponent<AudioSource>();
        ballRB = GetComponent<Rigidbody2D>();
        ballRB.isKinematic = true;
        paddle = FindObjectOfType<Paddle>();
        crossHair = FindObjectOfType<CrossHair>();
        

        /*print("Adesivo para pessoa numero: " + 
                Random.Range(1,20));
        print("Adesivo para pessoa numero: " +
                Random.Range(1, 20));
        print("Adesivo para pessoa numero: " +
                Random.Range(1, 20));
        print("Adesivo para pessoa numero: " +
                Random.Range(1, 20));
        print(Random.Range(0, 2));*/
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
        Vector2 ballDirection =
                (crossHair.transform.position -
                 transform.position);
        ballDirection.Normalize();
        ballRB.isKinematic = false;
        ballRB.AddForce(ballDirection * 15,ForceMode2D.Impulse);
        LevelControl.hasGameStarted = true;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        ballAudioSource.Play();
    }
}
