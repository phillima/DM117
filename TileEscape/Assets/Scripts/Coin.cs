using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    [SerializeField]
    AudioClip pickUpClip;

	// Use this for initialization
	void Start () {
		
	}

    void OnTriggerEnter2D(Collider2D collision) {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(pickUpClip, transform.position);
    }
}
