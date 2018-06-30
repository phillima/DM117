using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FimTile : MonoBehaviour {

    float tempoDestruir = 2.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        //Verificar se foi a esfera/jogador/bola que 
        //passou pelo trigger
        if (other.GetComponent<JogadorControle>()) {
            FindObjectOfType<Controlador>().
                SpawnProxTile();
            Destroy(transform.parent.gameObject);

        }
    }
}
