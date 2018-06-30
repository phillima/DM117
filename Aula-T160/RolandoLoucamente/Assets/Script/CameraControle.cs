using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControle : MonoBehaviour {

    JogadorControle jogador;
 
    Vector3 offset;

	// Use this for initialization
	void Start () {

        jogador = FindObjectOfType<JogadorControle>();
        offset = jogador.transform.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
