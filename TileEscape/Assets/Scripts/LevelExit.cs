using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {

    [SerializeField]
    Animator uiLevelExitAnimator;

    void OnTriggerEnter2D(Collider2D collision) {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel() {
        uiLevelExitAnimator.SetTrigger("LoadLevel");
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
