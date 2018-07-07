using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour {

    public static bool isPausado = false;

    [SerializeField]
    GameObject menuPausePanel;

    public void Resumir() {
        Time.timeScale = 1;
        isPausado = false;
        menuPausePanel.SetActive(false);
    }

    public void Reiniciar() {
        Time.timeScale = 1;
        isPausado = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MenuPrincipal() {
        Time.timeScale = 1;
        isPausado = false;
        SceneManager.LoadScene("TelaInicial");
    }

    public void Pausar() {
        Time.timeScale = 0;
        isPausado = true;
        menuPausePanel.SetActive(true);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
