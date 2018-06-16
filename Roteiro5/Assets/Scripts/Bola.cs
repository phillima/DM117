using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bola : MonoBehaviour {

    private Plataforma plataforma;

    private Rigidbody2D rb2D;

    private bool jogoComecou = false;

    private Vector3 plataformaBolaDis;

    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        rb2D = GetComponent<Rigidbody2D>();
        plataforma = FindObjectOfType<Plataforma>();
        audioSource = GetComponent<AudioSource>();
        plataformaBolaDis = transform.position -
                    plataforma.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (!jogoComecou) {
            transform.position = plataforma.transform.position +
            plataformaBolaDis;
            if (Input.GetMouseButton(0)) {
                rb2D.velocity = new Vector2(2f,10f);
                jogoComecou = true;
            }
        }
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        audioSource.Play();
    }
}
