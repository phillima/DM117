using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour {

    public static bool pausado;

    [Tooltip("Referencia para o GO. Usado para ligar/desligar")]
    public GameObject menuPause;

    /// <summary>
    /// Metodo para reiniciar a Scene, reiniciando o jogo
    /// </summary>
	public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Metodo para pausar o jogo
    /// </summary>
    /// <param name="isPausado">Parametro booleano que indicara se o jogo será pausado</param>
    public void SetMenuPause(bool isPausado) {
        pausado = isPausado;

        // Se o jogo estiver pausado o timeScale vale 0, caso contrário 1
        Time.timeScale = (pausado) ? 0 : 1;
        menuPause.SetActive(pausado);
    }

    /// <summary>
    /// Metodo para carregar uma Scene
    /// </summary>
    /// <param name="nomeScene">nome da scene que sera carregada</param>
    public void CarregaScene(string nomeScene) {
        SceneManager.LoadScene(nomeScene);
    }
    
    // Use this for initialization
	void Start () {
        pausado = false;
        SetMenuPause(false);
	}
}
