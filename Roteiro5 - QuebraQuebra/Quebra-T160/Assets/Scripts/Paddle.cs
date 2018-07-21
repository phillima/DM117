using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
    
    //Referencia para a camera principal
    Camera mainCamera;

	// Use this for initialization
	void Start () {
        mainCamera = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
        ManualMovement();
	}

    //Metodo para mover a plataforma (paddle)
    void ManualMovement() {
        Vector3 paddlePos = new Vector3(0,1,0);
        paddlePos.x =
            Mathf.Clamp(mainCamera.
            ScreenToWorldPoint(Input.mousePosition).x,
            1.2f,
            14.8f);
        transform.position = paddlePos;
    }
}
