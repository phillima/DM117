using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour {

    //Control number of Balls in the game
    public static int numBalls;

    //Control if there are no more block
    //on the scene
    public static int numBlocks;

    //Control if the game has started
    public static bool hasGameStarted = false;

    void Awake() {
        hasGameStarted = false;
        numBalls = 0;
        numBlocks = 0;
    }

    public void LoadlLevel(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene()
            .buildIndex + 1);
    }

    public void BlockDestroyed() {
        numBlocks--;
        if(numBlocks <= 0) {
            LoadNextLevel();
        }
    }

    public void BallFallen() {
        LoadlLevel("TelaGameOver");
    }

}
