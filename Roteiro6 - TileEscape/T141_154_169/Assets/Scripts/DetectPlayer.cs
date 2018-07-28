using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour {

    //Detecta que o player passou da porta
    //e assim o jogo pode comecar
    void OnTriggerEnter2D(Collider2D collision) {
        GameObject door = GameObject.Find("Door");
        Animator doorAnimator = door.GetComponent<Animator>();
        doorAnimator.SetTrigger("CloseDoor");
        GameSession.gameStart = true;
        Destroy(gameObject);
    }

}
