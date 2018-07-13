using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour {

    const string TAG_PLAYER = "Player";

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(TAG_PLAYER)) {
            if (collision.GetContact(0).normal.y <= -0.7) {
                collision.gameObject.SendMessage("PlayerDead");
            }
        }
    }
}
