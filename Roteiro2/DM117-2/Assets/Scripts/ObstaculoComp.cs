using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// ReSharper disable IdentifierTypo

public class ObstaculoComp : MonoBehaviour
{
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
