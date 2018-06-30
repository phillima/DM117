using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstaculo : MonoBehaviour {

    [SerializeField]
    float tempoReiniciar = 2.0f;

    private void OnCollisionEnter(Collision collision) {
        
        //Verificar se foi o jogador/bola
        if(collision.gameObject.
            GetComponent<JogadorControle>()) {
            Destroy(collision.gameObject);
            Invoke("ResetaJogo", tempoReiniciar);

        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ResetaJogo() {
        //Reinicia o Level/Scene/Fase
        SceneManager.LoadScene(SceneManager.
                GetActiveScene().name);
    }
}
