using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

    [SerializeField]
    private LevelControle levelControle;

    private void OnTriggerEnter2D(Collider2D collision) {
        levelControle.CarregaLevel("TelaVitoria");
    }

}
