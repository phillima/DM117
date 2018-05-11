using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObsComp : MonoBehaviour {

    [Tooltip("Quanto tempo antes de reiniciar o jogo")]
    private float tempoEspera = 2.0f;

    private void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.GetComponent<JogadorComp>()) {
            Destroy(collision.gameObject);
            Invoke("Reset", tempoEspera);
        }

    }

    /// <summary>
    /// Metodo para reiniciar o jogo
    /// </summary>
    private void Reset() {

        //Reinicia o level
        SceneManager.LoadScene(SceneManager.
            GetActiveScene().name);
    }
}
