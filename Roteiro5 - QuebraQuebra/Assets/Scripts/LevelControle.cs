using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControle : MonoBehaviour {

	public void CarregaLevel(string sceneNome) {
        Bloco.numBlocosDestrutivel = 0;
        SceneManager.LoadScene(sceneNome);
    }

    public void CarregaProxLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene()
            .buildIndex + 1);
    }

    public void BlocoDestruido() {
        if(Bloco.numBlocosDestrutivel <= 0) {
            Bloco.numBlocosDestrutivel = 0;
            CarregaProxLevel();
        }
    }

}
