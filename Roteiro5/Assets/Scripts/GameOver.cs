using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

    private LevelControle levelControle;

    private void Start() {
        levelControle = FindObjectOfType<LevelControle>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        levelControle.CarregaLevel("TelaVitoria");
    }

}
