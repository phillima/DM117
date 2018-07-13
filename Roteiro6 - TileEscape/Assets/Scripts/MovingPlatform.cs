using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    //Cached
    bool hasPlayer = false;

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Player") && !hasPlayer) {
            hasPlayer = true;
            collision.gameObject.transform.SetParent(gameObject.transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player") && hasPlayer) {
            hasPlayer = false;
            collision.gameObject.transform.SetParent(null);
        }
    }
}

