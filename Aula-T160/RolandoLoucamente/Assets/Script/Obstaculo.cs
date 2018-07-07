using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstaculo : MonoBehaviour {

    [SerializeField]
    float tempoReiniciar = 2.0f;

    [SerializeField]
    [Tooltip("Efeito de explosao do obstaculo")]
    GameObject explosao;

    private void OnCollisionEnter(Collision collision) {
        
        //Verificar se foi o jogador/bola
        if(collision.gameObject.
            GetComponent<JogadorControle>()) {
            Destroy(collision.gameObject);
            Invoke("ResetaJogo", tempoReiniciar);
        }
    }
    void ResetaJogo() {
        //Reinicia o Level/Scene/Fase
        SceneManager.LoadScene(SceneManager.
                GetActiveScene().name);
    }

    public void ObjetoTocado() {
        if(explosao != null) {
            //Cria o efeito da explosao
            var particulas = Instantiate(explosao, transform.position,
                Quaternion.identity);
            Destroy(particulas, 1.0f);
        }
        //Destroi o obstaculo
        Destroy(gameObject);
    }
}
