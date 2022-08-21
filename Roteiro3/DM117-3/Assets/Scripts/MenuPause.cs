using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public static bool Pause;

    [Tooltip(("Rreferencia para o GO. Usado para ligar/desligar"))]
    [SerializeField]
    private GameObject menuPause;

    /// <summary>
    /// Metodo para reiniciar a Scene, reiniciando o jogo.
    /// </summary>
    public void Restart()
    {
        print("Restart");       
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    /// <summary>
    /// Metodo para pausar o jogo.
    /// </summary>
    /// <param name="isPausado">Parametro booleano que indicara se o jogo sera pausado</param>
    public void SetMenuPause(bool isPausado)
    {
        Pause = isPausado;
        Time.timeScale = (Pause) ? 0 : 1;
        menuPause.SetActive(Pause);
    }
    
    /// <summary>
    /// Metodo para carregar a Scene.
    /// </summary>
    /// <param name="nomeScene">nome da scene que sera ca</param>
    public void CarregaScene(string nomeScene)
    {   
        print("cena carregada: " + nomeScene);
        SceneManager.LoadScene(nomeScene);
    }
    
    public void Test(bool test)
    {
        print("Teste");
    }

    void Start()
    {
        // pause = false;
        SetMenuPause(false);
    }
}
