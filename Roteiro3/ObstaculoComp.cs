using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstaculoComp : MonoBehaviour {

    [Tooltip("Particle System da explosao")]
    public GameObject explosao;

    [Tooltip("Quanto tempo antes de reiniciar o jogo")]
    public float tempoEspera = 2.0f;

    private void OnCollisionEnter(Collision collision) {
        //Verifica se eh(acho boa pratica nao usar acento) o jogador
        if (collision.gameObject.GetComponent<JogadorComportamento>())
        {
            Destroy(collision.gameObject);
            Invoke("ResetaJogo", tempoEspera);
        }
    }
    /// <summary>
    /// Reinicia o level
    /// </summary>
    void ResetaJogo()
    {
        // Reinicia o level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Metodo invocado atraves do SendMessage(), para detectar que este objeto foi tocado
    /// </summary>
    public void ObjetoTocado() {
        if (explosao != null) {
            //Cria o efeito da explosao
            var particulas = Instantiate(explosao, transform.position,
                    Quaternion.identity);
            //Destroi as particulas
            Destroy(particulas, 1.0f);
        }
        //Destroi este obstaculo
        Destroy(this.gameObject);
    }
    

}


