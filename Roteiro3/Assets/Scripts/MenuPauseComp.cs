using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPauseComp : MonoBehaviour {

    public static bool pausado;

    [SerializeField]
    private GameObject menuPausePanel;

    /// <summary>
    /// Metodo para reiniciar a scene
    /// </summary>
    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Metodo para pausar o jogo
    /// </summary>
    /// <param name="isPausado"></param>
    public void SetPauseMenu(bool isPausado) {
        pausado = isPausado;

        //Se o jogo estiver pausado, timescale recebe 0
        Time.timeScale = (pausado) ? 0 : 1;
        //Habilita/Desabilita o menu pause
        menuPausePanel.SetActive(pausado);
    }
    /// <summary>
    /// Metodo para carregar uma scene
    /// </summary>
    /// <param name="nomeScene"></param>
    public void CarregaScene(string nomeScene) {
        SceneManager.LoadScene(nomeScene);
    }

    private void Start() {
        //pausado = false;
        SetPauseMenu(false);
    }

}
