using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstaculoComp : MonoBehaviour {

    [SerializeField]
    [Tooltip("Tempo para destruir o jogador")]
    float tempoDestruir = 2.0f;

    [SerializeField]
    [Tooltip("Tempo para resetar o jogo")]
    float tempoResetJogo = 2.0f;


    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.GetComponent<JogadorControle>()) {
            Destroy(collision.gameObject);
            Invoke("ResetJogo", tempoResetJogo);
        }    
    }

    public void ResetJogo() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
