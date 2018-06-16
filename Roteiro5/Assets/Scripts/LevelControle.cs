using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControle : MonoBehaviour {

	public void CarregaLevel(string sceneNome) {
        SceneManager.LoadScene(sceneNome);
    }

}
