using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControle : MonoBehaviour {

    [SerializeField]
    JogadorControle jogador;
 
    Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = jogador.transform.position 
            - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(jogador != null) {
            transform.position =
                jogador.transform.position - offset;
        }
	}
}
