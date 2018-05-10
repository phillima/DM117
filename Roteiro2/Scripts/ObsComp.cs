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

    [SerializeField]
    [Tooltip("Referencia para a explosao")]
    private GameObject explosao;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.GetComponent<JogadorComp>()) {
            Destroy(collision.gameObject);
            Invoke("Reset", tempoDestruir);
        }
    }

    private void Reset() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    /// <summary>
    /// Metodo para verificar se o obstaculo foi tocado
    /// </summary>
    public void ObjetoTocado() {
        print("Aqui");
        if (explosao) {

            var particulas = Instantiate(explosao, transform.position, Quaternion.identity);
            Destroy(particulas, 1.0f);
        }

        Destroy(this.gameObject);
    }
}
