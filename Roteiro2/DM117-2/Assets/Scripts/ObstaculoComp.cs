using UnityEngine;
using UnityEngine.SceneManagement;
// ReSharper disable IdentifierTypo

public class ObstaculoComp : MonoBehaviour
{
    [Tooltip("Particle System da explosao")]
    public GameObject explosao;

    // Start is called before the first frame update
    // ReSharper disable once StringLiteralTypo
    [Tooltip("Quanto tempo antes de reiniciar o jogo")]
    public float tempoEspera = 2.0f;

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se eh o jogfador
        if (collision.gameObject.GetComponent<JogadorComportamento>())
        {
            Destroy(collision.gameObject);
            Invoke(nameof(ResetaJogo), tempoEspera);
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
    /// Metodo invocado atraves doSendMessage(), para detectar se esse objeto foi tocado
    /// </summary>
    public void ObjetoTocado()
    {
        print("Particulas criadas");
        if (explosao != null)
        {
            // Criar o efeito da explosao
            var particulas = Instantiate(explosao, transform.position, Quaternion.identity);


            // Destroi as particulas
            Destroy(particulas, 1.0f);
        }
        // Destroi esse obstaculo
        Destroy(this.gameObject);
    }
    
    // void Start()
    // {
    //     
    // }
    //
    // // Update is called once per frame
    // void Update()
    // {
    //     
    // }
}
