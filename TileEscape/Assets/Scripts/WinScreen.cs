using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour {

	public void PlayAgain() {
        if (FindObjectOfType<GameSession>()) {
            Destroy(FindObjectOfType<GameSession>().gameObject);
        }
        SceneManager.LoadScene("Level1");
    }
}
