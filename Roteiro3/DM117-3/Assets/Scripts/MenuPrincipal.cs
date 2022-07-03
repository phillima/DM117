using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void CarregaScene(string nomeScene)
    {
        SceneManager.LoadScene(nomeScene);
    }
}
