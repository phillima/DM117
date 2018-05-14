using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour {

    public static bool pausado;

    [SerializeField]
    [Tooltip("Referencia para o menu pause")]
    private GameObject menuPause;

    /// <summary>
    /// Metodo para reiniciar a tela do jogo
    /// </summary>
    public void Restart() {
        SceneManager.LoadScene(SceneManager.
                        GetActiveScene().name);
    }

    /// <summary>
    /// Metodo para pausar/despausar o jogo
    /// </summary>
    /// <param name="isPausado"></param>
    public void SetMenuPause(bool isPausado) {
        pausado = isPausado;

        //Se o jogo estiver pausado, timeScale recebe0
        Time.timeScale = (pausado) ? 0 : 1;

        //Deixa o MenuPause-Panel ativado/desativado
        menuPause.SetActive(pausado);

    }
    
    /// <summary>
    /// Metodo para carregar scenes
    /// </summary>
    /// <param name="nomeScene"></param>
    public void CarregaScene(string nomeScene) {
        SceneManager.LoadScene(nomeScene);
    }

	// Use this for initialization
	void Start () {
        pausado = false;
#if !UNITY_ADS
        SetMenuPause(false);
#endif
	}
}
