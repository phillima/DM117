using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    //Serialized
    [SerializeField]
    float enemySpeed = 2.0f;

    //Cached
    Rigidbody2D enemyRB;

	// Use this for initialization
	void Start () {
		enemyRB = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate() {
        enemyRB.velocity = new Vector2(enemySpeed,0.0f);
    }

    void OnTriggerExit2D(Collider2D collision) {
        if(!collision.CompareTag("Player"))
            FlipSprite();
    }

    void FlipSprite() { 
        transform.localScale = new Vector3(-Mathf.Sign(enemyRB.velocity.x), 1, 1);
        enemySpeed *= -1;
    }
}
