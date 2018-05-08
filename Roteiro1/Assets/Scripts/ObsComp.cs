using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe para definir o comportamento do obstaculo
/// </summary>
public class ObsComp : MonoBehaviour {

    [SerializeField]
    [Tooltip("Tempo para reiniciar o jogo")]
    private float tempoDestruir = 2.0f;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.GetComponent<JogadorComp>()) {
            Invoke("Reset", tempoDestruir);
        }
    }

    private void Reset() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
